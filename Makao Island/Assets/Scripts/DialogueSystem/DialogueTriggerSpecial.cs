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

        //Checks that all speakers exist and if they are ready to talk
        for (int i = 0; i < mSpeakers.Length; i++)
        {
            //If one of the speakers are no longer in the game, this dialogue sphere is obsolete
            if (mSpeakers[i] == null)
            {
                Destroy(gameObject);
            }
            else
            {
                if (Vector3.Distance(mSpeakers[i].transform.position, transform.position) <= GetComponent<SphereCollider>().radius)
                {
                    mSpeakerPresent[i] = true;
                    mSpeakerControllers[i].mInDialogueSphere = true;
                }
                if (mGuideScripts[i].mGuided)
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
