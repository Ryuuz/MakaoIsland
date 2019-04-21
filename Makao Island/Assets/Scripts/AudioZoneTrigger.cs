using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioZoneTrigger : MonoBehaviour
{
    private AudioSource mAudio;

    void Start()
    {
        mAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && mAudio)
        {
            mAudio.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && mAudio)
        {
            mAudio.Stop();
        }
    }
}
