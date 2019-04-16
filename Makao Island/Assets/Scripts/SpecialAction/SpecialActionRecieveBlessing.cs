using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialActionRecieveBlessing : SpecialActionObject
{
    private SpiritAnimal mOwner;

    public SpecialActionRecieveBlessing(SpiritAnimal owner)
    {
        mOwner = owner;
    }

    public override void UseSpecialAction(bool active)
    {
        if (active)
        {
            mOwner.TriggerBlessing();
        }
    }
}
