﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine.AI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public float mGameSpeed = 1f;
    public GameObject mPlayer;
    public GameObject mMainCamera;
    public GameObject mDialogueManager;
    public ControlsUIScript mControlUI;

    [HideInInspector]
    public GameData mData;
    [HideInInspector]
    public InputHandler mInputHandler;
    [HideInInspector]
    public Transform mCurrentRespawnPoint;

    //Events
    [HideInInspector]
    public SpeedChangeEvent eSpeedChanged = new SpeedChangeEvent();
    [HideInInspector]
    public TimeChangeEvent eTimeChanged = new TimeChangeEvent();
    [HideInInspector]
    public SpiritAnimalEvent eSpiritAnimalFound = new SpiritAnimalEvent();
    [HideInInspector]
    public UnityEvent eSpiritGirlFound = new UnityEvent();

    [SerializeField]
    private DayCycle mDayCycle;

    private PlayableDirector mStartDirector;
    private PlayableAsset mStartClip;
    private PlayableDirector mEndDirector;
    private PlayableAsset mEndClip;
    private PlayableDirector mDoorDirector;
    private PlayableAsset mDoorClip;
    private List<string> mRemovedObjects = new List<string>();
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

        //Cutscenes (doing it like this because it seems to be bugged in the editor)
        mStartDirector = GameObject.Find("OpeningTimeline").GetComponent<PlayableDirector>();
        mStartClip = mStartDirector.playableAsset;
        mEndDirector = GameObject.Find("EndingTimeline").GetComponent<PlayableDirector>();
        mEndClip = mEndDirector.playableAsset;
        mDoorDirector = GameObject.Find("OpenDoor").GetComponent<PlayableDirector>();
        mDoorClip = mDoorDirector.playableAsset;

        //Retrieve all AIs and sort them so they will always be in the same order
        GameObject[] tempAIs = GameObject.FindGameObjectsWithTag("NPC");
        for(int i = 0; i < tempAIs.Length; i++)
        {
            mAIs.Add(tempAIs[i].GetComponent<AIController>());
        }
        mAIs.Sort((obj1, obj2) => obj1.gameObject.name.CompareTo(obj2.gameObject.name));

        //Load or generate data
        RetrieveData();
    }

    void Start()
    {
        mInputHandler = InputHandler.InputInstance();
        NavMesh.avoidancePredictionTime = 5f;

        //Play opening cutscene if new game
        if (PlayerPrefs.GetInt("Load", 0) == 0)
        {
            if (mInputHandler)
            {
                mInputHandler.StartCutscene((float)mStartClip.duration);
            }

            mStartDirector.Play(mStartClip);
        }
    }

    //Set the speed the game should play at. 1 = normal speed, >1 = speed up
    public void SetGameSpeed(float speed)
    {
        mGameSpeed = speed;
        eSpeedChanged.Invoke(mGameSpeed);
    }

    //When the time of the day changes
    public void TimeOfDayChanged()
    {
        DayCyclus currentTimeOfDay = mDayCycle.GetTimeOfDay();
        eTimeChanged.Invoke(currentTimeOfDay);
    }

    public void FoundSpiritGirl()
    {
        mData.mSpiritGirlStatus = true;
        eSpiritGirlFound.Invoke();
    }

    //The status of a spirit animal has changed
    public void UpdateSpiritAnimals(int type)
    {
        mData.mSpiritAnimalsStatus[type] = true;
        eSpiritAnimalFound.Invoke(type);

        //Check if all of the spirits have been found
        bool allSpiritsFound = true;
        for(int i = 0; i < mData.mSpiritAnimalsStatus.Length; i++)
        {
            allSpiritsFound = allSpiritsFound && mData.mSpiritAnimalsStatus[i];
        }

        //If all are found, open the stone gate
        if(allSpiritsFound)
        {
            StartCoroutine(OpenGate());
        }
    }

    //Load or generate the player's progress
    private void RetrieveData()
    {
        //Starting a new game. Data set to default
        if(PlayerPrefs.GetInt("Load", 0) == 0)
        {
            mData = new GameData();
            mCurrentRespawnPoint = GameObject.Find(mData.mCheckPoint).transform;
        }
        else
        {
            bool openDoor = true;
            mData = SaveGameScript.LoadData();

            if(mData.mMapStatus)
            {
                Destroy(GameObject.Find("Pandamoose"));
            }

            for(int i = 0; i < mData.mSpiritAnimalsStatus.Length; i++)
            {
                openDoor = openDoor && mData.mSpiritAnimalsStatus[i];
            }

            if(openDoor)
            {
                mDoorDirector.Play(mDoorClip);
            }

            mPlayer.transform.position = new Vector3(mData.mPlayerPosition[0], mData.mPlayerPosition[1], mData.mPlayerPosition[2]);
            mCurrentRespawnPoint = GameObject.Find(mData.mCheckPoint).transform;

            //Place all the AIs
            Vector3 AIPosition;
            for(int i = 0; i < mData.mAIPositions.Length; i++)
            {
                AIPosition = new Vector3(mData.mAIPositions[i][0], mData.mAIPositions[i][1], mData.mAIPositions[i][2]);
                mAIs[i].mCurrentLocation = AIPosition;
            }

            //Delete all objects marked as removed
            for(int i = 0; i < mData.mDeletedObjects.Length; i++)
            {
                mRemovedObjects.Add(mData.mDeletedObjects[i]);
                Destroy(GameObject.Find(mData.mDeletedObjects[i]));
            }
        }
    }

    //Save the data of the current game
    public void StoreData()
    {
        Transform playerTransform = mPlayer.transform;
        mData.mPlayerPosition = new float[3] { playerTransform.position.x, playerTransform.position.y, playerTransform.position.z };
        mData.mDayTime = (int)mDayCycle.GetTimeOfDay();
        mData.mCyclusTime = mDayCycle.GetCurrentTime();
        mData.mCheckPoint = mCurrentRespawnPoint.name;

        //The positions of all the AIs
        mData.mAIPositions = new float[mAIs.Count][];
        for(int i = 0; i < mAIs.Count; i++)
        {
            mData.mAIPositions[i] = new float[3] { mAIs[i].mCurrentLocation.x, mAIs[i].mCurrentLocation.y, mAIs[i].mCurrentLocation.z };
        }

        mData.mDeletedObjects = new string[mRemovedObjects.Count];
        for(int i = 0; i < mRemovedObjects.Count; i++)
        {
            mData.mDeletedObjects[i] = mRemovedObjects[i];
        }

        SaveGameScript.SaveData();
    }

    //Plays animation to open the stone gate
    private IEnumerator OpenGate()
    {
        yield return new WaitForSeconds(2f);

        if (mInputHandler)
        {
            mInputHandler.StartCutscene((float)mEndClip.duration);
        }

        mEndDirector.Play(mEndClip);
    }

    //Adds the name to the list of objects that have been removed
    public void RemoveObject(string name)
    {
        mRemovedObjects.Add(name);
    }
}
