using UnityEngine;

public class ControlTutorial : MonoBehaviour
{
    public ControlAction mAction;

    protected void Start()
    {
        //Check if player is inside the trigger
        if(PlayerPrefs.GetInt("Load", 0) == 0 && (GameManager.ManagerInstance().mPlayer.transform.position - transform.position).sqrMagnitude < GetComponent<SphereCollider>().radius)
        {
            if(GameManager.ManagerInstance().mControlUI)
            {
                GameManager.ManagerInstance().mControlUI.ShowControlUI(mAction);
            }
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && (PlayerPrefs.GetInt("Load", 0) == 0 || mAction != ControlAction.walk))
        {
            GameManager.ManagerInstance().mControlUI.ShowControlUI(mAction);
        }
    }

    //Delete the trigger when the player exits
    protected virtual void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            GameManager.ManagerInstance().RemoveObject(name);
            GameManager.ManagerInstance().mControlUI.HideControlUI();
            Destroy(gameObject);
        }
    }
}
