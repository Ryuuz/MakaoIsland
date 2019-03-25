using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuffaloAnimationScript : AIAnimationScript
{
    protected float mCurrentTime;

    protected override void Start()
    {
        base.Start();
        mCurrentTime = Random.Range(5f, 12f);
        mAnimator.Play("idle_normal", 0, Random.value);
    }

    void Update()
    {
        mCurrentTime -= Time.deltaTime;

        /*if (mAgent.velocity != Vector3.zero)
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

            if (mAnimator.GetCurrentAnimatorStateInfo(0).IsName("idle_normal") && mCurrentTime <= 0f)
            {
                mAnimator.CrossFadeInFixedTime("idle_eat", 2f);
            }
        }

        if(mCurrentTime <= 0f)
        {
            mCurrentTime = Random.Range(12f, 20f);
        }*/

        if (mCurrentTime <= 0f)
        {
            mAnimator.CrossFadeInFixedTime("idle_eat", 2f);
            mCurrentTime = Random.Range(12f, 20f);
        }
    }
}
