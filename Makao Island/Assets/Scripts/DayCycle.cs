using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    public float mDawnLength = 10f;
    public float mDayLength = 20f;
    public float mDuskLength = 10f;
    public float mNightLength = 20f;
    public float mCyclusSpeed = 1f;

    private float mCurrentTime;
    private DayCyclus mCurrentCyclusStep;
    private float mCurrentCyclusLength;
    private float mCurrentRotation;
    private float mRotationStep;

    enum DayCyclus
    {
        dawn, day, dusk, night
    };

	void Start()
    {
        mCurrentCyclusStep = DayCyclus.dawn;
        mCurrentCyclusLength = mDawnLength;
        mCurrentTime = 0f;
        mRotationStep = 360f / (mDawnLength + mDayLength + mDuskLength + mNightLength);
        mCurrentRotation = 0f;
    }
	
	void Update()
    {
        mCurrentTime += Time.deltaTime * mCyclusSpeed;
        mCurrentRotation = (mRotationStep * mCurrentTime);
        transform.eulerAngles = new Vector3(mCurrentRotation, 0f, 0f);

        if(mCurrentTime >= mCurrentCyclusLength)
        {
            NextCyclusStep();
        }
	}

    void NextCyclusStep()
    {
        switch (mCurrentCyclusStep)
        {
            case DayCyclus.dawn:
                mCurrentCyclusLength += mDayLength;
                mCurrentCyclusStep = DayCyclus.day;
                break;

            case DayCyclus.day:
                mCurrentCyclusLength += mDuskLength;
                mCurrentCyclusStep = DayCyclus.dusk;
                break;

            case DayCyclus.dusk:
                mCurrentCyclusLength += mNightLength;
                mCurrentCyclusStep = DayCyclus.night;
                break;

            case DayCyclus.night:
                mCurrentCyclusLength = mDawnLength;
                mCurrentTime = 0f;
                mCurrentCyclusStep = DayCyclus.dawn;
                break;
        }
    }
}
