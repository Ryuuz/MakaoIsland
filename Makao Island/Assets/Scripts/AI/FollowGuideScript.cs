using UnityEngine;
using UnityEngine.AI;

public class FollowGuideScript : MonoBehaviour
{
    public float mStoppingDistance = 3;

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
        //If the player goes to far away the guiding will be stopped
        if (other.tag == "Player" && mPlayer)
        {
            SetGuideAction(false);
            mAgent.isStopped = true;
            mAgent.ResetPath();
            mPlayer = null;
            mAgent.stoppingDistance = 0;
        }
    }

    //Goal has been reached and the NPC will no longer move
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
        mAgent.stoppingDistance = mStoppingDistance;
        mAgent.SetDestination(pos);
    }

    //Give or take the guide action provided the player is available
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
