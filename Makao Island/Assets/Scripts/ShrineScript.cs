using UnityEngine;

public class ShrineScript : MonoBehaviour
{
    private SpecialActionMeditate mMeditateAction;
    private PlayerController mPlayer;

    void Start()
    {
        mMeditateAction = new SpecialActionMeditate();
        mPlayer = GameManager.ManagerInstance().mPlayer.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Give the player a special action when in range of shrine
        if(other.tag == "Player")
        {
            GameManager.ManagerInstance().mControlUI.ShowControlUI(ControlAction.meditate);
            mPlayer.mSpecialAction = mMeditateAction;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //Removes the special action if it is the one currently active
            if (mPlayer.mSpecialAction == mMeditateAction)
            {
                mPlayer.mSpecialAction = null;
                GameManager.ManagerInstance().mControlUI.HideControlUI();

                //Makes sure the game speed is returned to normal
                mMeditateAction.UseSpecialAction(false);
            }
        }
    }
}
