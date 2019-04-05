using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
            float playerSpeed = mPlayerController.velocity.magnitude;
            mAgent.speed = (playerSpeed > 4f) ? (playerSpeed + 1f) : mAgent.speed;

            //Debug.Log(mAgent.hasPath);
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
        yield return new WaitForSeconds(5f);

        GameManager.ManagerInstance().UpdateSpiritAnimals((int)SpiritAnimalType.life);
        InputHandler.InputInstance().mMapManager.mMapAvailable = true;
        if (mMapTutorial)
        {
            GameObject temp = Instantiate(mMapTutorial, transform.position, Quaternion.identity);
            temp.GetComponent<ControlTutorial>().mAction = ControlAction.map;
        }

        Destroy(gameObject);
    }
}
