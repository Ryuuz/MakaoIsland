using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrineScript : MonoBehaviour
{
    public SpecialActionObject mSpecialObject;

    // Start is called before the first frame update
    void Start()
    {
        if(mSpecialObject == null)
        {
            mSpecialObject = new SpecialActionMeditate();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
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
