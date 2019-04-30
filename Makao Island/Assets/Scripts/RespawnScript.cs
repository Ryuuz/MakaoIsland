using UnityEngine;
using UnityEngine.Playables;

public class RespawnScript : MonoBehaviour
{
    public float mLowestPoint = -10f;

    private Transform mPosition;
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
        //Respawn when below the lowest point
        if(mPosition.position.y < mLowestPoint)
        {
            if (mDirector)
            {
                mDirector.Play(mClip);
            }
            mPosition.position = GameManager.ManagerInstance().mCurrentRespawnPoint.position;
        }
    }
}
