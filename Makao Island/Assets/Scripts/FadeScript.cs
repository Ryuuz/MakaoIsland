using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScript : MonoBehaviour
{
    public float mFadeDelay = 2f;
    public bool mFadedIn = false;
    public bool mFading { get; set; }

    private List<Material> mMaterialList;
    private GameManager mGameManager;

    private void Start()
    {
        mMaterialList = new List<Material>();
        mFading = false;
        mGameManager = GameManager.ManagerInstance();

        //Get all the renderers and their materials
        SkinnedMeshRenderer[] tempSkinnedRenderer = GetComponentsInChildren<SkinnedMeshRenderer>();
        MeshRenderer[] tempRenderer = GetComponentsInChildren<MeshRenderer>();

        for(int i = 0; i < tempRenderer.Length; i++)
        {
            if (tempRenderer[i])
            {
                mMaterialList.AddRange(tempRenderer[i].materials);
            }
        }
        
        for(int i = 0; i < tempSkinnedRenderer.Length; i++)
        {
            if(tempSkinnedRenderer[i])
            {
                mMaterialList.AddRange(tempSkinnedRenderer[i].materials);
            }
        }

        //Set the materials to transparent if the object shouldn't start as visible
        if (mMaterialList.Count > 0 && !mFadedIn)
        {
            for (int i = 0; i < mMaterialList.Count; i++)
            {
                mMaterialList[i].SetInt("_ZWrite", 1); //For correct sorting by the renderer https://forum.unity.com/threads/unity-5-fade-render-mode-sort-order-issue.318298/
                mMaterialList[i].color = new Color(mMaterialList[i].color.r, mMaterialList[i].color.g, mMaterialList[i].color.b, 0f);
            }
        }
    }

    //Fades out the object and changes its status to match
    public IEnumerator FadeOut()
    {
        mFading = true;
        float fadeTime = 0f;
        //The delay can't be 0 since it will be used for dividing
        float fadeSpeed = (mFadeDelay > 0) ? mFadeDelay : 1f;

        if (mMaterialList.Count > 0)
        {
            //The values it should end at
            Color[] endColors = new Color[mMaterialList.Count];
            for (int i = 0; i < endColors.Length; i++)
            {
                endColors[i] = mMaterialList[i].color;
                endColors[i].a = 0f;
            }

            //The values it starts at
            Color[] startColors = new Color[mMaterialList.Count];
            for (int i = 0; i < startColors.Length; i++)
            {
                startColors[i] = mMaterialList[i].color;
            }

            //Fades
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
            for (int i = 0; i < mMaterialList.Count; i++)
            {
                mMaterialList[i].color = endColors[i];
            }
        }

        mFading = false;
        mFadedIn = false;
    }

    //Fade the object in and update its status
    public IEnumerator FadeIn()
    {
        mFading = true;
        float fadeTime = 0f;
        //Make sure the delay isn't 0
        float fadeSpeed = (mFadeDelay > 0) ? mFadeDelay : 1f;

        if (mMaterialList.Count > 0)
        {
            //The values it should end at
            Color[] endColors = new Color[mMaterialList.Count];
            for (int i = 0; i < endColors.Length; i++)
            {
                endColors[i] = mMaterialList[i].color;
                endColors[i].a = 1f;
            }

            //The values it should start at
            Color[] startColors = new Color[mMaterialList.Count];
            for (int i = 0; i < startColors.Length; i++)
            {
                startColors[i] = mMaterialList[i].color;
            }

            //Fade
            while (mMaterialList[0].color.a < 1f)
            {
                fadeTime += (Time.deltaTime / fadeSpeed) * mGameManager.mGameSpeed;

                for (int i = 0; i < startColors.Length; i++)
                {
                    mMaterialList[i].color = Color.Lerp(startColors[i], endColors[i], fadeTime);
                }

                yield return null;
            }
            //To make absolutely sure it is fully opaque
            for (int i = 0; i < mMaterialList.Count; i++)
            {
                mMaterialList[i].color = endColors[i];
            }
        }

        mFading = false;
        mFadedIn = true;
    }
}
