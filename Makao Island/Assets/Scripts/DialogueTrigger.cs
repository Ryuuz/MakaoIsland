using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public int mDialogueNumber;
    public GameObject[] mSpeakers;

    private bool[] mSpeakerPresent;
    private GameObject mPlayerPresent = null;
    private List<Sentence> mSentences;

    void Start()
    {
        //Retrieve dialogue lines from the dialogue manager

        //The speakers that are present inside the dialogue trigger from the start
        mSpeakerPresent = new bool[mSpeakers.Length];

        for(int i = 0; i < mSpeakers.Length; i++)
        {
            if(Vector3.Distance(mSpeakers[i].transform.position, transform.position) <= GetComponent<SphereCollider>().radius)
            {
                mSpeakerPresent[i] = true;
            }
            else
            {
                mSpeakerPresent[i] = false;
            }
        }
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            mPlayerPresent = other.gameObject;
        }
        else if(!AllSpeakersPresent())
        {
            if(IsASpeaker(other.gameObject))
            {
                for(int i = 0; i < mSpeakers.Length; i++)
                {
                    if(other.gameObject == mSpeakers[i])
                    {
                        mSpeakerPresent[i] = true;
                    }
                }
            }
        }

        if(AllSpeakersPresent() && mPlayerPresent)
        {
            //Give listen action
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            mPlayerPresent.GetComponent<PlayerController>().mSpecialAction = null;
            mPlayerPresent = null;
        }
        else if(IsASpeaker(other.gameObject))
        {
            for (int i = 0; i < mSpeakers.Length; i++)
            {
                if (other.gameObject == mSpeakers[i])
                {
                    mSpeakerPresent[i] = false;
                }
            }
        }

        if(mPlayerPresent && !AllSpeakersPresent())
        {
            mPlayerPresent.GetComponent<PlayerController>().mSpecialAction = null;
        }
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

    private bool IsASpeaker(GameObject character)
    {
        for(int i = 0; i < mSpeakers.Length; i++)
        {
            if(character == mSpeakers[i])
            {
                return true;
            }
        }

        return false;
    }
}
