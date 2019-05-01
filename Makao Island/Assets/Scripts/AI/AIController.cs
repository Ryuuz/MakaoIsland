using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public float mSpeed = 3.5f;
    public float mAcceleration = 8f;
    public float mWaypointRadius = 4f;
    public float mTransitionDelay = 2f;

    [HideInInspector]
    public Vector3 mCurrentLocation = Vector3.zero;

    [SerializeField]
    protected Transform mDawnLocation;
    [SerializeField]
    protected Transform mDayLocation;
    [SerializeField]
    protected Transform mDuskLocation;
    [SerializeField]
    protected Transform mNightLocation;

    protected NavMeshAgent mAgent;
    protected GameManager mGameManager;
    protected Transform mTransform;

    protected virtual void Start()
    {
        mAgent = GetComponent<NavMeshAgent>();
        if(mAgent)
        {
            mAgent.speed = mSpeed;
        }
        
        mGameManager = GameManager.ManagerInstance();
        mTransform = GetComponent<Transform>();

        mGameManager.eSpeedChanged.AddListener(ChangingSpeed);
        mGameManager.eTimeChanged.AddListener(Transition);

        if(PlayerPrefs.GetInt("Load", 0) == 1 && mCurrentLocation != Vector3.zero)
        {
            mAgent.Warp(mCurrentLocation);
        }

        Transition((DayCyclus)mGameManager.mData.mDayTime);
    }

    //Transition to a new position (if required) when the time of day changes
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

        SetNewDestination(pos);
    }

    //Set the agent's destination based on the given position
    protected virtual void SetNewDestination(Transform position)
    {
        if (position)
        {
            mCurrentLocation = position.position;

            //Take the position of the waypoint and set a destination in a radius near it
            Vector3 tempPosition = position.position;
            tempPosition += (Random.insideUnitSphere * mWaypointRadius);
            tempPosition.y = position.position.y;

            StartCoroutine(MoveWhenReady(tempPosition));
        }
        else if(mCurrentLocation == Vector3.zero)
        {
            mCurrentLocation = mTransform.position;
        }
    }

    //Change the movement speed of the agent
    public virtual void ChangingSpeed(float speed)
    {
        if(mAgent)
        {
            //Speeding up
            if(speed > 1f)
            {
                mAgent.speed = 10f;
                mAgent.acceleration = 60f; //https://answers.unity.com/questions/236828/how-can-i-stop-navmesh-agent-sliding.html
            }
            //Normal speed
            else
            {
                mAgent.speed = mSpeed;
                mAgent.acceleration = mAcceleration;
            }
        }
    }

    //Have the NPC turn and look at the given position
    public IEnumerator LookAtObject(Vector3 obj)
    {
        if(mTransform.up == Vector3.up)
        {
            float rotationTime = 0f;

            //The desired rotation
            Quaternion startRotation = mTransform.rotation;
            Quaternion endRotation = new Quaternion();
            endRotation.eulerAngles = new Vector3(startRotation.eulerAngles.x, Quaternion.LookRotation(obj - mTransform.position).eulerAngles.y, startRotation.eulerAngles.z);

            while (Quaternion.Angle(mTransform.rotation, endRotation) > 1f)
            {
                rotationTime += (Time.deltaTime * mGameManager.mGameSpeed);
                mTransform.rotation = Quaternion.Slerp(startRotation, endRotation, rotationTime);
                yield return null;
            }
        }
    }

    //The agent will be assigned a destination as soon as it is ready to move
    protected virtual IEnumerator MoveWhenReady(Vector3 position)
    {
        if(mGameManager.mGameSpeed > 0f)
        {
            yield return new WaitForSeconds(mTransitionDelay / mGameManager.mGameSpeed);
        }
        
        if(mAgent)
        {
            mAgent.SetDestination(position);
        }
    }
}
