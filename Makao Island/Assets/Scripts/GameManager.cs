using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SpeedChangeEvent : UnityEvent<float>
{

}

[System.Serializable]
public class TimeChangeEvent : UnityEvent<DayCyclus>
{

}

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public InputHandler mInputHandler;

    public GameObject mPlayer;
    public GameObject mMainCamera;
    public GameObject mDialogueManager;
    public DayCycle mDayCycle;

    public SpeedChangeEvent eSpeedChanged = new SpeedChangeEvent();
    public TimeChangeEvent eTimeChanged = new TimeChangeEvent();

    public float mGameSpeed = 1f;

    //Singleton to ensure only one instance of the class
    private static GameManager sGameManager;

    //Returns an instance of the class if it exists. Otherwise creates an instance
    public static GameManager ManagerInstance()
    {
        if(sGameManager == null)
        {
            GameObject manager = GameObject.Find("Manager");

            if (manager)
            {
                sGameManager = manager.AddComponent<GameManager>();
            }
            else
            {
                manager = new GameObject("Manager");
                sGameManager = manager.AddComponent<GameManager>();
            }
        }

        return sGameManager;
    }

    private void Awake()
    {
        //See if an instance of the class already exists
        if(sGameManager == null)
        {
            sGameManager = this;
        }
        else if(sGameManager != this)
        {
            Destroy(this);
        }

        //Get the needed objects if they haven't been provided
        if (!mPlayer)
        {
            mPlayer = GameObject.Find("Player");
        }
        if (!mMainCamera)
        {
            mMainCamera = GameObject.Find("Main Camera");
        }
        if(!mDayCycle)
        {
            GameObject tempDay = GameObject.Find("DayNight");

            if(tempDay)
            {
                mDayCycle = tempDay.GetComponent<DayCycle>();
            }
        }
    }

    void Start()
    {
        mInputHandler = InputHandler.InputInstance();

        if(mDayCycle)
        {
            eSpeedChanged.AddListener(mDayCycle.CycleSpeedChanged);
        }
    }

    //Set the speed the game should play at. 0 = pause, 1 = normal speed, >1 = speed up
    public void SetGameSpeed(float speed)
    {
        mGameSpeed = speed;
        eSpeedChanged.Invoke(mGameSpeed);
    }

    //When the time of the day changes
    public void TimeOfDayChanged()
    {
        if(mDayCycle)
        {
            DayCyclus currentTimeOfDay = mDayCycle.GetTimeOfDay();
            eTimeChanged.Invoke(currentTimeOfDay);
        }
    }
}
