using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlacanAnimationScript : AIAnimationScript
{
    protected float mCurrentTime;

    protected override void Start()
    {
        base.Start();
        mCurrentTime = Random.Range(5f, 12f);
    }

    void Update()
    {
        mCurrentTime -= Time.deltaTime;
        if (mCurrentTime <= 0f)
        {
            mAnimator.CrossFadeInFixedTime("idle_turn", 2f);
            mCurrentTime = Random.Range(12f, 30f);
        }
    }
}
