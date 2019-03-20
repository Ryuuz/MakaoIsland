using UnityEngine;
using UnityEngine.AI;

public class VillagerAnimationScript : AIAnimationScript
{
    protected NavMeshAgent mAgent;
    protected float mCurrentTime;
    protected bool mSwitchIdle = false;

    protected override void Start()
    {
        base.Start();
        mAgent = GetComponentInParent<NavMeshAgent>();
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

        if(mAgent.velocity != Vector3.zero)
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
}
