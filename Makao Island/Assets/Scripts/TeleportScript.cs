using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    [SerializeField]
    private GameObject mStartPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (mStartPoint)
            {
                transform.position = mStartPoint.transform.position;
                mStartPoint = mStartPoint.GetComponent<NextPoint>().mNext;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
