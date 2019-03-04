using System.Collections;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    [SerializeField]
    private GameObject mStartPoint;
    [SerializeField]
    private GameObject mMapPrefab;

    private FadeScript mFadeScript;

    private void Start()
    {
        mFadeScript = GetComponentInChildren<FadeScript>();
    }

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
                StartCoroutine(Vanish());
            }
        }
    }

    private IEnumerator Vanish()
    {
        if(mFadeScript)
        {
            yield return StartCoroutine(mFadeScript.FadeOut());
        }
        Instantiate(mMapPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
