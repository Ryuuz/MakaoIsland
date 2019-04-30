using UnityEngine;

public class FlacanAnimationScript : AIAnimationScript
{
    public AudioClip mFlacanSound;

    protected override void Update()
    {
        mCurrentTime -= Time.deltaTime;

        if (mCurrentTime <= 0f)
        {
            RandomIdleAnimation();
            mCurrentTime = Random.Range(mMinDelay, mMaxDelay);
        }
    }

    protected override void RandomIdleAnimation()
    {
        //Randomly choose between playing a different idle animation or playing a sound clip
        if (mAnimator.GetCurrentAnimatorStateInfo(0).IsName(mDefaultIdleAnimation))
        {
            if (Random.value > 0.5f && mExtraIdleAnimations.Length > 0)
            {
                mAnimator.CrossFade(mExtraIdleAnimations[Random.Range(0, mExtraIdleAnimations.Length)], 0.2f);
            }
            else if (mAudio && mFlacanSound && !mAudio.isPlaying)
            {
                mAudio.PlayOneShot(mFlacanSound);
            }
        }
    }
}
