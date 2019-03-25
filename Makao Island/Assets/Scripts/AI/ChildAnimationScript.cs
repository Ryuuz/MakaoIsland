using UnityEngine;
using UnityEngine.AI;

public class ChildAnimationScript : AIAnimationScript
{
    protected NavMeshAgent mAgent;
    protected Transform mTransform;
    protected Vector3 mPreviousPosition;

    protected override void Start()
    {
        base.Start();
        mAgent = GetComponentInParent<NavMeshAgent>();
        mTransform = GetComponentInParent<Transform>();
        mPreviousPosition = mTransform.position;
        mAnimator.Play("Child_Idle_Animation", 0, Random.value);
    }

    void Update()
    {
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
        }
    }
}
