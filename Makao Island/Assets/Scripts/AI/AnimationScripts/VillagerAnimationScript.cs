using UnityEngine;
using UnityEngine.AI;

public class VillagerAnimationScript : AIAnimationScript
{
    protected override void Start()
    {
        base.Start();
        mAgent = GetComponentInParent<NavMeshAgent>();
        mTransform = GetComponentInParent<Transform>();
        mPreviousPosition = mTransform.position;
    }

    //Plays the animation associated with 'gesture'
    public void PlayTalkAnimation(TalkAnimation gesture)
    {
        if(!mAnimator.GetBool("Walking"))
        {
            string stateName;

            switch(gesture)
            {
                case TalkAnimation.nod: stateName = "conversation_nod";
                    break;
                case TalkAnimation.hands: stateName = "conversation_handGestures";
                    break;
                case TalkAnimation.cry: stateName = "conversation_cry";
                    break;
                default: stateName = "none";
                    break;
            }

            if(stateName != "none")
            {
                mAnimator.CrossFade(stateName, 0.2f);
            }
        }
    }
}
