using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTutorialScript : ControlTutorial
{
    protected bool mPlayer = false;

    protected void Update()
    {
        if(mPlayer && (Input.GetButtonDown("Map") || Input.GetButtonDown("GP Map")))
        {
            GameManager.ManagerInstance().mControlUI.HideControlUI();
            Destroy(gameObject);
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.ManagerInstance().mControlUI.ShowControlUI(mAction);
            mPlayer = true;
        }
    }

    //Delete the trigger when the player exits
    protected override void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.ManagerInstance().mControlUI.HideControlUI();
            Destroy(gameObject);
        }
    }
}
