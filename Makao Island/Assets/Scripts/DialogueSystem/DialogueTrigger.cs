using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public int mDialogueNumber;
    public float mCooldown = 10f;
    public GameObject[] mSpeakers;

    protected AITalking[] mSpeakerControllers;
    protected bool[] mSpeakerPresent;
    protected PlayerController mPlayerPresent = null;
    protected List<Sentence> mSentences = new List<Sentence>();
    protected DialogueManager mDialogueManager;
    protected bool mPlaying = false;
    protected bool mPlayerListening = false;
    protected bool mCoolingDown = false;
    protected SpecialActionListen mListenAction;

    protected virtual void Start()
    {
        mListenAction = new SpecialActionListen(this);

        //Retrieve dialogue lines from the dialogue manager
        GameObject temp = GameManager.ManagerInstance().mDialogueManager;
        if(temp)
        {
            mDialogueManager = temp.GetComponent<DialogueManager>();
        }

        GetDialogue();

        //Get the script component of the AI
        mSpeakerControllers = new AITalking[mSpeakers.Length];
        mSpeakerPresent = new bool[mSpeakers.Length];

        for (int i = 0; i < mSpeakerControllers.Length; i++)
        {
            mSpeakerControllers[i] = mSpeakers[i].GetComponent<AITalking>();
            mSpeakerControllers[i].eStartedMoving.AddListener(AIIsLeaving);
        }

        for(int i = 0; i < mSpeakers.Length; i++)
        {
            if(Vector3.Distance(mSpeakers[i].transform.position, transform.position) <= GetComponent<SphereCollider>().radius)
            {
                mSpeakerPresent[i] = true;
                mSpeakerControllers[i].mInDialogueSphere = true;
            }
            else
            {
                mSpeakerPresent[i] = false;
            }
        }

        EvaluateStatus();
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            mPlayerPresent = other.gameObject.GetComponent<PlayerController>();
        }
        //If one of the AI belonging to the dialogue sphere enters, set its status to present
        else if(IsASpeaker(other.gameObject))
        {
            for(int i = 0; i < mSpeakers.Length; i++)
            {
                if(other.gameObject == mSpeakers[i])
                {
                    mSpeakerPresent[i] = true;
                    mSpeakerControllers[i].mInDialogueSphere = true;
                }
            }
        }

        //Give the player the listen action if all conditions are met
        EvaluateStatus();
    }

    protected void OnTriggerExit(Collider other)
    {
        //Player left the dialogue sphere. Conversation will continue if playing, but player will not hear it
        if (other.tag == "Player")
        {
            SetListenAction(false);
            mPlayerPresent = null;
            mPlayerListening = false;

            if(mDialogueManager)
            {
                mDialogueManager.HideDialogueBox();
            }
        }
    }

    public virtual void EvaluateStatus()
    {
        if (AllSpeakersPresent() && !mPlaying)
        {
            ToggleSpeechBubbles(true);
        }

        if (AllSpeakersPresent() && !mPlayerListening && mPlayerPresent && !mCoolingDown)
        {
            SetListenAction(true);
        }
        else if(!AllSpeakersPresent() && mPlaying && !mPlayerListening && mPlayerPresent)
        {
            SetListenAction(true);
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

            SetListenAction(false);
            ToggleSpeechBubbles(false);

            StartCoroutine(DialogueRunning());
        }
        //Else if the dialogue is already playing, let the player listen to it
        else if(mPlaying)
        {
            if (mDialogueManager)
            {
                mDialogueManager.ShowDialogueBox();
            }
            mPlayerListening = true;

            SetListenAction(false);
        }
    }

    protected IEnumerator DialogueRunning()
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
    protected IEnumerator StopDialogue()
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

        SetListenAction(false);

        //Cooldown
        mCoolingDown = true;
        yield return new WaitForSeconds(mCooldown);
        mCoolingDown = false;

        //Give the player the listen action back if everyone is still in the dialogue sphere
        EvaluateStatus();
    }

    //Returns if all the NPCs that are part of the dialogue are present in the dialogue sphere
    protected bool AllSpeakersPresent()
    {
        if (mSpeakerPresent.Length == 0)
        {
            return false;
        }

        for (int i = 0; i < mSpeakerPresent.Length; i++)
        {
            if (!mSpeakerPresent[i])
            {
                return false;
            }
        }

        return true;
    }

    //Checks if the given gameobject is one of the NPCs set as a speaker
    protected bool IsASpeaker(GameObject character)
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
    protected void GetDialogue()
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

    //Shows or hides the speech bubbles
    protected void ToggleSpeechBubbles(bool show)
    {
        for(int i = 0; i < mSpeakerControllers.Length; i++)
        {
            mSpeakerControllers[i].ToggleSpeechBubble(show);
        }
    }

    //The player can no longer start the conversation if one of the NPCs is leaving
    protected void AIIsLeaving(GameObject npc)
    {
        ToggleSpeechBubbles(false);

        for (int i = 0; i < mSpeakers.Length; i++)
        {
            if (npc == mSpeakers[i])
            {
                mSpeakerPresent[i] = false;
                mSpeakerControllers[i].mInDialogueSphere = false;
            }
        }

        SetListenAction(false);
    }

    protected void SetListenAction(bool listen)
    {
        if(mPlayerPresent)
        {
            if (listen)
            {
                mPlayerPresent.mSpecialAction = mListenAction;
                GameManager.ManagerInstance().mControlUI.ShowControlUI(ControlAction.listen);
            }
            else if (mPlayerPresent.mSpecialAction == mListenAction)
            {
                mPlayerPresent.mSpecialAction = null;
                GameManager.ManagerInstance().mControlUI.HideControlUI();
            }
        }
    }
}
