using UnityEngine;
using UnityEngine.AI;

public class HuffaloAnimationScript : AIAnimationScript
{
    public AudioClip mEatingSound;
    public AudioClip mHuffaloSound;

    protected override void Start()
    {
        base.Start();
        mAgent = GetComponentInParent<NavMeshAgent>();
        mTransform = GetComponentInParent<Transform>();
        mPreviousPosition = mTransform.position;
    }

    protected override void RandomIdleAnimation()
    {
        if (mAnimator.GetCurrentAnimatorStateInfo(0).IsName(mDefaultIdleAnimation))
        {
            if(Random.value > 0.3f && mExtraIdleAnimations.Length > 0)
            {
                mAnimator.CrossFade(mExtraIdleAnimations[Random.Range(0, mExtraIdleAnimations.Length)], 0.2f);

                if(mAudio && mEatingSound)
                {
                    mAudio.PlayOneShot(mEatingSound);
                }
            }
            else if(mAudio && mHuffaloSound)
            {
                mAudio.PlayOneShot(mHuffaloSound);
            }
        }
    }
}
