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
        //Respawn
        if(mPosition.position.y < mLowestPoint)
        {
            if (mDirector)
            {
                Debug.Log("playing black out");
                mDirector.Play(mClip);
            }
            mPosition.position = GameManager.ManagerInstance().mCurrentRespawnPoint.position;
        }
    }
}
