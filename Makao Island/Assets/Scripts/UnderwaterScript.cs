using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        mFogColor = RenderSettings.fogColor;
        mDefaultDensity = RenderSettings.fogDensity;
    }

    void Update()
    {
        if(!mIsUnderwater && mCameraTransform.position.y < mWaterTransform.position.y)
        {
            RenderSettings.fogColor = mUnderwaterColor;
            RenderSettings.fogDensity = mFogDensity;
            mIsUnderwater = true;
        }
        else if(mIsUnderwater && mCameraTransform.position.y > mWaterTransform.position.y)
        {
            RenderSettings.fogColor = mFogColor;
            RenderSettings.fogDensity = mDefaultDensity;
            mIsUnderwater = false;
        }
    }
}
