using UnityEngine;
using UnityEngine.AI;

public class FollowGuideScript : MonoBehaviour
{
    public float mStoppingDistance = 3f;

    [HideInInspector]
    public bool mGuided = false;
    [HideInInspector]
    public bool mGoalReached = false;

    private NavMeshAgent mAgent;
    private SpecialActionGuide mGuideAction;
    private PlayerController mPlayer;
    private bool mPlayerPresent = false;

    void Start()
    {
        mAgent = GetComponentInParent<NavMeshAgent>();
        mGuideAction = new SpecialActionGuide(this);
        mPlayer = GameManager.ManagerInstance().mPlayer.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            mPlayerPresent = true;

            if (mGuided)
            {
                SetGuideAction(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //If the player goes too far away the guiding will be stopped
        if (other.tag == "Player")
        {
            mPlayerPresent = false;

            if(mGuided)
            {
                SetGuideAction(false);
                StopAgent();
            }
        }
    }

    private void StopAgent()
    {
        if(mAgent)
        {
            mAgent.isStopped = true;
            mAgent.ResetPath();
            mAgent.stoppingDistance = 0;
        }
    }

    //Goal has been reached and the NPC will no longer move
    public void GoalReached()
    {
        mGuided = false;
        mGoalReached = true;
        SetGuideAction(false);
        StopAgent();
    }

    public void FollowGuide(Vector3 pos)
    {
        mAgent.stoppingDistance = mStoppingDistance;
        mAgent.SetDestination(pos);
    }

    //Give or take the guide action provided the player is available
    public void SetGuideAction(bool give)
    {
        if(mPlayer && mPlayerPresent)
        {
            if(give)
            {
                mPlayer.mSpecialAction = mGuideAction;
                GameManager.ManagerInstance().mControlUI.ShowControlUI(ControlAction.guide);
            }
            else if(!give && mPlayer.mSpecialAction == mGuideAction)
            {
                mPlayer.mSpecialAction = null;
                GameManager.ManagerInstance().mControlUI.HideControlUI();
            }
        }
    }
}
