using UnityEngine;
using UnityEngine.AI;

public class HuffaloAnimationScript : AIAnimationScript
{
    protected override void Start()
    {
        base.Start();
        mAgent = GetComponentInParent<NavMeshAgent>();
        mTransform = GetComponentInParent<Transform>();
        mPreviousPosition = mTransform.position;
    }
}
