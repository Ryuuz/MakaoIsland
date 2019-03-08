using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    public float mLowestPoint = -10f;

    [SerializeField]
    private Transform mStartSpawnPoint;

    private Transform mPosition;

    void Start()
    {
        mPosition = GetComponent<Transform>();
    }

    void Update()
    {
        if (mPosition.position.y < mLowestPoint)
        {
            if(GameManager.ManagerInstance().mCurrentRespawnPoint)
            {
                mPosition.position = GameManager.ManagerInstance().mCurrentRespawnPoint.position;
            }
            else
            {
                mPosition.position = mStartSpawnPoint.position;
            }
        }
    }
}
