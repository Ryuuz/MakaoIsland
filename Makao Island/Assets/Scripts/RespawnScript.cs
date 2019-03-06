using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    public float mLowestPoint = -10f;

    private Transform mPosition;

    void Start()
    {
        mPosition = GetComponent<Transform>();
    }

    void Update()
    {
        if (mPosition.position.y < mLowestPoint)
        {
            mPosition.position = GameManager.ManagerInstance().mCurrentRespawnPoint;
        }
    }
}
