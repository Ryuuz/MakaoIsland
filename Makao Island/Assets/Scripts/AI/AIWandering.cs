using System.Collections;
using UnityEngine;

public class AIWandering : AIController
{
    public float mWanderingRadius = 5f;
    public float mWanderingSpeed = 1.5f;
    public float mMinWalkDelay = 0f;
    public float mMaxWalkDelay = 1f;

    private float mMoveCountdown;
    private float mTimeSpeed = 1f;

    protected override void Start()
    {
        base.Start();
        mTimeSpeed = mGameManager.mGameSpeed;
        mMoveCountdown = Random.Range(mMinWalkDelay, mMaxWalkDelay);
    }

    void Update()
    {
        mMoveCountdown -= Time.deltaTime;

        //If the NPC isn't already headed somewhere
        if(!mAgent.hasPath && mMoveCountdown <= 0f)
        {
            if (mTimeSpeed > 1f)
            {
                mAgent.speed = 8f;
            }
            else
            {
                mAgent.speed = mWanderingSpeed;
            }

            //Set a random destination in a radius around a set location
            mAgent.SetDestination(mCurrentLocation + (Random.insideUnitSphere * mWanderingRadius));
            mMoveCountdown = Random.Range(mMinWalkDelay, mMaxWalkDelay);
        }
    }

    public override void ChangingSpeed(float speed)
    {
        base.ChangingSpeed(speed);
        mTimeSpeed = speed;
    }

    protected override IEnumerator MoveWhenReady(Vector3 position)
    {
        if(mTimeSpeed > 1f)
        {
            mAgent.speed = 10f;
        }
        else
        {
            mAgent.speed = mSpeed;
        }

        mAgent.SetDestination(position);

        yield return null;
    }
}
