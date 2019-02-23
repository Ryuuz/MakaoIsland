using UnityEngine;

public class GoalScript : MonoBehaviour
{
    [SerializeField]
    private GameObject mTarget;
    [SerializeField]
    private DialogueTrigger mTriggerDialogue;

    private void OnTriggerEnter(Collider other)
    {
        //If the assigned object entered the trigger
        if (other.gameObject == mTarget)
        {
            other.GetComponentInChildren<FollowGuideScript>().GoalReached();

            //If a dialogue sphere is connected with the goal then check if it can be played
            if (mTriggerDialogue)
            {
                mTriggerDialogue.EvaluateStatus();
            }

            //All the goal sphere's tasks have been completed. No longer needed
            Destroy(gameObject);
        }
    }
}
