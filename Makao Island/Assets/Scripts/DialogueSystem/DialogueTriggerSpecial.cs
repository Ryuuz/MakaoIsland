using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerSpecial : DialogueTrigger
{
    private FollowGuideScript[] mGuideScripts;

    protected override void Start()
    {
        mGuideScripts = new FollowGuideScript[mSpeakers.Length];
        for (int i = 0; i < mGuideScripts.Length; i++)
        {
            mGuideScripts[i] = mSpeakers[i].GetComponentInChildren<FollowGuideScript>();
        }

        base.Start();
    }

    public override void EvaluateStatus()
    {
        bool readyToTalk = true;

        for (int i = 0; i < mSpeakers.Length; i++)
        {
            if (mSpeakers[i] == null)
            {
                Destroy(gameObject);
            }
            else if(mGuideScripts[i] != null)
            {
                if(mGuideScripts[i].mGuided)
                {
                    readyToTalk = false;
                }
            }
        }

        if (AllSpeakersPresent() && readyToTalk)
        {
            if(!mPlaying)
            {
                ToggleSpeechBubbles(true);
            }
            
            if(!mPlayerListening && mPlayerPresent && !mCoolingDown)
            {
                SetListenAction(true);
            }
        }
        else if (!AllSpeakersPresent() && mPlaying && !mPlayerListening && mPlayerPresent)
        {
            SetListenAction(true);
        }
    }
}
