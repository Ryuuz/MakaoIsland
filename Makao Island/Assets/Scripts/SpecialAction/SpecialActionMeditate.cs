using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialActionMeditate : SpecialActionObject
{
    public override void UseSpecialAction(bool active)
    {
        if(active)
        {
            GameManager.ManagerInstance().SetGameSpeed(4f);
        }
        else
        {
            GameManager.ManagerInstance().SetGameSpeed(1f);
        }
    }
}
