using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialActionGuide : SpecialActionObject
{
    private FollowGuideScript mOwner;
    private Transform mPlayer;

    public SpecialActionGuide(FollowGuideScript owner)
    {
        mOwner = owner;
        mPlayer = GameManager.ManagerInstance().mPlayer.transform;
    }

    public override void UseSpecialAction(bool active)
    {
        if (active)
        {
            mOwner.FollowGuide(mPlayer.position);
        }
    }
}
