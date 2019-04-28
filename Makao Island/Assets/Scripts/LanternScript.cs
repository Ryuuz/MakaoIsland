using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LanternScript : MonoBehaviour
{
    public Color mLightColor;
    public float mOnStrength = 5f;
    public float mOffStrength = 0.01f;
    public float mLightIntensity = 1f;
    private Renderer mRenderer;
    private Material mMaterial;
    private Light mLight;

    void Start()
    {
        mRenderer = GetComponentInChildren<Renderer>();
        Material[] materials = mRenderer.materials;
        mLight = GetComponentInChildren<Light>();
        mLight.color = mLightColor;

        //Find the material with emission enabled
        for (int i = 0; i < materials.Length; i++)
        {
            if (materials[i].IsKeywordEnabled("_EMISSION"))
            {
                mMaterial = materials[i];
                i = materials.Length;
            }
        }

        Color onColor = mLightColor * mOnStrength;
        mMaterial.SetColor("_EmissionColor", onColor);
        DynamicGI.SetEmissive(mRenderer, onColor);
        mLight.intensity = mLightIntensity;

        GameManager.ManagerInstance().eTimeChanged.AddListener(ToggleLantern);
        ToggleLantern((DayCyclus)GameManager.ManagerInstance().mData.mDayTime);
    }

    //Increase emission at night, decrease at dawn
    public void ToggleLantern(DayCyclus timeOfDay)
    {
        if(mRenderer)
        {
            if(timeOfDay == DayCyclus.dusk)
            {

                Color onColor = mLightColor * mOnStrength;
                mMaterial.SetColor("_EmissionColor", onColor);
                DynamicGI.SetEmissive(mRenderer, onColor);
                mLight.intensity = mLightIntensity;
            }
            else if(timeOfDay == DayCyclus.day)
            {
                Color offColor = mLightColor * mOffStrength;
                mMaterial.SetColor("_EmissionColor", offColor);
                DynamicGI.SetEmissive(mRenderer, offColor);
                mLight.intensity = 0f;
            }
        }
    }
}
