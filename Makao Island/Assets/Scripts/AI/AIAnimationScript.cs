using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimationScript : MonoBehaviour
{
    protected Animator mAnimator;
    protected GameManager mGameManager;

    protected virtual void Start()
    {
        mAnimator = GetComponent<Animator>();
        mGameManager = GameManager.ManagerInstance();
        mGameManager.eSpeedChanged.AddListener(SetPlaySpeed);
        SetPlaySpeed(mGameManager.mGameSpeed);
    }

    public void SetPlaySpeed(float speed)
    {
        mAnimator.SetFloat("GameSpeed", speed);
    }
}
