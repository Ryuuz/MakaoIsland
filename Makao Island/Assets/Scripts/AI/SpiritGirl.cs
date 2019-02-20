using System.Collections;
using System.Collections.Generic;
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

        if(talking == false && !mFollow.mGoalReached)
        {
            mFollow.mGuided = true;
            mFollow.SetGuideAction(true);
        }
        else if(talking == false && mFollow.mGoalReached)
        {
            StartCoroutine(RestInPeace());
        }
        else
        {
            eStartedMoving.Invoke(gameObject);
        }
    }

    private IEnumerator StartFadingIn()
    {
        yield return new WaitUntil(() => mFade.mFading == false);
        yield return StartCoroutine(mFade.FadeIn());
        if (mCanvas)
        {
            mCanvas.alpha = 1f;
        }
        mFollow.mGoalReached = false;

        if (mDialogueSphere)
        {
            mInDialogueSphere = true;
            mDialogueSphere.EvaluateStatus();
        }
    }

    private IEnumerator StartFadingOut()
    {
        eStartedMoving.Invoke(gameObject);
        yield return new WaitUntil(() => mTalking == false);
        yield return new WaitUntil(() => mFade.mFading == false);
        mFollow.mGuided = false;
        mFollow.SetGuideAction(false);
        if (mCanvas)
        {
            mCanvas.alpha = 0f;
        }
        StartCoroutine(mFade.FadeOut());
    }

    private IEnumerator RestInPeace()
    {
        eStartedMoving.Invoke(gameObject);
        yield return StartCoroutine(mFade.FadeOut());
        Destroy(gameObject);
    }
}
