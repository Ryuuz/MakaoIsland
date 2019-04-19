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
        if (AllSpeakersPresent() && IsReadyToTalk())
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
        //Even if some of the speakers have signalled to leave, give the player the listen action as long as conversation is still playing
        else if (!AllSpeakersPresent() && mPlaying && !mPlayerListening && mPlayerPresent)
        {
            SetListenAction(true);
        }
    }

    private bool IsReadyToTalk()
    {
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
                //Can't talk if being guided
                if (mGuideScripts[i].mGuided)
                {
                    return false;
                }

                if (Vector3.Distance(mTalkingAIs[i].mAITransform.position, transform.position) <= GetComponent<SphereCollider>().radius)
                {
                    mTalkingAIs[i].mAIPresent = true;
                }
                else
                {
                    return false;
                }
            }
        }

        return true;
    }
}
