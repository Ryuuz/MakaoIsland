using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternScript : MonoBehaviour
{
    private Renderer mRenderer;
    private Material mMaterial;

    void Start()
    {
        mRenderer = GetComponentInChildren<Renderer>();
        Material[] materials = mRenderer.materials;
        for(int i = 0; i < materials.Length; i++)
        {
            if (materials[i].IsKeywordEnabled("_EMISSION"))
            {
                mMaterial = materials[i];
                i = materials.Length;
            }
        }
        GameManager.ManagerInstance().eTimeChanged.AddListener(ToggleLantern);
        ToggleLantern(GameManager.ManagerInstance().mGameStatus.mDayTime);
    }

    public void ToggleLantern(DayCyclus timeOfDay)
    {
        if(mRenderer)
        {
            //This is bad
            if (timeOfDay == DayCyclus.night)
            {
                Color onColor = new Color(0.506f, 0.173f, 0.02f) * 10f;
                mMaterial.SetColor("_EmissionColor", onColor);
                DynamicGI.SetEmissive(mRenderer, onColor);
            }
            else if (timeOfDay == DayCyclus.dawn)
            {
                Color offColor = new Color(0.506f, 0.173f, 0.02f) * 0.01f;
                mMaterial.SetColor("_EmissionColor", offColor);
                DynamicGI.SetEmissive(mRenderer, offColor);
            }
        }
    }
}
