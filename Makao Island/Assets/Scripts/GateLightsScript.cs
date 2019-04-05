using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateLightsScript : MonoBehaviour
{
    public float mIntensity = 0.4f;

    [SerializeField]
    private Material[] mGateMaterials;
    [SerializeField]
    private Color[] mLightColors;

    private Renderer[] mRenderers;

    void Start()
    {
        GameManager.ManagerInstance().eSpiritAnimalFound.AddListener(UpdateLights);
        mRenderers = GetComponentsInChildren<Renderer>();
        UpdateLights((int)SpiritAnimalType.spiritAnimals);
    }

    public void UpdateLights(int n)
    {
        if (n == mGateMaterials.Length)
        {
            for (int i = 0; i < mGateMaterials.Length; i++)
            {
                if (GameManager.ManagerInstance().mData.mSpiritAnimalsStatus[i])
                {
                    Color emissiveColor = (mLightColors.Length > i) ? mLightColors[i] : Color.white;
                    emissiveColor *= mIntensity;

                    for(int j = 0; j < mRenderers.Length; j++)
                    {
                        if(mRenderers[j].material.name == (mGateMaterials[i].name + " (Instance)"))
                        {
                            mRenderers[j].material.SetColor("_EmissionColor", emissiveColor);
                            DynamicGI.SetEmissive(mRenderers[j], emissiveColor);
                        }
                    }
                }
            }
        }
        else if (n < mGateMaterials.Length)
        {
            StartCoroutine(FadeInLight(n));
        }
    }

    private IEnumerator FadeInLight(int n)
    {
        Color startColor = mGateMaterials[n].GetColor("_EmissionColor");
        Color endColor = (mLightColors.Length > n) ? mLightColors[n] : Color.white;
        endColor *= mIntensity;
        Color emissiveColor;
        float lerpTime = 0f;

        while(lerpTime <= 1f)
        {
            lerpTime += (Time.deltaTime * 0.4f) * GameManager.ManagerInstance().mGameSpeed;

            emissiveColor = Color.Lerp(startColor, endColor, lerpTime);

            for (int i = 0; i < mRenderers.Length; i++)
            {
                if (mRenderers[i].material.name == (mGateMaterials[n].name + " (Instance)"))
                {
                    mRenderers[i].material.SetColor("_EmissionColor", emissiveColor);
                    DynamicGI.SetEmissive(mRenderers[i], emissiveColor);
                }
            }

            yield return null;
        }

        for (int i = 0; i < mRenderers.Length; i++)
        {
            if (mRenderers[i].material.name == (mGateMaterials[n].name + " (Instance)"))
            {
                mRenderers[i].material.SetColor("_EmissionColor", endColor);
                DynamicGI.SetEmissive(mRenderers[i], endColor);
            }
        }
    }
}
