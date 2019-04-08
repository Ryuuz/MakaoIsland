using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalAIScript : MonoBehaviour
{
    public float mGrazingRadius = 4f;
    private NavMeshAgent mAgent;
    private float mMoveCountdown;

    void Start()
    {
        mAgent = GetComponent<NavMeshAgent>();
        mMoveCountdown = Random.Range(8f, 20f);
    }

    void Update()
    {
        mMoveCountdown -= Time.deltaTime;

        if(mMoveCountdown <= 0f)
        {
            //if(Random.value > 0.5f)
            //{
                mAgent.SetDestination(transform.position + Random.insideUnitSphere * mGrazingRadius);
            //}

            mMoveCountdown = Random.Range(8f, 20f);
        }
    }

    //Function to move animal x units in the direction of a vector
}
