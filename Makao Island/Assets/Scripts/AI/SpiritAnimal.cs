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

        Transition(mGameManager.mGameStatus.mDayTime);

        if (mCurrentLocation != transform.position)
        {
            transform.position = mCurrentLocation;
        }
    }

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

        if (pos)
        {
            if (!mFadedIn && !mFading)
            {
                mFading = true;
                mCurrentLocation = pos.position;
                StartCoroutine(FadeIn());
            }
        }
        else
        {
            if (mFadedIn && !mFading)
            {
                mFading = true;
                StartCoroutine(FadeOut());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && mFadedIn && !mFading)
        {
            Debug.Log("Blessing player");//StartCoroutine(TriggerBlessing());
        }
    }

    private IEnumerator FadeOut()
    {
        float fadeTime = 0f;
        Color endColor = mMaterial.color;
        endColor.a = 0f;
        Color startColor = mMaterial.color;
        float fadeSpeed = (mTransitionDelay > 0) ? mTransitionDelay : 1f;

        while (mMaterial.color.a > 0f)
        {
            fadeTime += (Time.deltaTime / fadeSpeed) * mGameManager.mGameSpeed;

            mMaterial.color = Color.Lerp(startColor, endColor, fadeTime);
            yield return null;
        }
        mMaterial.color = endColor;

        mFading = false;
        mFadedIn = false;
    }

    private IEnumerator FadeIn()
    {
        transform.position = mCurrentLocation;

        float fadeTime = 0f;
        Color endColor = mMaterial.color;
        endColor.a = 1f;
        Color startColor = mMaterial.color;
        float fadeSpeed = (mTransitionDelay > 0) ? mTransitionDelay : 1f;

        while (mMaterial.color.a < 1f)
        {
            fadeTime += (Time.deltaTime / fadeSpeed) * mGameManager.mGameSpeed;

            mMaterial.color = Color.Lerp(startColor, endColor, fadeTime);
            yield return null;
        }
        mMaterial.color = endColor;

        mFading = false;
        mFadedIn = true;
    }

    private IEnumerator TriggerBlessing(Vector3 player)
    {
        yield return StartCoroutine(LookAtObject(player));
        mGameManager.mProgress.mSpiritAnimalsStatus[(int)mAnimalType] = true;
        yield return StartCoroutine(FadeOut());
        Destroy(gameObject);
    }
}
