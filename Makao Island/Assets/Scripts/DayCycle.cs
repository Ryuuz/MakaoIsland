using UnityEngine;
using UnityEngine.Events;

public class DayCycle : MonoBehaviour
{
    //How long each part of the day last
    public float mDawnLength = 10f;
    public float mDayLength = 20f;
    public float mDuskLength = 10f;
    public float mNightLength = 20f;
    //public float mFullCycleLength = 60f;

    public Gradient mSkyColorTint;
    public Gradient mSunlightTint;
    public AnimationCurve mAtmosphere;
    public AnimationCurve mExposure;
    public AnimationCurve mSunIntensity;

    [HideInInspector]
    public UnityEvent eTimeChanged = new UnityEvent();

    private float[] mStartRotation = new float[] { 335f, 15f, 165f, 205f };
    private float[] mCyclusLength;
    private float[] mRotationStep;

    private DayCyclus mCurrentCyclusStep;
    private float mCurrentTime = 0;
    private float mCurrentRotation;
    private float mFactor;
    private float mOffset = 0;

    private Material mSkyMaterial;
    private Light mSun;

    private GameManager mGameManager;

	void Start()
    {
        mGameManager = GameManager.ManagerInstance();

        if(mGameManager)
        {
            eTimeChanged.AddListener(mGameManager.TimeOfDayChanged);
        }

        mSkyMaterial = RenderSettings.skybox;
        mSun = RenderSettings.sun;
        mFactor = 1f / 360f;
        mCyclusLength = new float[] { mDawnLength, mDayLength, mDuskLength, mNightLength };
        mRotationStep = new float[mCyclusLength.Length];

        //Finds how fast the sun must move for each cycle step
        for(int i = 0; i < mRotationStep.Length; i++)
        {
            if ((mStartRotation[(i + 1) % mRotationStep.Length] < mStartRotation[i]))
            {
                mOffset = (360f - mStartRotation[i]);
                mRotationStep[i] = mOffset + mStartRotation[(i + 1) % mRotationStep.Length];
            }
            else
            {
                mRotationStep[i] = mStartRotation[(i + 1) % mRotationStep.Length] - mStartRotation[i];
            }

            mRotationStep[i] /= mCyclusLength[i];
        }

        //Retrieves the saved time of day
        mCurrentCyclusStep = mGameManager.mGameStatus.mDayTime;
        mCurrentTime = mGameManager.mGameStatus.mCyclusTime;
        
        //Sets the rotation the sun should start at
        mCurrentRotation = mStartRotation[(int)mCurrentCyclusStep] + (mRotationStep[(int)mCurrentCyclusStep] * mCurrentTime);
    }
	
	void Update()
    {
        //Moves the day and night cycle along at the set speed
        mCurrentTime += Time.deltaTime * mGameManager.mGameSpeed;

        if(mCurrentTime >= mCyclusLength[(int)mCurrentCyclusStep])
        {
            NextCyclusStep();
            eTimeChanged.Invoke();
        }

        mCurrentRotation = mStartRotation[(int)mCurrentCyclusStep] + (mRotationStep[(int)mCurrentCyclusStep] * mCurrentTime);
        transform.eulerAngles = new Vector3(mCurrentRotation, 0f, 0f);
        UpdateSky((mCurrentRotation + mOffset) * mFactor);
    }

    //Change to the next part of the day
    private void NextCyclusStep()
    {
        mCurrentTime = 0f;

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
                break;
        }
    }

    private void UpdateSky(float sunProgress)
    {
        if(mSkyMaterial)
        {
            mSkyMaterial.SetColor("_SkyTint", mSkyColorTint.Evaluate(sunProgress));
            mSkyMaterial.SetFloat("_AtmosphereThickness", mAtmosphere.Evaluate(sunProgress));
            mSkyMaterial.SetFloat("_Exposure", mExposure.Evaluate(sunProgress));
        }
        
        if(mSun)
        {
            mSun.color = mSunlightTint.Evaluate(sunProgress);
            mSun.intensity = mSunIntensity.Evaluate(sunProgress);
        }
    }

    public DayCyclus GetTimeOfDay()
    {
        return mCurrentCyclusStep;
    }
}
