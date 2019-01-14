using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public float mSpeed = 3.5f;
    public Transform mDawnLocation;
    public Transform mDayLocation;
    public Transform mDuskLocation;
    public Transform mNightLocation;

    private NavMeshAgent mAgent;

    // Start is called before the first frame update
    void Start()
    {
        mAgent = GetComponent<NavMeshAgent>();
        mAgent.speed = mSpeed;
        transform.position = mDawnLocation.position;

        GameManager.ManagerInstance().eSpeedChanged.AddListener(ChangingSpeed);
        GameManager.ManagerInstance().eTimeChanged.AddListener(Transition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Transition(DayCyclus time)
    {
        Vector3 pos = transform.position;

        switch (time)
        {
            case DayCyclus.dawn:
                pos = mDawnLocation.position;
                break;

            case DayCyclus.day:
                pos = mDayLocation.position;
                break;

            case DayCyclus.dusk:
                pos = mDuskLocation.position;
                break;

            case DayCyclus.night:
                pos = mNightLocation.position;
                break;
        }

        mAgent.SetDestination(pos);
    }

    public void ChangingSpeed(float speed)
    {
        mAgent.speed = mSpeed * speed;
    }
}
