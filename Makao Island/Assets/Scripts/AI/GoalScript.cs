using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    [SerializeField]
    private GameObject mTarget;
    [SerializeField]
    private DialogueTrigger mTriggerDialogue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == mTarget)
        {
            other.GetComponentInChildren<FollowGuideScript>().GoalReached();
            if(mTriggerDialogue)
            {
                mTriggerDialogue.EvaluateStatus();
            }
            Destroy(gameObject);
        }
    }
}
