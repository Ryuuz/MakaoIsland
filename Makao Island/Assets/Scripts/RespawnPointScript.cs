using UnityEngine;

public class RespawnPointScript : MonoBehaviour
{
    private Transform mTransform;

    private void Start()
    {
        mTransform = GetComponent<Transform>();
    }

    //Update where to respawn
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameManager.ManagerInstance().mCurrentRespawnPoint = mTransform;
        }
    }
}
