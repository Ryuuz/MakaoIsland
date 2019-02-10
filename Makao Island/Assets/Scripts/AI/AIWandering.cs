using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        if(!mAgent.hasPath)
        {
            mAgent.speed = mWanderingSpeed * mTimeSpeed;
            mDestination = mCurrentLocation + Random.insideUnitSphere* mWanderingRadius;
            mAgent.SetDestination(mDestination);
        }
    }

    public override void ChangingSpeed(float speed)
    {
        mTimeSpeed = speed;
    }

    protected override IEnumerator MoveWhenReady(Vector3 position)
    {
        yield return new WaitForSeconds(mTransitionDelay / mGameManager.mGameSpeed);
        mAgent.speed = mSpeed * mTimeSpeed;
        mAgent.SetDestination(position);
    }
}
