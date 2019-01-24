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

    void Start()
    {
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
