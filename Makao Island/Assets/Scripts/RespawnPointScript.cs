using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPointScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameManager.ManagerInstance().mCurrentRespawnPoint = transform.position;
        }
    }
}
