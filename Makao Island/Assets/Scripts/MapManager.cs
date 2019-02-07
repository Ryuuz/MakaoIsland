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
        mMapAvailable = true;
        HideMap();
        GameManager.ManagerInstance().eTimeChanged.AddListener(UpdateSidePanel);

        if(InputHandler.InputInstance().mMapManager == null)
        {
            InputHandler.InputInstance().mMapManager = this;
        }

        mDayTime.overrideSprite = mDayCycleIcons[(int)DayCyclus.dawn];
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

    public void UpdateSidePanel(DayCyclus cycle)
    {
        if((int)cycle < mDayCycleIcons.Length && mDayTime)
        {
            mDayTime.overrideSprite = mDayCycleIcons[(int)cycle];
        }
    }
}
