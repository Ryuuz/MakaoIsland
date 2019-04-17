using System.Collections;
using UnityEngine;

public class AITalking : AIController
{
    public Sprite mIcon;

    [HideInInspector]
    public AILeavingEvent eStartedMoving = new AILeavingEvent();
    [HideInInspector]
    public bool mInDialogueSphere;

    [SerializeField]
    protected RectTransform mSpeechBubble;

    protected bool mTalking = false;

    private void Awake()
    {
        ToggleSpeechBubble(false);
        mInDialogueSphere = false;
    }

    //Show or hide the speech bubble
    public void ToggleSpeechBubble(bool show)
    {
        if(show)
        {
            mSpeechBubble.gameObject.SetActive(true);
        }
        else
        {
            mSpeechBubble.gameObject.SetActive(false);
        }
    }

    public virtual void SetTalking(bool talking)
    {
        mTalking = talking;
    }

    protected override IEnumerator MoveWhenReady(Vector3 position)
    {
        if (mInDialogueSphere)
        {
            eStartedMoving.Invoke(gameObject);
        }

        //Won't move until done talking
        if (mTalking)
        {
            yield return new WaitUntil(() => mTalking == false);
        }
        else
        {
            if(mGameManager.mGameSpeed > 0f)
            {
                yield return new WaitForSeconds(mTransitionDelay / mGameManager.mGameSpeed);
            }
        }

        mAgent.SetDestination(position);
    }

    //Is the position in range of the AI's destination
    public bool IsDestination(Vector3 target)
    {
        if(mAgent.hasPath && ((mAgent.destination - target).sqrMagnitude > (mWaypointRadius * mWaypointRadius)))
        {
            return false;
        }

        return true;
    }
}
