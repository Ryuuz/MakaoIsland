using System.Collections;
using UnityEngine;

public class SpiritGirl : AITalking
{
    [SerializeField]
    private DialogueTrigger mDialogueSphere;
    [SerializeField]
    private GameObject mMapTutorial;

    private FollowGuideScript mFollow;
    private FadeScript mFade;
    private GameObject mPlayer;
    private CanvasGroup mCanvas;

    protected override void Start()
    {
        base.Start();

        if (mGameManager.mData.mSpiritGirlStatus)
        {
            Destroy(transform.parent.gameObject);
        }

        mFollow = GetComponentInChildren<FollowGuideScript>();
        mPlayer = mGameManager.mPlayer;

        mCanvas = mSpeechBubble.GetComponent<CanvasGroup>();
        if(mCanvas)
        {
            mCanvas.alpha = 0f;
        }

        mFade = GetComponent<FadeScript>();
        if (!mFade)
        {
            mFade = gameObject.AddComponent<FadeScript>();
        }

        Transition((DayCyclus)mGameManager.mData.mDayTime);
    }

    protected override void SetNewDestination(Transform position)
    {
        //If faded out then fade in if a position is set
        if (position)
        {
            if(!mFade.mFadedIn && !mFade.mFading)
            {
                mCurrentLocation = position.position;
                mTransform.position = mCurrentLocation;
                StartCoroutine(StartFadingIn());
            }
        }
        //Else if faded in and no position, fade out
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

        mFollow.mGoalReached = false;

        if (mDialogueSphere)
        {
            mDialogueSphere.EvaluateStatus();
        }
        
        //Make the speech bubble visible
        if (mCanvas)
        {
            mCanvas.alpha = 1f;
        }
    }

    private IEnumerator StartFadingOut()
    {
        //Hide speech bubble
        if (mCanvas)
        {
            mCanvas.alpha = 0f;
        }

        eStartedMoving.Invoke(gameObject);

        //Make sure to not fade out while still speaking or while already in a fading phase
        yield return new WaitUntil(() => mTalking == false);
        yield return new WaitUntil(() => mFade.mFading == false);

        //Can no longer be guided
        mFollow.mGuided = false;
        mFollow.SetGuideAction(false);
        StartCoroutine(mFade.FadeOut());
    }

    //Makes the AI fade out and disappear for good
    private IEnumerator RestInPeace()
    {
        eStartedMoving.Invoke(gameObject);
        mGameManager.FoundSpiritGirl();

        //Fade out sound if there is one
        FadeOutSound soundTemp = GetComponent<FadeOutSound>();
        if(soundTemp)
        {
            soundTemp.StartFadingSound();
        }

        yield return StartCoroutine(mFade.FadeOut());

        //Remind player to check the map
        if (mMapTutorial)
        {
            Instantiate(mMapTutorial, mPlayer.transform.position, Quaternion.identity);
        }
        mPlayer.GetComponent<AudioSource>().Play();

        Destroy(gameObject);
    }
}
