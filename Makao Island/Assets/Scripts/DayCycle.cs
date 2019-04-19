using UnityEngine;
using UnityEngine.Events;

public class DayCycle : MonoBehaviour
{
    public float mCycleLength = 1800f;
    public float mDawnStartRotation = 335f;
    public float mDayStartRotation = 20f;
    public float mDuskStartRotation = 155f;
    public float mNightStartRotation = 205f;

    public Gradient mSkyColorTint;
    public Gradient mSunlightTint;
    public AnimationCurve mAtmosphere;
    public AnimationCurve mExposure;
    public AnimationCurve mSunIntensity;

    [HideInInspector]
    public UnityEvent eTimeChanged = new UnityEvent();

    [SerializeField]
    private CloudScript mClouds;

    private float[] mStartRotation;
    private float[] mRotationDegrees;

    private Transform mTransform;
    private DayCyclus mCurrentCyclusStep;
    private float mCurrentTime = 0;
    private float mCurrentRotation;
    private float mFactor;
    private float mOffset = 0;
    private float mRotationStep;

    private Material mSkyMaterial;
    private Light mSun;

    private GameManager mGameManager;

	void Start()
    {
        mTransform = GetComponent<Transform>();
        mGameManager = GameManager.ManagerInstance();
        eTimeChanged.AddListener(mGameManager.TimeOfDayChanged);

        mSkyMaterial = RenderSettings.skybox;
        mSun = RenderSettings.sun;
        mFactor = 1f / 360f;
        mRotationStep = 360f / mCycleLength;

        mStartRotation = new float[] { mDawnStartRotation, mDayStartRotation, mDuskStartRotation, mNightStartRotation };
        mRotationDegrees = new float[mStartRotation.Length];

        //How many degrees each part of the day lasts
        for(int i = 0; i < mRotationDegrees.Length; i++)
        {
            //If the next rotation is less than the current one the offset must be taken into consideration
            if ((mStartRotation[(i + 1) % mRotationDegrees.Length] < mStartRotation[i]))
            {
                mOffset = (360f - mStartRotation[i]);
                mRotationDegrees[i] = mOffset + mStartRotation[(i + 1) % mRotationDegrees.Length];
            }
            else
            {
                mRotationDegrees[i] = mStartRotation[(i + 1) % mRotationDegrees.Length] - mStartRotation[i];
            }
        }

        //Retrieves the saved time of day
        mCurrentCyclusStep = (DayCyclus)mGameManager.mData.mDayTime;
        mCurrentTime = mGameManager.mData.mCyclusTime;
        
        //Sets the rotation the sun should start at
        mCurrentRotation = (mRotationStep * mCurrentTime) - mOffset;
    }
	
	void Update()
    {
        mCurrentTime += (Time.deltaTime * mGameManager.mGameSpeed);

        //Checking if the time of day should change
        if(mCurrentRotation >= ((mStartRotation[(int)mCurrentCyclusStep] + mRotationDegrees[(int)mCurrentCyclusStep]) % 360))
        {
            NextCyclusStep();
            eTimeChanged.Invoke();
        }

        //Update the rotation
        mCurrentRotation = (mRotationStep * mCurrentTime) - mOffset;
        mTransform.eulerAngles = new Vector3(mCurrentRotation, 0f, 0f);
        UpdateSky((mCurrentRotation + mOffset) * mFactor);
    }

    //Change to the next part of the day
    private void NextCyclusStep()
    {
        switch (mCurrentCyclusStep)
        {
            case DayCyclus.dawn:
                mCurrentCyclusStep = DayCyclus.day;
                break;

            case DayCyclus.day:
                mCurrentCyclusStep = DayCyclus.dusk;
                break;

            case DayCyclus.dusk:
                mCurrentCyclusStep = DayCyclus.night;
                break;

            case DayCyclus.night:
                mCurrentCyclusStep = DayCyclus.dawn;
                mCurrentTime = 0f;
                break;
        }
    }

    //Changes the properties of the sky based on the sun's position
    private void UpdateSky(float sunProgress)
    {
        //Skybox
        if(mSkyMaterial)
        {
            mSkyMaterial.SetColor("_SkyTint", mSkyColorTint.Evaluate(sunProgress));
            mSkyMaterial.SetFloat("_AtmosphereThickness", mAtmosphere.Evaluate(sunProgress));
            mSkyMaterial.SetFloat("_Exposure", mExposure.Evaluate(sunProgress));
        }
        
        //Main directional light
        if(mSun)
        {
            mSun.color = mSunlightTint.Evaluate(sunProgress);
            mSun.intensity = mSunIntensity.Evaluate(sunProgress);
        }

        //Clouds
        if(mClouds)
        {
            mClouds.UpdateCloudColor(sunProgress);
        }
    }

    public DayCyclus GetTimeOfDay()
    {
        return mCurrentCyclusStep;
    }

    public float GetCurrentTime()
    {
        return mCurrentTime;
    }
}
