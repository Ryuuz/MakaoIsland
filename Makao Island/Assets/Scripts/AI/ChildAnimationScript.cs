using UnityEngine;
using UnityEngine.AI;

public class ChildAnimationScript : AIAnimationScript
{
    protected override void Start()
    {
        base.Start();
        mAgent = GetComponentInParent<NavMeshAgent>();
        mTransform = GetComponentInParent<Transform>();
        mPreviousPosition = mTransform.position;
    }

    protected override void Update()
    {
        WalkingAnimation();
    }
}
