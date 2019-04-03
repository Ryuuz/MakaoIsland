using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiritOrbScript : MonoBehaviour
{
    [SerializeField]
    private Transform mGoal;

    private NavMeshAgent mAgent;

    void Start()
    {
        mAgent = GetComponent<NavMeshAgent>();
        mGoal = GameObject.Find("OrbGoal").transform;
        if(mGoal)
        {
            mAgent.SetDestination(mGoal.position);
            mAgent.isStopped = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            mAgent.isStopped = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            mAgent.isStopped = true;
        }
    }
}
