using UnityEngine;
using UnityEngine.AI;

public class VillagerAnimationScript : AIAnimationScript
{
    protected NavMeshAgent mAgent;
    protected Transform mTransform;
    protected Vector3 mPreviousPosition;
    protected float mCurrentTime;
    protected bool mSwitchIdle = false;

    protected override void Start()
    {
        base.Start();
        mAgent = GetComponentInParent<NavMeshAgent>();
        mTransform = GetComponentInParent<Transform>();
        mPreviousPosition = mTransform.position;
        mCurrentTime = Random.Range(5f, 12f);
        mAnimator.Play("idle_normal", 0, Random.value);
    }

    void Update()
    {
        mCurrentTime -= Time.deltaTime;
        if(mCurrentTime <= 0f)
        {
            mSwitchIdle = true;
            mCurrentTime = Random.Range(5f, 12f);
        }

        Vector3 positionOffset = mTransform.position - mPreviousPosition;
        mPreviousPosition = mTransform.position;

        if(positionOffset != Vector3.zero && positionOffset.sqrMagnitude > (0.01f * 0.01f))
        {
            if(!mAnimator.GetBool("Walking"))
            {
                mAnimator.SetBool("Walking", true);
            }
            
            mAnimator.SetFloat("WalkSpeed", Mathf.Max(mAgent.velocity.magnitude / mAgent.speed, 0.5f));
        }
        else
        {
            if(mAnimator.GetBool("Walking"))
            {
                mAnimator.SetBool("Walking", false);
            }

            if(mAgent.remainingDistance > 0f && mAgent.remainingDistance < 2f)
            {
                mAgent.isStopped = true;
                mAgent.ResetPath();
            }

            if(mAnimator.GetCurrentAnimatorStateInfo(0).IsName("idle_normal") && mSwitchIdle)
            {
                mSwitchIdle = false;
                if(Random.value < 0.4f)
                {
                    if(Random.value < 0.5f)
                    {
                        mAnimator.SetTrigger("Lean");
                    }
                    else
                    {
                        mAnimator.SetTrigger("Turn");
                    }
                }
            }
            else if(!mAnimator.GetCurrentAnimatorStateInfo(0).IsName("idle_normal"))
            {
                mAnimator.ResetTrigger("Lean");
                mAnimator.ResetTrigger("Turn");
            }
        }
    }

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
                mAnimator.CrossFadeInFixedTime(stateName, 2f);
            }
        }
    }
}
