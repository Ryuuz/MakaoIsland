using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowGuideScript : MonoBehaviour
{
    [HideInInspector]
    public bool mGuided = false;
    [HideInInspector]
    public bool mGoalReached = false;

    private NavMeshAgent mAgent;
    private PlayerController mPlayer;
    private SpecialActionGuide mGuideAction;

    void Start()
    {
        mAgent = GetComponentInParent<NavMeshAgent>();
        mGuideAction = new SpecialActionGuide(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            mPlayer = other.GetComponent<PlayerController>();

            if (mGuided)
            {
                SetGuideAction(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && mPlayer)
        {
            SetGuideAction(false);
            mAgent.isStopped = true;
            mAgent.ResetPath();
            mPlayer = null;
            mAgent.stoppingDistance = 0;
        }
    }

    public void GoalReached()
    {
        mGuided = false;
        mGoalReached = true;
        SetGuideAction(false);
        mAgent.isStopped = true;
        mAgent.ResetPath();
    }

    public void FollowGuide(Vector3 pos)
    {
        mAgent.stoppingDistance = 4;
        mAgent.SetDestination(pos);
    }

    public void SetGuideAction(bool give)
    {
        if(mPlayer)
        {
            if (give)
            {
                mPlayer.mSpecialAction = mGuideAction;
                GameManager.ManagerInstance().mControlUI.ShowControlUI(ControlAction.guide);
            }
            else
            {
                if (mPlayer.mSpecialAction == mGuideAction)
                {
                    mPlayer.mSpecialAction = null;
                    GameManager.ManagerInstance().mControlUI.HideControlUI();
                }
            }
        }
    }
}
