using System.Collections;
using UnityEngine;

public class AIWandering : AIController
{
    public float mWanderingRadius = 5f;
    public float mWanderingSpeed = 1.5f;

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
        }
    }

    public override void ChangingSpeed(float speed)
    {
        base.ChangingSpeed(speed);
        mTimeSpeed = speed;
    }

    protected override IEnumerator MoveWhenReady(Vector3 position)
    {
        if (mGameManager.mGameSpeed > 0f)
        {
            yield return new WaitForSeconds(mTransitionDelay / mGameManager.mGameSpeed);
        }

        if(mTimeSpeed > 1f)
        {
            mAgent.speed = 10f;
        }
        else
        {
            mAgent.speed = mSpeed;
        }
        
        mAgent.SetDestination(position);
    }
}
