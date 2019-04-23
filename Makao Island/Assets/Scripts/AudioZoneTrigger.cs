using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioZoneTrigger : MonoBehaviour
{
    public AudioSource[] mDimmedSources;
    public float mDimmedVolume;

    private AudioSource mAudio;
    private float[] mOriginalVolume;
    private int mTriggerCount = 0;

    void Start()
    {
        mAudio = GetComponent<AudioSource>();

        if(mDimmedSources.Length > 0)
        {
            mOriginalVolume = new float[mDimmedSources.Length];

            for(int i = 0; i < mDimmedSources.Length; i++)
            {
                mOriginalVolume[i] = mDimmedSources[i].volume;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && mAudio)
        {
            ++mTriggerCount;

            if(!mAudio.isPlaying)
            {
                mAudio.Play();

                for (int i = 0; i < mDimmedSources.Length; i++)
                {
                    mDimmedSources[i].volume = mDimmedVolume;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && mAudio)
        {
            mTriggerCount--;

            if(mTriggerCount <= 0)
            {
                mAudio.Stop();

                for (int i = 0; i < mDimmedSources.Length; i++)
                {
                     mDimmedSources[i].volume = mOriginalVolume[i];
                }
            }
        }
    }
}
