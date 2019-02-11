using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class AILeavingEvent : UnityEvent<GameObject>
{

}

public class AITalking : AIController
{
    public Sprite mIcon;
    public AILeavingEvent eStartedMoving = new AILeavingEvent();

    [HideInInspector]
    public bool mInDialogueSphere = false;

    [SerializeField]
    private RectTransform mSpeechBubble;

    private bool mTalking = false;

    private void Awake()
    {
        ToggleSpeechBubble(false);
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

    public void SetTalking(bool talking)
    {
        mTalking = talking;
    }

    protected override IEnumerator MoveWhenReady(Vector3 position)
    {
        //Won't move until done talking
        if (mTalking)
        {
            yield return new WaitUntil(() => mTalking == false);
        }
        else
        {
            if(mInDialogueSphere)
            {
                eStartedMoving.Invoke(gameObject);
            }
            
            if(mGameManager.mGameSpeed > 0f)
            {
                yield return new WaitForSeconds(mTransitionDelay / mGameManager.mGameSpeed);
            }
        }

        mAgent.SetDestination(position);
    }
}
