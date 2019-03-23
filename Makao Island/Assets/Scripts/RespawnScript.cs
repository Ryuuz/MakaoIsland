using UnityEngine;
using UnityEngine.Playables;

public class RespawnScript : MonoBehaviour
{
    public float mLowestPoint = -10f;

    [SerializeField]
    private Transform mStartSpawnPoint;

    private Transform mPosition;
    private bool mRespawning = false;
    private PlayableDirector mDirector;
    private PlayableAsset mClip;

    void Start()
    {
        mPosition = GetComponent<Transform>();
        mDirector = GameObject.Find("BlackoutTimeline").GetComponent<PlayableDirector>();
        mClip = mDirector.playableAsset;
    }

    void Update()
    {
        if (mPosition.position.y < (mLowestPoint + 5f) && !mRespawning)
        {
            mRespawning = true;
            mDirector.Play(mClip);
        }
        if (mPosition.position.y < mLowestPoint)
        {
            if (GameManager.ManagerInstance().mCurrentRespawnPoint)
            {
                mPosition.position = GameManager.ManagerInstance().mCurrentRespawnPoint.position;
            }
            else
            {
                mPosition.position = mStartSpawnPoint.position;
            }
            mRespawning = false;
        }
    }
}
