﻿using UnityEngine;

public class ControlTutorial : MonoBehaviour
{
    public ControlAction mAction;

    void Start()
    {
        //Check if player is inside the trigger
        if(Vector3.Distance(GameManager.ManagerInstance().mPlayer.transform.position, transform.position) < GetComponent<SphereCollider>().radius)
        {
            if(GameManager.ManagerInstance().mControlUI)
            {
                GameManager.ManagerInstance().mControlUI.ShowControlUI(mAction);
            }
        }

        if(PlayerPrefs.GetInt("Load", 0) == 1)
        {
            GameManager.ManagerInstance().mControlUI.HideControlUI();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameManager.ManagerInstance().mControlUI.ShowControlUI(mAction);
        }
    }

    //Delete the trigger when the player exits
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            GameManager.ManagerInstance().RemoveObject(name);
            GameManager.ManagerInstance().mControlUI.HideControlUI();
            Destroy(gameObject);
        }
    }
}
