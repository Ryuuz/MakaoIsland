using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialActionGuide : SpecialActionObject
{
    private SpiritGirl mOwner;
    private Transform mPlayer;

    public SpecialActionGuide(SpiritGirl owner)
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
