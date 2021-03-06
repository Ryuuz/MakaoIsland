﻿using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SpiritOrbScript : MonoBehaviour
{
    [SerializeField]
    private GameObject mMapTutorial;

    private Transform mGoal;
    private NavMeshAgent mAgent;
    private bool mGoalReached = false;
    private CharacterController mPlayerController;

    void Start()
    {
        mAgent = GetComponent<NavMeshAgent>();
        mPlayerController = GameManager.ManagerInstance().mPlayer.GetComponent<CharacterController>();
        mGoal = GameObject.Find("OrbGoal").transform;
        if(mGoal)
        {
            mAgent.SetDestination(mGoal.position);
            mAgent.isStopped = true;
        }
    }

    //Start moving when the player is in range
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            mAgent.isStopped = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            //Adjust the speed to stay ahead of the player
            float playerSpeed = mPlayerController.velocity.magnitude;
            mAgent.speed = (playerSpeed > 4f) ? (playerSpeed + 1f) : mAgent.speed;

            //Disappear when the goal has been reached
            if(!mAgent.hasPath && !mAgent.pathPending && !mGoalReached)
            {
                mGoalReached = true;
                StartCoroutine(Vanish());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            mAgent.isStopped = true;
        }
    }

    private IEnumerator Vanish()
    {
        yield return new WaitForSeconds(3f);
        GetComponentInChildren<ParticleSystem>().Stop();

        //Fade out sound if there is one
        FadeOutSound soundTemp = GetComponent<FadeOutSound>();
        if(soundTemp)
        {
            soundTemp.StartFadingSound();
        }

        GameManager.ManagerInstance().UpdateSpiritAnimals((int)SpiritAnimalType.life);
        yield return new WaitForSeconds(4f);

        //Give the map to the player and spawn a tutorial sphere for it
        GameManager.ManagerInstance().mData.mMapStatus = true;
        if (mMapTutorial)
        {
            Instantiate(mMapTutorial, GameManager.ManagerInstance().mPlayer.transform.position, Quaternion.identity);
        }
        GameManager.ManagerInstance().mPlayer.GetComponent<AudioSource>().Play();

        Destroy(gameObject);
    }
}
