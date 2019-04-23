using UnityEngine;

public class LanternScript : MonoBehaviour
{
    public Color mLightColor;
    public float mOnStrength = 5f;
    public float mOffStrength = 0.01f;
    private Renderer mRenderer;
    private Material mMaterial;

    void Start()
    {
        mRenderer = GetComponentInChildren<Renderer>();
        Material[] materials = mRenderer.materials;

        //Find the material with emission enabled
        for (int i = 0; i < materials.Length; i++)
        {
            if (materials[i].IsKeywordEnabled("_EMISSION"))
            {
                mMaterial = materials[i];
                i = materials.Length;
            }
        }
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
            }
            else if(timeOfDay == DayCyclus.dawn)
            {
                Color offColor = mLightColor * mOffStrength;
                mMaterial.SetColor("_EmissionColor", offColor);
                DynamicGI.SetEmissive(mRenderer, offColor);
            }
        }
    }
}
