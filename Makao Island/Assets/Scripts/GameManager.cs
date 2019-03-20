using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

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
    public float mGameSpeed = 1f;
    public GameObject mPlayer;
    public GameObject mMainCamera;
    public GameObject mDialogueManager;
    public ControlsUIScript mControlUI;

    public GameData mData;

    //Events
    [HideInInspector]
    public SpeedChangeEvent eSpeedChanged = new SpeedChangeEvent();
    [HideInInspector]
    public TimeChangeEvent eTimeChanged = new TimeChangeEvent();
    [HideInInspector]
    public UnityEvent eSpiritAnimalFound = new UnityEvent();

    [HideInInspector]
    public InputHandler mInputHandler;
    public Transform mCurrentRespawnPoint;

    [SerializeField]
    private DayCycle mDayCycle;

    private List<AIController> mAIs = new List<AIController>();

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

        GameObject[] tempAIs = GameObject.FindGameObjectsWithTag("NPC");
        for(int i = 0; i < tempAIs.Length; i++)
        {
            //mAIs.Add(tempAIs[i].GetComponent<AIController>());
        }
        mAIs.Sort((obj1, obj2) => obj1.gameObject.name.CompareTo(obj2.gameObject.name));

        //Assign game data
        RetrieveData();
    }

    void Start()
    {
        mInputHandler = InputHandler.InputInstance();
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

    //The status of a spirit animal has changed
    public void UpdateSpiritAnimals(int type)
    {
        mData.mSpiritAnimalsStatus[type] = true;
        eSpiritAnimalFound.Invoke();
    }

    //Load or generate the player's progress
    private void RetrieveData()
    {
        if(PlayerPrefs.GetInt("Load", 0) == 0)
        {
            mData = new GameData();
        }
        else
        {
            mData = SaveGameScript.LoadData();
            Destroy(GameObject.Find("GodAnimal"));

            if(mData.mSpiritGirlStatus)
            {
                Destroy(GameObject.Find("SpiritGirlGuiding"));
            }

            mPlayer.transform.position = new Vector3(mData.mPlayerPosition[0], mData.mPlayerPosition[1], mData.mPlayerPosition[2]);
            mCurrentRespawnPoint = GameObject.Find(mData.mCheckPoint).transform;

            Vector3 AIPosition;
            for(int i = 0; i < mData.mAIPositions.Length; i++)
            {
                AIPosition = new Vector3(mData.mAIPositions[i][0], mData.mAIPositions[i][3], mData.mAIPositions[i][2]);
                mAIs[i].mCurrentLocation = AIPosition + Random.insideUnitSphere * mAIs[i].mWaypointRadius;
                mAIs[i].mCurrentLocation.y = AIPosition.y;
            }
        }
    }

    public void StoreData()
    {
        Transform playerTransform = mPlayer.transform;
        mData.mPlayerPosition = new float[3] { playerTransform.position.x, playerTransform.position.y, playerTransform.position.z };
        mData.mDayTime = (int)mDayCycle.GetTimeOfDay();
        mData.mCyclusTime = mDayCycle.GetCurrentTime();
        mData.mCheckPoint = mCurrentRespawnPoint.name;

        mData.mAIPositions = new float[mAIs.Count][];
        for(int i = 0; i < mAIs.Count; i++)
        {
            mData.mAIPositions[i] = new float[3] { mAIs[i].mCurrentLocation.x, mAIs[i].mCurrentLocation.y, mAIs[i].mCurrentLocation.z };
        }

        SaveGameScript.SaveData();
    }
}
