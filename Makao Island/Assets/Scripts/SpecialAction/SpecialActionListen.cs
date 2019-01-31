using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialActionListen : SpecialActionObject
{
    private DialogueTrigger mOwner;

    public SpecialActionListen(DialogueTrigger owner)
    {
        mOwner = owner;
    }

    public override void UseSpecialAction(bool active)
    {
        if (active)
        {
            mOwner.PlayDialogue();
        }
    }
}
