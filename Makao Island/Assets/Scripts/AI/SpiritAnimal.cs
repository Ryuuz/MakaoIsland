using System.Collections;
using UnityEngine;

public class SpiritAnimal : AIController
{
    public SpiritAnimalType mAnimalType;

    private FadeScript mFade;
    private SpecialActionRecieveBlessing mRecieveBlessing;
    private PlayerController mPlayer;
    private bool mPlayerPresent = false;

    protected override void Start()
    {
        if(GameManager.ManagerInstance().mData.mSpiritAnimalsStatus[(int)mAnimalType])
        {
            Destroy(gameObject);
        }

        base.Start();
        mFade = GetComponent<FadeScript>();

        if(!mFade)
        {
            mFade = gameObject.AddComponent<FadeScript>();
        }

        //Make sure the spirit animal is in the right place and state
        Transition((DayCyclus)mGameManager.mData.mDayTime);
        
        if (mCurrentLocation != transform.position)
        {
            transform.position = mCurrentLocation;
        }

        mRecieveBlessing = new SpecialActionRecieveBlessing(this);
        mPlayer = mGameManager.mPlayer.GetComponent<PlayerController>();
    }

    //Change the spirit animal's location based on the time of day
    public override void Transition(DayCyclus time)
    {
        Transform pos = null;

        switch (time)
        {
            case DayCyclus.dawn:
                pos = mDawnLocation;
                break;

            case DayCyclus.day:
                pos = mDayLocation;
                break;

            case DayCyclus.dusk:
                pos = mDuskLocation;
                break;

            case DayCyclus.night:
                pos = mNightLocation;
                break;
        }

        //If the spirit animal is set to appear at a location
        if (pos)
        {
            //Fade it in if possible
            if (!mFade.mFadedIn && !mFade.mFading)
            {
                mCurrentLocation = pos.position;
                transform.position = mCurrentLocation;
                StartCoroutine(mFade.FadeIn());

                if(mPlayerPresent)
                {
                    mGameManager.mControlUI.ShowControlUI(ControlAction.recieveBlessing);
                    mPlayer.mSpecialAction = mRecieveBlessing;
                }
            }
        }
        else
        {
            //Fade out if possible
            if (mFade.mFadedIn && !mFade.mFading)
            {
                if (mPlayerPresent && mPlayer.mSpecialAction == mRecieveBlessing)
                {
                    mPlayer.mSpecialAction = null;
                    mGameManager.mControlUI.HideControlUI();
                }

                StartCoroutine(mFade.FadeOut());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Only if the spirit animal isn't transitioning between states
        if(other.tag == "Player" && mFade.mFadedIn && !mFade.mFading)
        {
            mGameManager.mControlUI.ShowControlUI(ControlAction.recieveBlessing);
            mPlayer.mSpecialAction = mRecieveBlessing;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //Removes the special action if it is the one currently active
            if (mPlayer.mSpecialAction == mRecieveBlessing)
            {
                mPlayer.mSpecialAction = null;
                mGameManager.mControlUI.HideControlUI();
            }
        }
    }

    public void TriggerBlessing()
    {
        StartCoroutine(Blessing(mPlayer.transform.position));
    }

    //Register the spirit animal as found
    private IEnumerator Blessing(Vector3 player)
    {
        yield return StartCoroutine(LookAtObject(player));
        mGameManager.UpdateSpiritAnimals((int)mAnimalType);
        yield return StartCoroutine(mFade.FadeOut());
        Destroy(gameObject);
    }
}
