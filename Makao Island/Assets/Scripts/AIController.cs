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
    private Transform mDawnLocation;
    [SerializeField]
    private Transform mDayLocation;
    [SerializeField]
    private Transform mDuskLocation;
    [SerializeField]
    private Transform mNightLocation; 

    protected virtual void Start()
    {
        mAgent = GetComponent<NavMeshAgent>();
        mAgent.speed = mSpeed;
        mGameManager = GameManager.ManagerInstance();
        mCurrentLocation = transform.position;

        mGameManager.eSpeedChanged.AddListener(ChangingSpeed);
        mGameManager.eTimeChanged.AddListener(Transition);
    }

    public void Transition(DayCyclus time)
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
            mCurrentLocation = pos.position + Random.insideUnitSphere * mWaypointRadius;
            mCurrentLocation.y = pos.position.y;

            StartCoroutine(MoveWhenReady(mCurrentLocation));
        }
        
    }

    public virtual void ChangingSpeed(float speed)
    {
        mAgent.speed = mSpeed * speed;
    }

    public IEnumerator LookAtObject(Vector3 obj)
    {
        Quaternion endRotation = Quaternion.LookRotation(obj - transform.position, Vector3.up);

        while (Quaternion.Angle(transform.rotation, endRotation) > 2f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, endRotation, Time.deltaTime * mGameManager.mGameSpeed);
            yield return null;
        }
    }

    protected virtual IEnumerator MoveWhenReady(Vector3 position)
    {
        yield return new WaitForSeconds(mTransitionDelay/mGameManager.mGameSpeed);
        mAgent.SetDestination(position);
    }
}
