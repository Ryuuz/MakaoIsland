using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public bool mMapAvailable { get; set; }

    [SerializeField]
    private TrackPlayer mPlayerOnMap;

    private void Start()
    {
        mMapAvailable = true;
        HideMap();
        GameManager.ManagerInstance().eTimeChanged.AddListener(UpdateSidePanel);

        if(InputHandler.InputInstance().mMapManager == null)
        {
            InputHandler.InputInstance().mMapManager = this;
        }
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

    }
}
