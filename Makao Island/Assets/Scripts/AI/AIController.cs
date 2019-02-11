using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public float mSpeed = 3.5f;
    public float mWaypointRadius = 4f;
    public float mTransitionDelay = 2f;

    protected Vector3 mCurrentLocation;
    protected NavMeshAgent mAgent;
    protected GameManager mGameManager;

    [SerializeField]
    protected Transform mDawnLocation;
    [SerializeField]
    protected Transform mDayLocation;
    [SerializeField]
    protected Transform mDuskLocation;
    [SerializeField]
    protected Transform mNightLocation; 

    protected virtual void Start()
    {
        mAgent = GetComponent<NavMeshAgent>();
        mAgent.speed = mSpeed;
        mGameManager = GameManager.ManagerInstance();
        mCurrentLocation = transform.position;

        mGameManager.eSpeedChanged.AddListener(ChangingSpeed);
        mGameManager.eTimeChanged.AddListener(Transition);
    }

    public virtual void Transition(DayCyclus time)
    {
        Transform pos = null;

        switch (time)
        {
            case DayCyclus.dawn:
                pos = mDawnLocation;
                break;

            case DayCyclus.day:
                pos = mDayLocation;
                break;

            case DayCyclus.dusk:
                pos = mDuskLocation;
                break;

            case DayCyclus.night:
                pos = mNightLocation;
                break;
        }

        if(pos)
        {
            //Take the position of the waypoint and set a destination in a radius near it
            mCurrentLocation = pos.position;
            if(mWaypointRadius > 0f)
            {
                mCurrentLocation += Random.insideUnitSphere * mWaypointRadius;
            }
            mCurrentLocation.y = pos.position.y;

            StartCoroutine(MoveWhenReady(mCurrentLocation));
        }
    }

    //Change the movement speed of the agent
    public virtual void ChangingSpeed(float speed)
    {
        mAgent.speed = mSpeed * speed;
    }

    //Have the NPC turn and look at the given position
    public IEnumerator LookAtObject(Vector3 obj)
    {
        float rotationTime = 0f;
        //The desired rotation
        Quaternion endRotation = Quaternion.LookRotation(obj - transform.position, Vector3.up);
        Quaternion startRotation = transform.rotation;

        while (Quaternion.Angle(transform.rotation, endRotation) > 2f)
        {
            rotationTime += Time.deltaTime * mGameManager.mGameSpeed;
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, rotationTime);
            yield return null;
        }
    }

    //The agent will be assigned a destination as soon as it is ready to move
    protected virtual IEnumerator MoveWhenReady(Vector3 position)
    {
        if(mGameManager.mGameSpeed > 0f)
        {
            yield return new WaitForSeconds(mTransitionDelay / mGameManager.mGameSpeed);
        }
        
        mAgent.SetDestination(position);
    }
}
