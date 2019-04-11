using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialActionHitGong : SpecialActionObject
{
    private GongScript mOwner;

    public SpecialActionHitGong(GongScript owner)
    {
        mOwner = owner;
    }

    public override void UseSpecialAction(bool active)
    {
        if(active)
        {
            mOwner.PlayGongAnimation();
        }
    }
}
