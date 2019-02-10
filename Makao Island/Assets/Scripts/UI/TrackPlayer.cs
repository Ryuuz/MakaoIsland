using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    public Transform mUpperRight;
    public Transform mLowerLeft;

    [SerializeField]
    private RectTransform mPlayerOnMap;

    private Transform mPlayer;
    private Vector2 mScale = new Vector2(1f, 1f);
    
    void Start()
    {
        mPlayer = GameManager.ManagerInstance().mPlayer.transform;
        RectTransform mapImage = GetComponent<RectTransform>();

        if(mUpperRight && mLowerLeft)
        {
            Vector2 UISize = new Vector2(mapImage.rect.width, mapImage.rect.height);
            Vector2 mapSize = new Vector2(mUpperRight.position.x - mLowerLeft.position.x, mUpperRight.position.z - mLowerLeft.position.z);

            mScale = new Vector2(UISize.x / mapSize.x, UISize.y / mapSize.y);
        }
    }

    public void UpdatePlayerPosition()
    {
        if(mPlayer)
        {
            mPlayerOnMap.anchoredPosition = new Vector2((mPlayer.position.x * mScale.x), (mPlayer.position.z * mScale.y));
        }
    }
}
