using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutSound : MonoBehaviour
{
    public float mFadeDelay = 0.5f;
    private AudioSource mAudio;

    void Start()
    {
        mAudio = GetComponent<AudioSource>();
    }

    public void StartFadingSound()
    {
        if(mAudio)
        {
            StartCoroutine(FadeSound());
        }
    }

    private IEnumerator FadeSound()
    {
        float lerpTime = 0;

        while(lerpTime <= 1f)
        {
            lerpTime += (mFadeDelay * Time.deltaTime);
            mAudio.volume = Mathf.Lerp(1f, 0f, lerpTime);
            yield return null;
        }
    }
}
