using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScript : MonoBehaviour
{
    private ParticleSystem mStars;

    void Start()
    {
        GameManager tempManager = GameManager.ManagerInstance();
        mStars = GetComponent<ParticleSystem>();
        tempManager.eTimeChanged.AddListener(ToggleStars);

        if(mStars)
        {
            if (tempManager.mGameStatus.mDayTime == DayCyclus.night || tempManager.mGameStatus.mDayTime == DayCyclus.dawn)
            {
                var tempPS = mStars.main;
                tempPS.prewarm = true;
                mStars.Play();
                tempPS.prewarm = false;
            }
            if (tempManager.mGameStatus.mDayTime == DayCyclus.dawn || tempManager.mGameStatus.mDayTime == DayCyclus.dusk)
            {
                ToggleStars(tempManager.mGameStatus.mDayTime);
            }
        }
    }

    public void ToggleStars(DayCyclus day)
    {
        if(mStars)
        {
            if (day == DayCyclus.dawn)
            {
                mStars.Stop();
            }
            else if (day == DayCyclus.dusk)
            {
                mStars.Play();
            }
        }
    }
}
