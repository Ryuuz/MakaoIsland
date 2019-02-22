using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritAnimal : AIController
{
    public SpiritAnimalType mAnimalType;

    private FadeScript mFade;

    protected override void Start()
    {
        base.Start();
        mFade = GetComponent<FadeScript>();

        if(!mFade)
        {
            mFade = gameObject.AddComponent<FadeScript>();
        }

        //Make sure the spirit animal is in the right place and state
        Transition(mGameManager.mGameStatus.mDayTime);
        
        if (mCurrentLocation != transform.position)
        {
            transform.position = mCurrentLocation;
        }
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
            }
        }
        else
        {
            //Fade out if possible
            if (mFade.mFadedIn && !mFade.mFading)
            {
                StartCoroutine(mFade.FadeOut());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Only if the spirit animal isn't transitioning between states
        if(other.tag == "Player" && mFade.mFadedIn && !mFade.mFading)
        {
            StartCoroutine(TriggerBlessing(other.transform.position));
        }
    }

    //Register the spirit animal as found
    private IEnumerator TriggerBlessing(Vector3 player)
    {
        yield return StartCoroutine(LookAtObject(player));
        mGameManager.UpdateSpiritAnimals((int)mAnimalType);
        yield return StartCoroutine(mFade.FadeOut());
        Destroy(gameObject);
    }
}
