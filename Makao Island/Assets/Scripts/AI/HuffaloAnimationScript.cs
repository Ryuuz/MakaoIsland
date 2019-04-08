using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HuffaloAnimationScript : AIAnimationScript
{
    protected float mCurrentTime;
    protected Transform mTransform;
    protected Vector3 mPreviousPosition;
    protected NavMeshAgent mAgent;

    protected override void Start()
    {
        base.Start();
        mCurrentTime = Random.Range(5f, 12f);
        mAgent = GetComponentInParent<NavMeshAgent>();
        mTransform = GetComponentInParent<Transform>();
        mPreviousPosition = mTransform.position;
        mAnimator.Play("idle_normal", 0, Random.value);
    }

    void Update()
    {
        mCurrentTime -= Time.deltaTime;

        Vector3 positionOffset = mTransform.position - mPreviousPosition;
        mPreviousPosition = mTransform.position;

        if (positionOffset != Vector3.zero && positionOffset.sqrMagnitude > (0.01f * 0.01f))
        {
            if (!mAnimator.GetBool("Walking"))
            {
                mAnimator.SetBool("Walking", true);
            }

            mAnimator.SetFloat("WalkSpeed", Mathf.Max(mAgent.velocity.magnitude / mAgent.speed, 0.5f));
        }
        else
        {
            if (mAnimator.GetBool("Walking"))
            {
                mAnimator.SetBool("Walking", false);
            }

            if (mAgent.remainingDistance > 0f && mAgent.remainingDistance < 2f)
            {
                mAgent.isStopped = true;
                mAgent.ResetPath();
            }

            if (mAnimator.GetCurrentAnimatorStateInfo(0).IsName("idle_normal") && mCurrentTime <= 0f)
            {
                mAnimator.CrossFadeInFixedTime("idle_eat", 2f);
            }
        }

        if (mCurrentTime <= 0f)
        {
            mCurrentTime = Random.Range(12f, 20f);
        }
    }
}
