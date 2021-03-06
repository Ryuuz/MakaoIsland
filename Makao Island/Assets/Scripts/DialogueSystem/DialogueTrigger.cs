﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public int mDialogueNumber;
    public float mCooldown = 10f;
    public GameObject[] mSpeakers;

    protected DialogueManager mDialogueManager;
    protected GameManager mGameManager;
    protected SpecialActionListen mListenAction;
    protected SpecialActionSkip mSkipAction;
    protected PlayerController mPlayer;
    protected bool mPlayerPresent = false;
    protected List<Sentence> mSentences = new List<Sentence>();
    protected TalkingAIData[] mTalkingAIs;
    
    //Variables for playing and keeping track of the dialogue
    protected bool mPlaying = false;
    protected bool mPlayerListening = false;
    protected bool mCoolingDown = false;
    protected bool mSkipping = false;
    protected Coroutine mCountingDown = null;
    protected int mLineNumber = 0;
    

    protected virtual void Start()
    {
        mListenAction = new SpecialActionListen(this);
        mSkipAction = new SpecialActionSkip(this);
        mGameManager = GameManager.ManagerInstance();
        mPlayer = mGameManager.mPlayer.GetComponent<PlayerController>();

        //Retrieve dialogue lines from the dialogue manager if possible
        if(mGameManager.mDialogueManager)
        {
            mDialogueManager = mGameManager.mDialogueManager.GetComponent<DialogueManager>();
        }

        GetDialogue();

        //Get the various components of the AIs and check if the AIs are present
        mTalkingAIs = new TalkingAIData[mSpeakers.Length];

        for (int i = 0; i < mTalkingAIs.Length; i++)
        {
            mTalkingAIs[i].mTalkingScript = mSpeakers[i].GetComponent<AITalking>();
            mTalkingAIs[i].mTalkingScript.eStartedMoving.AddListener(AIIsLeaving);
            mTalkingAIs[i].mAIPresent = false;
            mTalkingAIs[i].mAnimationScript = mSpeakers[i].GetComponentInChildren<VillagerAnimationScript>();
            mTalkingAIs[i].mAITransform = mSpeakers[i].GetComponent<Transform>();

            if (PlayerPrefs.GetInt("Load", 0) == 0 && (mTalkingAIs[i].mAITransform.position - transform.position).sqrMagnitude <= Mathf.Pow(GetComponent<SphereCollider>().radius, 2))
            {
                mTalkingAIs[i].mAIPresent = true;
            }
        }

        //Check if the listen action should be given to the player
        EvaluateStatus();
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            mPlayerPresent = true;
        }
        //If one of the AIs belonging to the dialogue sphere enters, set its status to present
        else if(other.tag == "NPC")
        {
            int speakerIndex = IsASpeaker(other.gameObject);
            if(speakerIndex != -1)
            {
                //Check if this dialogue sphere is the AI's destination, or if just passing by
                if (mTalkingAIs[speakerIndex].mTalkingScript.IsDestination(transform.position))
                {
                    mTalkingAIs[speakerIndex].mAIPresent = true;
                }
            }
        }

        //Give the player the listen action if all conditions are met
        EvaluateStatus();
    }

    protected void OnTriggerExit(Collider other)
    {
        //Player left the dialogue sphere. Conversation will continue if already playing, but player will not hear it
        if (other.tag == "Player")
        {
            SetListenAction(true, false);
            SetListenAction(false, false);
            mPlayerPresent = false;
            mPlayerListening = false;

            if(mDialogueManager)
            {
                mDialogueManager.HideDialogueBox();
            }
        }
    }

    //Checks if the speech bubbles should be visible and if the player should have the listen action
    public virtual void EvaluateStatus()
    {
        if (AllSpeakersPresent() && !mPlaying)
        {
            ToggleSpeechBubbles(true);
        }

        if (AllSpeakersPresent() && !mPlayerListening && mPlayerPresent && !mCoolingDown)
        {
            SetListenAction(true, true);
        }
        else if(!AllSpeakersPresent() && mPlaying && !mPlayerListening && mPlayerPresent)
        {
            SetListenAction(true, true);
        }
    }

    //Prepares everything and starts playing the dialogue
    public void PlayDialogue()
    {
        if(!mPlaying)
        {
            for(int i = 0; i < mTalkingAIs.Length; i++)
            {
                mTalkingAIs[i].mTalkingScript.SetTalking(true);
            }
            mPlaying = true;
            mPlayerListening = true;

            SetListenAction(true, false);
            SetListenAction(false, true);
            ToggleSpeechBubbles(false);

            StartCoroutine(DialogueRunning());
        }
        //Else if the dialogue is already playing, let the player listen to it
        else if(mPlaying)
        {
            if (mDialogueManager)
            {
                mDialogueManager.FillDialogueBox(mSpeakers[mSentences[mLineNumber].speaker - 1].name, mSentences[mLineNumber].text, mTalkingAIs[mSentences[mLineNumber].speaker - 1].mTalkingScript.mIcon);
            }
            mPlayerListening = true;

            SetListenAction(true, false);
            SetListenAction(false, true);
        }
    }

    protected IEnumerator DialogueRunning()
    {
        float dialogueTime = 0f;
        LookAtListeners();

        foreach (Sentence line in mSentences)
        {
            //How long the line of dialogue will show calculated from the number of characters in it
            dialogueTime = (2f + (0.1f * line.text.Length));

            //Speed up the dialogue if time is sped up
            if(GameManager.ManagerInstance().mGameSpeed > 1f)
            {
                dialogueTime *= 4f;
            }

            if(line.speaker <= mSpeakers.Length)
            {
                //Have the other NPCs look at the one that is talking
                Vector3 target = mTalkingAIs[line.speaker - 1].mAITransform.position;
                for (int i = 0; i < mSpeakers.Length; i++)
                {
                    if (i != (line.speaker - 1))
                    {
                        StartCoroutine(mTalkingAIs[i].mTalkingScript.LookAtObject(target));
                    }
                }

                //Play the animation associated with the line if there is one
                if(mTalkingAIs[line.speaker - 1].mAnimationScript && line.gesture != TalkAnimation.none)
                {
                    mTalkingAIs[line.speaker - 1].mAnimationScript.PlayTalkAnimation(line.gesture);
                }

                //Only update the UI if the player is listening
                if (mPlayerListening && mDialogueManager)
                {
                    mDialogueManager.FillDialogueBox(mSpeakers[line.speaker - 1].name, line.text, mTalkingAIs[line.speaker - 1].mTalkingScript.mIcon);
                }
            }

            yield return 0; //Wait for end of frame to avoid registering input more than once

            mCountingDown = StartCoroutine(SkipCountdown(dialogueTime));
            yield return new WaitUntil(() => mSkipping == true);
            mSkipping = false;
            mLineNumber++;
        }

        StartCoroutine(StopDialogue());
    }

    //Stops the dialogue and enforces a cooldown before it can be triggered again
    protected IEnumerator StopDialogue()
    {
        SetListenAction(false, false);

        for (int i = 0; i < mTalkingAIs.Length; i++)
        {
            mTalkingAIs[i].mTalkingScript.SetTalking(false);
        }

        if (mDialogueManager && mPlayerListening)
        {
            mDialogueManager.HideDialogueBox();
        }

        //Reset variables
        mLineNumber = 0;
        mCountingDown = null;
        mPlaying = false;
        mPlayerListening = false;
        SetListenAction(true, false);

        //Cooldown
        mCoolingDown = true;
        yield return new WaitForSeconds(mCooldown);
        mCoolingDown = false;

        //Give the player the listen action back if everyone is still in the dialogue sphere
        EvaluateStatus();
    }

    //Skipping will be set to true after the assigned time
    protected IEnumerator SkipCountdown(float countdown)
    {
        yield return new WaitForSeconds(countdown);
        if(!mSkipping)
        {
            mSkipping = true;
        }
    }

    //Returns true if all the NPCs that are part of the dialogue are present in the dialogue sphere
    protected bool AllSpeakersPresent()
    {
        if (mTalkingAIs.Length == 0)
        {
            return false;
        }

        for (int i = 0; i < mTalkingAIs.Length; i++)
        {
            if (!mTalkingAIs[i].mAIPresent)
            {
                return false;
            }
        }

        return true;
    }

    //Checks if the given gameobject is one of the NPCs set as a speaker
    protected int IsASpeaker(GameObject character)
    {
        for(int i = 0; i < mSpeakers.Length; i++)
        {
            if(character == mSpeakers[i])
            {
                return i;
            }
        }

        return -1;
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
        for(int i = 0; i < mTalkingAIs.Length; i++)
        {
            mTalkingAIs[i].mTalkingScript.ToggleSpeechBubble(show);
        }
    }

    //The player can no longer start the conversation if one of the NPCs is leaving
    protected void AIIsLeaving(GameObject npc)
    {
        ToggleSpeechBubbles(false);

        int speakerIndex = IsASpeaker(npc);

        if(speakerIndex != -1 && mTalkingAIs[speakerIndex].mAIPresent)
        {
            mTalkingAIs[speakerIndex].mAIPresent = false;
        }

        SetListenAction(true, false);
    }

    //Gives or takes the listen or skip action from the player depending on the parameters
    protected void SetListenAction(bool listen, bool give)
    {
        if(mPlayerPresent)
        {
            SpecialActionObject tempAction;
            ControlAction tempControl;

            if (listen)
            {
                tempAction = mListenAction;
                tempControl = ControlAction.listen;
            }
            else
            {
                tempAction = mSkipAction;
                tempControl = ControlAction.skip;
            }

            if(give)
            {
                mPlayer.mSpecialAction = tempAction;
                mGameManager.mControlUI.ShowControlUI(tempControl);
            }
            else if(mPlayer.mSpecialAction == tempAction)
            {
                mPlayer.mSpecialAction = null;
                mGameManager.mControlUI.HideControlUI();
            }
        }
    }

    //Clear the current dialogue and retrieve a new one
    public void SwapDialogue(int dialogueNumber)
    {
        mSentences.Clear();
        mDialogueNumber = dialogueNumber;
        GetDialogue();
    }

    //Skip the current line
    public void SkipDialogue()
    {
        if (mPlaying && mPlayerListening && !mSkipping && mCountingDown != null)
        {
            mSkipping = true;
            StopCoroutine(mCountingDown);
        }
    }

    //The AI talking first will look at the listeners
    private void LookAtListeners()
    {
        Vector3 lookPosition = new Vector3();
        int minListener = 1;
        float minDistance = 100f;
        int maxListener = 1;
        float maxDistance = 0f;
        float distance = 0f;

        //Must be at least one listener (a total of two speakers including the one currently speaking)
        if(mTalkingAIs.Length > 1)
        {
            for (int i = 0; i < mTalkingAIs.Length; i++)
            {
                //Find the AIs that are closest and furthest from the speaker
                if ((mSentences[0].speaker - 1) != i)
                {
                    distance = (mTalkingAIs[mSentences[0].speaker - 1].mAITransform.position - mTalkingAIs[i].mAITransform.position).sqrMagnitude;
                    if (distance <= minDistance)
                    {
                        minListener = i;
                        minDistance = distance;
                    }
                    if(distance >= maxDistance)
                    {
                        maxListener = i;
                        maxDistance = distance;
                    }
                }
            }

            //If it's the same AI for both cases (suggesting only one other AI in addition to speaker)
            if(minListener == maxListener)
            {
                StartCoroutine(mTalkingAIs[mSentences[0].speaker - 1].mTalkingScript.LookAtObject(mTalkingAIs[minListener].mAITransform.position));
            }
            //Else look at a point between the max and min AI
            else
            {
                lookPosition = mTalkingAIs[maxListener].mAITransform.position - mTalkingAIs[minListener].mAITransform.position;
                lookPosition = mTalkingAIs[minListener].mAITransform.position + (lookPosition.normalized * (lookPosition.magnitude * 0.5f));
                StartCoroutine(mTalkingAIs[mSentences[0].speaker - 1].mTalkingScript.LookAtObject(lookPosition));
            }
        }
    }
}
