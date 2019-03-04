using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveMapScript : MonoBehaviour
{
    [SerializeField]
    private GameObject mMapControl;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            InputHandler.InputInstance().mMapManager.mMapAvailable = true;

            if(mMapControl)
            {
                GameObject temp = Instantiate(mMapControl, transform.position, Quaternion.identity);
                temp.GetComponent<ControlTutorial>().mAction = ControlAction.map;
            }
            
            Destroy(gameObject);
        }
    }
}
