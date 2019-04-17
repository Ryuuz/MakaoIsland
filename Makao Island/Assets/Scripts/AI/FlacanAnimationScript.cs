using UnityEngine;

public class FlacanAnimationScript : AIAnimationScript
{
    protected override void Update()
    {
        mCurrentTime -= Time.deltaTime;

        if (mCurrentTime <= 0f)
        {
            RandomIdleAnimation();
            mCurrentTime = Random.Range(mMinDelay, mMaxDelay);
        }
    }
}
