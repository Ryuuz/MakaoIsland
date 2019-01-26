using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public int mDialogueNumber;
    public float mCooldown = 10f;
    public GameObject[] mSpeakers;

    private AIController[] mSpeakerControllers;
    private bool[] mSpeakerPresent;
    private GameObject mPlayerPresent = null;
    private List<Sentence> mSentences = new List<Sentence>();
    private DialogueManager mDialogueManager;
    private bool mPlaying = false;
    private bool mPlayerListening = false;
    private bool mCoolingDown = false;
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

        mSpeakerControllers = new AIController[mSpeakers.Length];

        for(int i = 0; i < mSpeakerControllers.Length; i++)
        {
            mSpeakerControllers[i] = mSpeakers[i].GetComponent<AIController>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
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

        if(AllSpeakersPresent() && mPlayerPresent && !mCoolingDown)
        {
            mPlayerPresent.GetComponent<PlayerController>().mSpecialAction = mListenAction;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            mPlayerPresent.GetComponent<PlayerController>().mSpecialAction = null;
            mPlayerPresent = null;
            mPlayerListening = false;
            mDialogueManager.HideDialogueBox();
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
            for(int i = 0; i < mSpeakerControllers.Length; i++)
            {
                mSpeakerControllers[i].mTalking = true;
            }
            mPlaying = true;
            mPlayerListening = true;

            mPlayerPresent.GetComponent<PlayerController>().mSpecialAction = null;

            StartCoroutine(DialogueRunning());
        }
        else
        {
            mPlayerListening = true;
            mDialogueManager.ShowDialogueBox();
            mPlayerPresent.GetComponent<PlayerController>().mSpecialAction = null;
        }
    }

    private IEnumerator DialogueRunning()
    {
        float dialogueTime = 0f;

        foreach(Sentence line in mSentences)
        {
            dialogueTime = 0.4f * line.text.Length;

            if(mPlayerListening)
            {
                mDialogueManager.FillDialogueBox(mSpeakers[line.speaker - 1].name, line.text, mSpeakerControllers[line.speaker - 1].mIcon);
            }

            yield return new WaitForSeconds(dialogueTime);
        }

        StartCoroutine(StopDialogue());
    }

    private IEnumerator StopDialogue()
    {
        for (int i = 0; i < mSpeakerControllers.Length; i++)
        {
            mSpeakerControllers[i].mTalking = false;
        }
        mPlaying = false;
        mPlayerListening = false;

        mDialogueManager.HideDialogueBox();

        mPlayerPresent.GetComponent<PlayerController>().mSpecialAction = null;

        Debug.Log("Starting cooldown");
        mCoolingDown = true;
        yield return new WaitForSeconds(mCooldown);
        mCoolingDown = false;
        Debug.Log("Cooldown over");

        if (AllSpeakersPresent() && mPlayerPresent)
        {
            mPlayerPresent.GetComponent<PlayerController>().mSpecialAction = mListenAction;
        }
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
