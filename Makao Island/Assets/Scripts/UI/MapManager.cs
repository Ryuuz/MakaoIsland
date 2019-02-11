using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public Sprite[] mDayCycleIcons;
    public bool mMapAvailable { get; set; }

    [SerializeField]
    private TrackPlayer mPlayerOnMap;
    [SerializeField]
    private Image[] mSpiritAnimalStatus;
    [SerializeField]
    private Image mDayTime;

    private GameManager mGameManager;
    private CanvasGroup mCanvasGroup;

    private void Start()
    {
        mCanvasGroup = GetComponent<CanvasGroup>();
        mGameManager = GameManager.ManagerInstance();
        mGameManager.eTimeChanged.AddListener(UpdateDayIcon);
        mGameManager.eSpiritAnimalFound.AddListener(UpdateSpiritAnimal);
        mMapAvailable = mGameManager.mProgress.mMapStatus;

        //Give the input handler access to this object
        if (InputHandler.InputInstance().mMapManager == null)
        {
            InputHandler.InputInstance().mMapManager = this;
        }

        UpdateDayIcon(mGameManager.mGameStatus.mDayTime);
        UpdateSpiritAnimal();
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
        mCanvasGroup.alpha = 0f;
        mCanvasGroup.blocksRaycasts = false;
        mCanvasGroup.interactable = false;
    }

    public void ShowMap()
    {
        //Can only open the map if it is available to the player
        if(mMapAvailable)
        {
            mCanvasGroup.alpha = 1f;
            mCanvasGroup.blocksRaycasts = false;
            mCanvasGroup.interactable = false;
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

    //Updates the icons showing which spirit animals have been found
    public void UpdateSpiritAnimal()
    {
        if(mSpiritAnimalStatus.Length <= mGameManager.mProgress.mSpiritAnimalsStatus.Length)
        {
            for (int i = 0; i < mSpiritAnimalStatus.Length; i++)
            {
                if (mGameManager.mProgress.mSpiritAnimalsStatus[i])
                {
                    mSpiritAnimalStatus[i].color = Color.white;
                }
            }
        }
    }
}
