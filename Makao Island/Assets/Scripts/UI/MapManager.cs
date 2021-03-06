﻿using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class MapManager : MonoBehaviour
{
    public Sprite[] mDayCycleIcons;

    [SerializeField]
    private TrackPlayer mPlayerOnMap;
    [SerializeField]
    private Image[] mSpiritAnimalStatus;
    [SerializeField]
    private Image mDayTime;
    [SerializeField]
    private Image mSpiritGirl;

    private GameManager mGameManager;
    private CanvasGroup mCanvasGroup;

    private void Start()
    {
        mCanvasGroup = GetComponent<CanvasGroup>();
        mGameManager = GameManager.ManagerInstance();

        //Listens for these events
        mGameManager.eTimeChanged.AddListener(UpdateDayIcon);
        mGameManager.eSpiritAnimalFound.AddListener(UpdateSpiritAnimal);
        mGameManager.eSpiritGirlFound.AddListener(UpdateSpiritGirl);

        //Give the input handler access to this object
        if (InputHandler.InputInstance().mMapManager == null)
        {
            InputHandler.InputInstance().mMapManager = this;
        }

        //Set the correct icons
        UpdateDayIcon((DayCyclus)mGameManager.mData.mDayTime);
        UpdateSpiritAnimal((int)SpiritAnimalType.spiritAnimals);
        UpdateSpiritGirl();
        HideMap();
    }

    private void Update()
    {
        //Keep the position of the player on the map up to date
        if(mPlayerOnMap)
        {
            mPlayerOnMap.UpdatePlayerPosition();
        }
    }

    public void HideMap()
    {
        if(mCanvasGroup)
        {
            mCanvasGroup.alpha = 0f;
            mCanvasGroup.blocksRaycasts = false;
            mCanvasGroup.interactable = false;
        }
    }

    public void ShowMap()
    {
        //Can only open the map if it is available to the player
        if(mCanvasGroup && mGameManager.mData.mMapStatus)
        {
            mCanvasGroup.alpha = 1f;
            mCanvasGroup.blocksRaycasts = true;
            mCanvasGroup.interactable = true;
        }
    }

    //Updates the icon that shows what time of the day it currently is
    public void UpdateDayIcon(DayCyclus cycle)
    {
        if((int)cycle < mDayCycleIcons.Length && mDayTime)
        {
            mDayTime.overrideSprite = mDayCycleIcons[(int)cycle];
        }
    }

    public void UpdateSpiritGirl()
    {
        if(mGameManager.mData.mSpiritGirlStatus)
        {
            mSpiritGirl.color = new Color(1, 1, 1, 0);
        }
    }

    //Updates the icons showing which spirit animals have been found
    public void UpdateSpiritAnimal(int n)
    {
        //Goes through and checks all the spirit animals
        if(n == mSpiritAnimalStatus.Length)
        {
            for (int i = 0; i < mSpiritAnimalStatus.Length; i++)
            {
                if (mGameManager.mData.mSpiritAnimalsStatus[i])
                {
                    mSpiritAnimalStatus[i].color = new Color(1, 1, 1, 0);
                }
            }
        }
        //Only sets the given animal
        else if(n < mSpiritAnimalStatus.Length)
        {
            mSpiritAnimalStatus[n].color = new Color(1, 1, 1, 0);
        }
    }
}
