using System.Collections;
using UnityEngine;

public class AIWandering : AIController
{
    public float mWanderingRadius = 5f;
    public float mWanderingSpeed = 1.5f;

    private Vector3 mDestination;
    private float mTimeSpeed;

    protected override void Start()
    {
        base.Start();
        mTimeSpeed = mGameManager.mGameSpeed;
    }

    void Update()
    {
        //If the NPC isn't already headed somewhere
        if(!mAgent.hasPath)
        {
            mAgent.speed = mWanderingSpeed * mTimeSpeed;

            //Set a random destination in a radius around a set location
            mDestination = mCurrentLocation + Random.insideUnitSphere* mWanderingRadius;
            mAgent.SetDestination(mDestination);
        }
    }

    public override void ChangingSpeed(float speed)
    {
        mTimeSpeed = speed;
        mAgent.speed = mSpeed * mTimeSpeed;
    }

    protected override IEnumerator MoveWhenReady(Vector3 position)
    {
        if (mGameManager.mGameSpeed > 0f)
        {
            yield return new WaitForSeconds(mTransitionDelay / mGameManager.mGameSpeed);
        }

        mAgent.speed = mSpeed * mTimeSpeed;
        mAgent.SetDestination(position);
    }
}
