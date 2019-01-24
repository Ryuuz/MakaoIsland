using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public int mDialogueNumber;
    public GameObject[] mSpeakers;

    private bool[] mSpeakerPresent;
    private bool mPlayerPresent = false;
    private List<Sentence> mSentences;

    void Start()
    {
        //Retrieve dialogue lines from the dialogue manager
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            mPlayerPresent = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

    private void PlayDialogue()
    {

    }

    private void StopDialogue()
    {

    }

    private bool AllSpeakersPresent()
    {
        bool present = true;

        for(int i = 0; i < mSpeakerPresent.Length; i++)
        {
            present = present && mSpeakerPresent[i];
        }

        return present;
    }
}
