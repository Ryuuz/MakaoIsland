using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DayCycle : MonoBehaviour
{
    //How long each part of the day last
    public float mDawnLength = 10f;
    public float mDayLength = 20f;
    public float mDuskLength = 10f;
    public float mNightLength = 20f;

    public UnityEvent eTimeChanged = new UnityEvent();

    private DayCyclus mCurrentCyclusStep;
    private float mCurrentTime;
    private float mCurrentCyclusLength;
    private float mCurrentRotation;
    private float mRotationStep;
    private float mCyclusSpeed;

    private GameManager mGameManager;

	void Start()
    {
        mGameManager = GameManager.ManagerInstance();
        eTimeChanged.AddListener(mGameManager.TimeOfDayChanged);
        CycleSpeedChanged();

        //Game starts at dawn
        mCurrentCyclusStep = DayCyclus.dawn;
        mCurrentCyclusLength = mDawnLength;
        mCurrentTime = 0f;

        mRotationStep = 360f / (mDawnLength + mDayLength + mDuskLength + mNightLength);
        mCurrentRotation = 0f;
    }
	
	void Update()
    {
        //Moves the day and night cycle along at the set speed
        mCurrentTime += Time.deltaTime * mCyclusSpeed;
        mCurrentRotation = (mRotationStep * mCurrentTime);
        transform.eulerAngles = new Vector3(mCurrentRotation, 0f, 0f);

        if(mCurrentTime >= mCurrentCyclusLength)
        {
            NextCyclusStep();
            eTimeChanged.Invoke();
        }
	}

    //Change to the next part of the day
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

    public void CycleSpeedChanged()
    {
        mCyclusSpeed = mGameManager.GetGameSpeed();
    }

    public DayCyclus GetTimeOfDay()
    {
        return mCurrentCyclusStep;
    }
}
