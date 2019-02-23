using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    public float mLowestPoint = -10f;

    [SerializeField]
    private Transform mRespawnPoint;

    private Transform mPosition;

    void Start()
    {
        mPosition = GetComponent<Transform>();
    }

    void Update()
    {
        if(mPosition.position.y < mLowestPoint)
        {
            mPosition.position = mRespawnPoint.position;
        }
    }
}
