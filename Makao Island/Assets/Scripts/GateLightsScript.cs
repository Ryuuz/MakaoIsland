using System.Collections;
using UnityEngine;

public class GateLightsScript : MonoBehaviour
{
    public float mIntensity = 0.4f;
    public float mEmissionDelay = 0.4f;

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

    //Update the materials on the door based on what spirit animals have been found
    public void UpdateLights(int n)
    {
        //Goes through and checks all the animals
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
                        //If it is the correct material for the animal
                        if(mRenderers[j].material.name == (mGateMaterials[i].name + " (Instance)"))
                        {
                            mRenderers[j].material.SetColor("_EmissionColor", emissiveColor);
                            DynamicGI.SetEmissive(mRenderers[j], emissiveColor);
                        }
                    }
                }
            }
        }
        //Only for the given animal
        else if (n < mGateMaterials.Length)
        {
            StartCoroutine(FadeInLight(n));
        }
    }

    //Increases the intensity of the emission over time
    private IEnumerator FadeInLight(int n)
    {
        Color startColor = mGateMaterials[n].GetColor("_EmissionColor");
        Color endColor = (mLightColors.Length > n) ? mLightColors[n] : Color.white;
        endColor *= mIntensity;
        Color emissiveColor;
        float lerpTime = 0f;

        while(lerpTime <= 1f)
        {
            lerpTime += (Time.deltaTime * mEmissionDelay);

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
    }
}
