using UnityEngine;
using UnityEngine.Playables;

public class RespawnScript : MonoBehaviour
{
    public float mLowestPoint = -10f;

    private Transform mPosition;
    private bool mRespawning = false;
    private PlayableDirector mDirector;
    private PlayableAsset mClip;

    void Start()
    {
        mPosition = GetComponent<Transform>();
        mDirector = GameObject.Find("BlackoutTimeline").GetComponent<PlayableDirector>();

        if(mDirector)
        {
            mClip = mDirector.playableAsset;
        }
    }

    void Update()
    {
        //Start playing the blackout before reaching the lowest point
        if(mPosition.position.y < (mLowestPoint + 5f) && !mRespawning)
        {
            mRespawning = true;

            if(mDirector)
            {
                mDirector.Play(mClip);
            }
        }
        //Respawn
        if(mPosition.position.y < mLowestPoint)
        {
            mPosition.position = GameManager.ManagerInstance().mCurrentRespawnPoint.position;
            mRespawning = false;
        }
    }
}
