using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    [SerializeField]
    private GameObject mStartPoint;

    //Teleport to start point when player gets too close
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (mStartPoint)
            {
                transform.position = mStartPoint.transform.position;

                //Get the next point to teleport to
                mStartPoint = mStartPoint.GetComponent<NextPoint>().mNext;
            }
            //If there isn't a point to teleport to
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
