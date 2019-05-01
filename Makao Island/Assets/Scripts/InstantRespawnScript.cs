using UnityEngine;

public class InstantRespawnScript : MonoBehaviour
{
    private RespawnScript mRespawn;

    void Start()
    {
        GameObject temp = GameManager.ManagerInstance().mPlayer;
        mRespawn = temp.GetComponent<RespawnScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && mRespawn)
        {
            mRespawn.RespawnPlayer();
        }
    }
}
