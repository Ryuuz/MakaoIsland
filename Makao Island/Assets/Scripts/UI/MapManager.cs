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
        mMapAvailable = mGameManager.mData.mMapStatus;

        //Give the input handler access to this object
        if (InputHandler.InputInstance().mMapManager == null)
        {
            InputHandler.InputInstance().mMapManager = this;
        }

        //Set the correct icons
        UpdateDayIcon((DayCyclus)mGameManager.mData.mDayTime);
        UpdateSpiritAnimal((int)SpiritAnimalType.spiritAnimals);
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
        if(mMapAvailable && mCanvasGroup)
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

    //Updates the icons showing which spirit animals have been found
    public void UpdateSpiritAnimal(int n)
    {
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
        else if(n > mSpiritAnimalStatus.Length)
        {
            mSpiritAnimalStatus[n].color = new Color(1, 1, 1, 0);
        }
    }
}
