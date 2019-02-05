using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public int mDialogueNumber;
    public float mCooldown = 10f;
    public GameObject[] mSpeakers;

    private AITalking[] mSpeakerControllers;
    private bool[] mSpeakerPresent;
    private PlayerController mPlayerPresent = null;
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

        //Get the script component of the AI
        mSpeakerControllers = new AITalking[mSpeakers.Length];

        for(int i = 0; i < mSpeakerControllers.Length; i++)
        {
            mSpeakerControllers[i] = mSpeakers[i].GetComponent<AITalking>();
        }

        if(AllSpeakersPresent())
        {
            ToggleSpeechBubbles(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            mPlayerPresent = other.gameObject.GetComponent<PlayerController>();
        }
        //If one of the AI belonging to the dialogue sphere enters, set its status to present
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

                if (AllSpeakersPresent())
                {
                    ToggleSpeechBubbles(true);
                }
            }
        }

        //Give the player the listen action if all conditions are met
        if(AllSpeakersPresent() && mPlayerPresent && !mCoolingDown)
        {
            mPlayerPresent.mSpecialAction = mListenAction;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Player left the dialogue sphere. Conversation will continue if playing, but player will not hear it
        if (other.tag == "Player")
        {
            mPlayerPresent.mSpecialAction = null;
            mPlayerPresent = null;
            mPlayerListening = false;

            if(mDialogueManager)
            {
                mDialogueManager.HideDialogueBox();
            }
        }
        //If one of the AIs that take part in the conversation leaves
        else if(IsASpeaker(other.gameObject))
        {
            if (AllSpeakersPresent())
            {
                ToggleSpeechBubbles(false);
            }

            for (int i = 0; i < mSpeakers.Length; i++)
            {
                if (other.gameObject == mSpeakers[i])
                {
                    mSpeakerPresent[i] = false;
                }
            }

            //Check if the dialogue can still take place
            if (mPlayerPresent && !AllSpeakersPresent())
            {
                mPlayerPresent.mSpecialAction = null;
            }
        }
    }

    //Prepares everything and starts playing the dialogue
    public void PlayDialogue()
    {
        if(!mPlaying)
        {
            for(int i = 0; i < mSpeakerControllers.Length; i++)
            {
                mSpeakerControllers[i].SetTalking(true);
            }
            mPlaying = true;
            mPlayerListening = true;

            mPlayerPresent.mSpecialAction = null;
            ToggleSpeechBubbles(false);

            StartCoroutine(DialogueRunning());
        }
        //Else if the dialogue is already playing, let the player listen to it
        else
        {
            if (mDialogueManager)
            {
                mDialogueManager.ShowDialogueBox();
            }
            mPlayerListening = true;
            mPlayerPresent.mSpecialAction = null;
        }
    }

    private IEnumerator DialogueRunning()
    {
        float dialogueTime = 0f;

        foreach(Sentence line in mSentences)
        {
            //How long the line of dialogue will show calculated from the number of characters in it
            dialogueTime = (0.3f * line.text.Length) * GameManager.ManagerInstance().mGameSpeed;

            if(line.speaker <= mSpeakers.Length)
            {
                Vector3 target = mSpeakers[line.speaker - 1].transform.position;
                for (int i = 0; i < mSpeakers.Length; i++)
                {
                    if (i != (line.speaker - 1))
                    {
                        StartCoroutine(mSpeakerControllers[i].LookAtObject(target));
                    }
                }

                //Only update the UI if the player is listening
                if (mPlayerListening && mDialogueManager)
                {
                    mDialogueManager.FillDialogueBox(mSpeakers[line.speaker - 1].name, line.text, mSpeakerControllers[line.speaker - 1].mIcon);
                }
            }

            yield return new WaitForSeconds(dialogueTime);
        }

        StartCoroutine(StopDialogue());
    }

    //Stops the dialogue and enforces a cooldown before it can be triggered again
    private IEnumerator StopDialogue()
    {
        for (int i = 0; i < mSpeakerControllers.Length; i++)
        {
            mSpeakerControllers[i].SetTalking(false);
        }
        mPlaying = false;
        mPlayerListening = false;

        if (mDialogueManager)
        {
            mDialogueManager.HideDialogueBox();
        }

        mPlayerPresent.mSpecialAction = null;

        //Cooldown
        mCoolingDown = true;
        yield return new WaitForSeconds(mCooldown);
        mCoolingDown = false;

        //Give the player the listen action back if everyone is still in the dialogue sphere
        if (AllSpeakersPresent())
        {
            ToggleSpeechBubbles(true);

            if (mPlayerPresent)
            {
                mPlayerPresent.mSpecialAction = mListenAction;
            }
        }
    }

    //Returns if all the NPCs that are part of the dialogue are present in the dialogue sphere
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

    //Checks if the given gameobject is one of the NPCs set as a speaker
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

    //Retrieves the dialogue lines
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

    private void ToggleSpeechBubbles(bool show)
    {
        for(int i = 0; i < mSpeakerControllers.Length; i++)
        {
            mSpeakerControllers[i].ToggleSpeechBubble(show);
        }
    }
}
