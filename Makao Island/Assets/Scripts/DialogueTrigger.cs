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
    private DialogueManager mDialogueManager;
    private bool mPlaying = false;
    private bool mPlayerListening = false;
    private SpecialActionListen mListenAction;

    void Start()
    {
        mListenAction = new SpecialActionListen(this);

        //Retrieve dialogue lines from the dialogue manager
        GameObject temp = GameManager.ManagerInstance().mDialogueManager;
        if(temp)
        {
            mDialogueManager = temp.GetComponent<DialogueManager>();
        }

        GetDialogue();

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
            mPlayerPresent.GetComponent<PlayerController>().mSpecialAction = mListenAction;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            mPlayerPresent.GetComponent<PlayerController>().mSpecialAction = null;
            mPlayerPresent = null;
            mPlayerListening = false;
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

    public void PlayDialogue()
    {
        if(!mPlaying)
        {
            //Set npcs talking to true
            //Set listening and playing to true
        }
    }

    private IEnumerator DialogueRunning()
    {
        foreach(Sentence line in mSentences)
        {
            //Calculate time
            //if listening
                //Retrieve npc name and icon
                //Update dialogue box 
            //yield the calculated time
        }

        StopDialogue();
        yield return null;
    }

    private void StopDialogue()
    {
        //Set npcs talking to false
        //Set listening and playing to false
        //Hide dialogue box
    }

    private bool AllSpeakersPresent()
    {
        if(mSpeakerPresent.Length == 0)
        {
            return false;
        }

        for(int i = 0; i < mSpeakerPresent.Length; i++)
        {
            if(!mSpeakerPresent[i])
            {
                return false;
            }
        }

        return true;
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

    private void GetDialogue()
    {
        if(mDialogueManager)
        {
            Sentence[] tempDialogue = mDialogueManager.mDialogueLines.GetConversation(mDialogueNumber);

            foreach (Sentence line in tempDialogue)
            {
                mSentences.Add(line);
            }
        }
    }
}
