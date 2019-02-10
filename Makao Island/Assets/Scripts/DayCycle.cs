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
    private float mCurrentTime = 0;
    private float mCurrentCyclusLength = 0;
    private float mCurrentRotation;
    private float mRotationStep;
    private float mCyclusSpeed;

    private GameManager mGameManager;

	void Start()
    {
        mGameManager = GameManager.ManagerInstance();

        if(mGameManager)
        {
            eTimeChanged.AddListener(mGameManager.TimeOfDayChanged);
            mGameManager.eSpeedChanged.AddListener(CycleSpeedChanged);
        }

        mCyclusSpeed = mGameManager.mGameSpeed;
        float[] tempCyclusLengths = new float[] { mDawnLength, mDayLength, mDuskLength, mNightLength };

        //Retrieves the saved time of day
        mCurrentCyclusStep = mGameManager.mGameStatus.mDayTime;

        //Sets the current cyclus' length and the current time
        for(int i = 0; i <= (int)mCurrentCyclusStep; i++)
        {
            mCurrentCyclusLength += tempCyclusLengths[i];
        }

        //Time is set to beginning of the cyclus step
        for (int i = 0; i < (int)mCurrentCyclusStep; i++)
        {
            mCurrentTime += tempCyclusLengths[i]; //-----------safer option is to save current time and find cyclus step based on it
        }
        
        //The positions of the sun and moon in the sky
        mRotationStep = 360f / (mDawnLength + mDayLength + mDuskLength + mNightLength);
        mCurrentRotation = mRotationStep * mCurrentTime;
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

    public void CycleSpeedChanged(float speed)
    {
        mCyclusSpeed = speed;
    }

    public DayCyclus GetTimeOfDay()
    {
        return mCurrentCyclusStep;
    }
}
