using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrineScript : MonoBehaviour
{
    private SpecialActionObject mSpecialObject;

    void Start()
    {
        mSpecialObject = new SpecialActionMeditate();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Give the player a special action when in range of shrine
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerController>().mSpecialAction = mSpecialObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().mSpecialAction = null;
            mSpecialObject.UseSpecialAction(false);
        }
    }
}
