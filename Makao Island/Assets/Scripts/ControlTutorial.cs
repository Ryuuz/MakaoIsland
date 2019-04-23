using UnityEngine;

public class ControlTutorial : MonoBehaviour
{
    public ControlAction mAction;
    private GameManager mGameManager;

    void Start()
    {
        mGameManager = GameManager.ManagerInstance();
        //Check if player is inside the trigger
        if(Vector3.Distance(GameManager.ManagerInstance().mPlayer.transform.position, transform.position) < GetComponent<SphereCollider>().radius)
        {
            if(GameManager.ManagerInstance().mControlUI)
            {
                GameManager.ManagerInstance().mControlUI.ShowControlUI(mAction);
            }
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
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        if(mGameManager)
        {
            mGameManager.mControlUI.HideControlUI();
        }
    }
}
