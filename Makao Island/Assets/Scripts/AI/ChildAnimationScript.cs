using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChildAnimationScript : AIAnimationScript
{
    protected NavMeshAgent mAgent;

    protected override void Start()
    {
        base.Start();
        mAgent = GetComponentInParent<NavMeshAgent>();
        mAnimator.Play("Child_Idle_Animation", 0, Random.value);
    }

    void Update()
    {
        if (mAgent.velocity != Vector3.zero)
        {
            if (!mAnimator.GetBool("Walking"))
            {
                mAnimator.SetBool("Walking", true);
            }

            mAnimator.SetFloat("WalkSpeed", Mathf.Max(mAgent.velocity.magnitude / mAgent.speed, 0.3f));
        }
        else
        {
            if (mAnimator.GetBool("Walking"))
            {
                mAnimator.SetBool("Walking", false);
            }
        }
    }
}
