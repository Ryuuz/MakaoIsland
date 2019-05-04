using UnityEngine;

//http://wiki.unity3d.com/index.php/Underwater_Script
public class UnderwaterScript : MonoBehaviour
{
    public Color mUnderwaterColor = new Color(1, 1, 1, 1);
    public float mFogDensity = 0.01f;

    private Transform mWaterTransform;
    private Transform mCameraTransform;
    private bool mIsUnderwater = false;
    private Color mFogColor;
    private float mDefaultDensity;

    void Start()
    {
        mCameraTransform = GameManager.ManagerInstance().mMainCamera.transform;
        mWaterTransform = gameObject.transform;

        //The default values
        mFogColor = RenderSettings.fogColor;
        mDefaultDensity = RenderSettings.fogDensity;
    }

    void Update()
    {
        //Increase the fog when going underwater
        if(!mIsUnderwater && mCameraTransform.position.y < mWaterTransform.position.y)
        {
            RenderSettings.fogColor = mUnderwaterColor;
            RenderSettings.fogDensity = mFogDensity;
            mIsUnderwater = true;
        }
        //Reset the fog to default values when out of the water
        else if(mIsUnderwater && mCameraTransform.position.y >= mWaterTransform.position.y)
        {
            RenderSettings.fogColor = mFogColor;
            RenderSettings.fogDensity = mDefaultDensity;
            mIsUnderwater = false;
        }
    }
}
