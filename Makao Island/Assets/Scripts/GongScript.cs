using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GongScript : MonoBehaviour
{
    public AudioClip mSound;

    private SpecialActionHitGong mHitGong;
    private PlayerController mPlayer;
    private Animator mAnimator;
    private AudioSource mAudio;

    void Start()
    {
        mPlayer = GameManager.ManagerInstance().mPlayer.GetComponent<PlayerController>();
        mHitGong = new SpecialActionHitGong(this);
        mAnimator = GetComponentInChildren<Animator>();
        mAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.ManagerInstance().mControlUI.ShowControlUI(ControlAction.hitGong);
            mPlayer.mSpecialAction = mHitGong;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //Removes the special action if it is the one currently active
            if (mPlayer.mSpecialAction == mHitGong)
            {
                mPlayer.mSpecialAction = null;
                GameManager.ManagerInstance().mControlUI.HideControlUI();
            }
        }
    }

    //Play the gong animation
    public void PlayGongAnimation()
    {
        mAnimator.CrossFadeInFixedTime("Gong_Animation_swing", 2f);

        if(mSound)
        {
            mAudio.PlayOneShot(mSound);
        }
    }
}
