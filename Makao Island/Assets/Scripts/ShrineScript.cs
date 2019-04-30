using UnityEngine;

public class ShrineScript : MonoBehaviour
{
    private SpecialActionMeditate mMeditateAction;
    private PlayerController mPlayer;
    private AudioSource mAudio;

    void Start()
    {
        mMeditateAction = new SpecialActionMeditate(this);
        mPlayer = GameManager.ManagerInstance().mPlayer.GetComponent<PlayerController>();
        mAudio = GetComponent<AudioSource>();
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

    //Play the sound the shrine has
    public void ShrineSound(bool play)
    {
        if(mAudio && mAudio.clip)
        {
            if(play && !mAudio.isPlaying)
            {
                mAudio.Play();
            }
            else
            {
                mAudio.Stop();
            }
        }
    }
}
