using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITalking : AIController
{
    public Sprite mIcon;

    [SerializeField]
    private RectTransform mSpeechBubble;

    private bool mTalking = false;

    private void Awake()
    {
        ToggleSpeechBubble(false);
    }

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
        if (mTalking)
        {
            yield return new WaitUntil(() => mTalking == false);
        }
        else
        {
            yield return new WaitForSeconds(mTransitionDelay / mGameManager.mGameSpeed);
        }

        mAgent.SetDestination(position);
    }
}
