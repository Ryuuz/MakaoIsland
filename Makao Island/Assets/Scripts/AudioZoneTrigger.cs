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

        //Save the original volume of the sounds that are set to be dimmed
        if(mDimmedSources.Length > 0)
        {
            mOriginalVolume = new float[mDimmedSources.Length];

            for(int i = 0; i < mDimmedSources.Length; i++)
            {
                mOriginalVolume[i] = mDimmedSources[i].volume;
            }
        }
    }

    //https://johnleonardfrench.com/articles/10-unity-audio-tips-that-you-wont-find-in-the-tutorials/#audio_zones
    private void OnTriggerEnter(Collider other)
    {
        //Increase the trigger count and start playing the sound while dimming the other assigned sounds
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
        //Decrease trigger count. If 0 then the player isn't in any of the triggers and the sound should stop playing
        if (other.tag == "Player" && mAudio)
        {
            mTriggerCount--;

            if(mTriggerCount <= 0)
            {
                mAudio.Stop();

                //Return the dimmed sounds to their original volume
                for (int i = 0; i < mDimmedSources.Length; i++)
                {
                     mDimmedSources[i].volume = mOriginalVolume[i];
                }
            }
        }
    }
}
