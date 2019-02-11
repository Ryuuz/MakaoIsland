using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritAnimal : AIController
{
    public SpiritAnimalType mAnimalType;

    private Material mMaterial;
    private bool mFadedIn;
    private bool mFading = false;

    protected override void Start()
    {
        base.Start();

        mMaterial = GetComponent<MeshRenderer>().material;
        mFadedIn = true;

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
            if (!mFadedIn && !mFading)
            {
                mFading = true;
                mCurrentLocation = pos.position;
                StartCoroutine(FadeIn());
            }
        }
        else
        {
            //Fade out if possible
            if (mFadedIn && !mFading)
            {
                mFading = true;
                StartCoroutine(FadeOut());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Only if the spirit animal isn't transitioning between states
        if(other.tag == "Player" && mFadedIn && !mFading)
        {
            StartCoroutine(TriggerBlessing(other.transform.position));
        }
    }

    //Fades out the spirit animal and changes its status to match
    private IEnumerator FadeOut()
    {
        float fadeTime = 0f;
        //The delay can't be 0 since it will be used for dividing
        float fadeSpeed = (mTransitionDelay > 0) ? mTransitionDelay : 1f;

        Color endColor = mMaterial.color;
        endColor.a = 0f;
        Color startColor = mMaterial.color;

        while (mMaterial.color.a > 0f)
        {
            fadeTime += (Time.deltaTime / fadeSpeed) * mGameManager.mGameSpeed;
            mMaterial.color = Color.Lerp(startColor, endColor, fadeTime);
            yield return null;
        }
        //To make absolutely sure it is fully transparent
        mMaterial.color = endColor;

        mFading = false;
        mFadedIn = false;
    }

    //Fade the spirit animal in and update its status and position
    private IEnumerator FadeIn()
    {
        transform.position = mCurrentLocation;

        float fadeTime = 0f;
        //Make sure the delay isn't 0
        float fadeSpeed = (mTransitionDelay > 0) ? mTransitionDelay : 1f;

        Color endColor = mMaterial.color;
        endColor.a = 1f;
        Color startColor = mMaterial.color;

        while (mMaterial.color.a < 1f)
        {
            fadeTime += (Time.deltaTime / fadeSpeed) * mGameManager.mGameSpeed;
            mMaterial.color = Color.Lerp(startColor, endColor, fadeTime);
            yield return null;
        }
        //To make sure it is opaque
        mMaterial.color = endColor;

        mFading = false;
        mFadedIn = true;
    }

    //Register the spirit animal as found
    private IEnumerator TriggerBlessing(Vector3 player)
    {
        yield return StartCoroutine(LookAtObject(player));
        mGameManager.UpdateSpiritAnimals((int)mAnimalType);
        yield return StartCoroutine(FadeOut());
        Destroy(gameObject);
    }
}
