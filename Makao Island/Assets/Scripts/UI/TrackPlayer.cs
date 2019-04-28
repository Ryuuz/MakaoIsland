using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    public Transform mUpperRight;
    public Transform mLowerLeft;

    [SerializeField]
    private RectTransform mPlayerOnMap;

    private Transform mPlayer;
    private Vector2 mScale = new Vector2(1f, 1f); //Scale in x and y direction
    
    void Start()
    {
        mPlayer = GameManager.ManagerInstance().mPlayer.transform;
        RectTransform mapImage = GetComponent<RectTransform>();

        //Use the points in the game world and the size of the map to find the scale
        if(mUpperRight && mLowerLeft)
        {
            Vector2 UISize = new Vector2(mapImage.rect.width, mapImage.rect.height);
            Vector2 mapSize = new Vector2(mUpperRight.position.x - mLowerLeft.position.x, mUpperRight.position.z - mLowerLeft.position.z);

            mScale = new Vector2(UISize.x / mapSize.x, UISize.y / mapSize.y);

            //Set the anchor to the image at where the origin of the game world is
            mPlayerOnMap.anchorMax = new Vector2(((0f - mLowerLeft.position.x) * mScale.x) / UISize.x, ((0f - mLowerLeft.position.z) * mScale.y) / UISize.y);
            mPlayerOnMap.anchorMin = mPlayerOnMap.anchorMax;
        }
    }

    public void UpdatePlayerPosition()
    {
        //Update the position and rotation of the player icon on the map as the player moves
        mPlayerOnMap.anchoredPosition = new Vector2((mPlayer.position.x * mScale.x), (mPlayer.position.z * mScale.y));
        mPlayerOnMap.rotation = Quaternion.Euler(0f, 0f, 180f - mPlayer.rotation.eulerAngles.y);
    }
}
