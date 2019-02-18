using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScript : MonoBehaviour
{
    public float mFadeDelay = 2f;
    public bool mFadedIn { get; set; }
    public bool mFading { get; set; }

    private Material[] mMaterialList;
    private GameManager mGameManager;

    private void Start()
    {
        mFadedIn = false;
        mFading = false;
        mGameManager = GameManager.ManagerInstance();
        MeshRenderer tempRenderer = GetComponent<MeshRenderer>();

        if (tempRenderer)
        {
            mMaterialList = tempRenderer.materials;
        }
        else
        {
            mMaterialList = GetComponentInChildren<SkinnedMeshRenderer>().materials;
        }

        if (mMaterialList.Length > 0)
        {
            for (int i = 0; i < mMaterialList.Length; i++)
            {
                mMaterialList[i].color = new Color(mMaterialList[i].color.r, mMaterialList[i].color.g, mMaterialList[i].color.b, 0f);
            }
        }
    }

    //Fades out the spirit animal and changes its status to match
    public IEnumerator FadeOut()
    {
        mFading = true;
        float fadeTime = 0f;
        //The delay can't be 0 since it will be used for dividing
        float fadeSpeed = (mFadeDelay > 0) ? mFadeDelay : 1f;

        if (mMaterialList.Length > 0)
        {
            Color[] endColors = new Color[mMaterialList.Length];
            for (int i = 0; i < endColors.Length; i++)
            {
                endColors[i] = mMaterialList[i].color;
                endColors[i].a = 0f;
            }

            Color[] startColors = new Color[mMaterialList.Length];
            for (int i = 0; i < startColors.Length; i++)
            {
                startColors[i] = mMaterialList[i].color;
            }

            while (mMaterialList[0].color.a > 0f)
            {
                fadeTime += (Time.deltaTime / fadeSpeed) * mGameManager.mGameSpeed;

                for (int i = 0; i < startColors.Length; i++)
                {
                    mMaterialList[i].color = Color.Lerp(startColors[i], endColors[i], fadeTime);
                }

                yield return null;
            }
            //To make absolutely sure it is fully transparent
            for (int i = 0; i < mMaterialList.Length; i++)
            {
                mMaterialList[i].color = endColors[i];
            }
        }

        mFading = false;
        mFadedIn = false;
    }

    //Fade the spirit animal in and update its status and position
    public IEnumerator FadeIn()
    {
        mFading = true;
        float fadeTime = 0f;
        //Make sure the delay isn't 0
        float fadeSpeed = (mFadeDelay > 0) ? mFadeDelay : 1f;

        if (mMaterialList.Length > 0)
        {
            Color[] endColors = new Color[mMaterialList.Length];
            for (int i = 0; i < endColors.Length; i++)
            {
                endColors[i] = mMaterialList[i].color;
                endColors[i].a = 1f;
            }

            Color[] startColors = new Color[mMaterialList.Length];
            for (int i = 0; i < startColors.Length; i++)
            {
                startColors[i] = mMaterialList[i].color;
            }

            while (mMaterialList[0].color.a < 1f)
            {
                fadeTime += (Time.deltaTime / fadeSpeed) * mGameManager.mGameSpeed;

                for (int i = 0; i < startColors.Length; i++)
                {
                    mMaterialList[i].color = Color.Lerp(startColors[i], endColors[i], fadeTime);
                }

                yield return null;
            }
            //To make absolutely sure it is fully transparent
            for (int i = 0; i < mMaterialList.Length; i++)
            {
                mMaterialList[i].color = endColors[i];
            }
        }

        mFading = false;
        mFadedIn = true;
    }
}
