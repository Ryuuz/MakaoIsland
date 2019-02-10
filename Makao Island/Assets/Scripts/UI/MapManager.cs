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

    private void Start()
    {
        GameManager tempManager = GameManager.ManagerInstance();

        tempManager.eTimeChanged.AddListener(UpdateDayIcon);
        mMapAvailable = tempManager.mProgress.mMapStatus;
        int tempDayIcon = (int)tempManager.mGameStatus.mDayTime;

        if (InputHandler.InputInstance().mMapManager == null)
        {
            InputHandler.InputInstance().mMapManager = this;
        }
        
        if(tempDayIcon < mDayCycleIcons.Length)
        {
            mDayTime.overrideSprite = mDayCycleIcons[tempDayIcon];
        }

        HideMap();
    }

    private void Update()
    {
        if(gameObject.activeSelf && mPlayerOnMap)
        {
            mPlayerOnMap.UpdatePlayerPosition();
        }
    }

    public void HideMap()
    {
        gameObject.SetActive(false);
    }

    public void ShowMap()
    {
        if(mMapAvailable)
        {
            gameObject.SetActive(true);
        }
    }

    public void UpdateDayIcon(DayCyclus cycle)
    {
        if((int)cycle < mDayCycleIcons.Length && mDayTime)
        {
            mDayTime.overrideSprite = mDayCycleIcons[(int)cycle];
        }
    }
}
