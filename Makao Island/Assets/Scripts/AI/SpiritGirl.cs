using System.Collections;
using UnityEngine;

public class SpiritGirl : AITalking
{
    [SerializeField]
    private DialogueTrigger mDialogueSphere;

    private FollowGuideScript mFollow;
    private FadeScript mFade;
    private PlayerController mPlayer;
    private CanvasGroup mCanvas;

    protected override void Start()
    {
        base.Start();

        mFade = GetComponent<FadeScript>();
        mFollow = GetComponentInChildren<FollowGuideScript>();
        mCanvas = mSpeechBubble.GetComponent<CanvasGroup>();

        if (!mFade)
        {
            mFade = gameObject.AddComponent<FadeScript>();
        }

        Transition(mGameManager.mGameStatus.mDayTime);

        if(mCanvas)
        {
            mCanvas.alpha = 0f;
        }
    }

    public override void Transition(DayCyclus time)
    {
        Transform pos = null;

        switch (time)
        {
            case DayCyclus.dawn:
                pos = mDawnLocation;
                break;

            case DayCyclus.day:
                pos = mDayLocation;
                break;

            case DayCyclus.dusk:
                pos = mDuskLocation;
                break;

            case DayCyclus.night:
                pos = mNightLocation;
                break;
        }

        if (pos)
        {
            if(!mFade.mFadedIn && !mFade.mFading)
            {
                mCurrentLocation = pos.position;
                transform.position = mCurrentLocation;
                StartCoroutine(StartFadingIn());
            }
        }
        else
        {
            if(mFade.mFadedIn && !mFade.mFading)
            {
                StartCoroutine(StartFadingOut());
            }
        }
    }

    public override void SetTalking(bool talking)
    {
        base.SetTalking(talking);

        //If done talking but not at the goal let the player guide the NPC
        if(talking == false && !mFollow.mGoalReached)
        {
            mFollow.mGuided = true;
            mFollow.SetGuideAction(true);
        }
        //Else if done talking and at the goal then request has been complete
        else if(talking == false && mFollow.mGoalReached)
        {
            StartCoroutine(RestInPeace());
        }
        //Else if started talking, make the NPC unavailable to the dialogue sphere
        else
        {
            eStartedMoving.Invoke(gameObject);
        }
    }

    private IEnumerator StartFadingIn()
    {
        yield return new WaitUntil(() => mFade.mFading == false);
        yield return StartCoroutine(mFade.FadeIn());

        //Make the speech bubble visible and reset variables
        mFollow.mGoalReached = false;

        if (mDialogueSphere)
        {
            mInDialogueSphere = true;
            mDialogueSphere.EvaluateStatus();
        }

        if (mCanvas)
        {
            mCanvas.alpha = 1f;
        }
    }

    private IEnumerator StartFadingOut()
    {
        if (mCanvas)
        {
            mCanvas.alpha = 0f;
        }

        eStartedMoving.Invoke(gameObject);
        yield return new WaitUntil(() => mTalking == false);
        yield return new WaitUntil(() => mFade.mFading == false);

        //Can no longer be guided
        mFollow.mGuided = false;
        mFollow.SetGuideAction(false);
        StartCoroutine(mFade.FadeOut());
    }

    private IEnumerator RestInPeace()
    {
        eStartedMoving.Invoke(gameObject);
        yield return StartCoroutine(mFade.FadeOut());
        Destroy(gameObject);
    }
}
