using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public float mSpeed = 3.5f;
    public float mWaypointRadius = 4f;
    public float mTransitionDelay = 2f;

    [HideInInspector]
    public bool mTalking = false;

    [SerializeField]
    private Transform mDawnLocation;
    [SerializeField]
    private Transform mDayLocation;
    [SerializeField]
    private Transform mDuskLocation;
    [SerializeField]
    private Transform mNightLocation;
    
    private NavMeshAgent mAgent;

    // Start is called before the first frame update
    void Start()
    {
        mAgent = GetComponent<NavMeshAgent>();
        mAgent.speed = mSpeed;

        GameManager.ManagerInstance().eSpeedChanged.AddListener(ChangingSpeed);
        GameManager.ManagerInstance().eTimeChanged.AddListener(Transition);
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
            Vector3 destination = pos.position + Random.insideUnitSphere * mWaypointRadius;
            destination.y = pos.position.y;

            StartCoroutine("MoveWhenReady", destination);
        }
        
    }

    public void ChangingSpeed(float speed)
    {
        mAgent.speed = mSpeed * speed;
    }

    private IEnumerator MoveWhenReady(Vector3 position)
    {
        if (mTalking)
        {
            yield return new WaitUntil(() => mTalking == false);
        }
        else
        {
            yield return new WaitForSeconds(mTransitionDelay);
        }

        mAgent.SetDestination(position);
    }
}
