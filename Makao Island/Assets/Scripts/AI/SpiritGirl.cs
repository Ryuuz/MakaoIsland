using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritGirl : AITalking
{
    [SerializeField]
    private DialogueTrigger mDialogueSphere;

    private bool mGuided = false;
    private FadeScript mFade;
    private SpecialActionGuide mGuideAction;
    private PlayerController mPlayer;
    private CanvasGroup mCanvas;

    protected override void Start()
    {
        base.Start();

        mFade = GetComponent<FadeScript>();
        mGuideAction = new SpecialActionGuide(this);
        mCanvas = GetComponentInChildren<CanvasGroup>();
        mCanvas.alpha = 0;

        Transition(mGameManager.mGameStatus.mDayTime);
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

        if(talking == false)
        {
            mGuided = true;

            if(mPlayer)
            {

                mPlayer.mSpecialAction = mGuideAction;
            }
        }
        else
        {
            eStartedMoving.Invoke(gameObject);
        }
    }

    public void FollowGuide(Vector3 pos)
    {
        mAgent.stoppingDistance = 4;
        mAgent.SetDestination(pos);
    }

    private IEnumerator StartFadingIn()
    {
        yield return new WaitUntil(() => mFade.mFading == false);
        yield return StartCoroutine(mFade.FadeIn());
        mCanvas.alpha = 1f;
        if(Vector3.Distance(mDialogueSphere.gameObject.transform.position, gameObject.transform.position) <= mDialogueSphere.gameObject.GetComponent<SphereCollider>().radius)
        {
            mInDialogueSphere = true;
        }

        if (mDialogueSphere)
        {
            mDialogueSphere.EvaluateStatus();
        }
    }

    private IEnumerator StartFadingOut()
    {
        eStartedMoving.Invoke(gameObject);
        yield return new WaitUntil(() => mTalking == false);
        yield return new WaitUntil(() => mFade.mFading == false);
        mGuided = false;
        if (mPlayer)
        {
            if (mPlayer.mSpecialAction == mGuideAction)
            {
                mPlayer.mSpecialAction = null;
            }
        }
        mCanvas.alpha = 0f;
        StartCoroutine(mFade.FadeOut());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            mPlayer = other.GetComponent<PlayerController>();

            if(mGuided)
            {
                mPlayer.mSpecialAction = mGuideAction;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player" && mPlayer)
        {
            if (mPlayer.mSpecialAction == mGuideAction)
            {
                mPlayer.mSpecialAction = null;
            }

            mPlayer = null;
            mAgent.stoppingDistance = 0;
            mAgent.SetDestination(mCurrentLocation);
        }
    }
}
