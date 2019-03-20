using UnityEngine;

public class StarScript : MonoBehaviour
{
    private ParticleSystem mStars;

    void Start()
    {
        GameManager tempManager = GameManager.ManagerInstance();
        mStars = GetComponent<ParticleSystem>();
        tempManager.eTimeChanged.AddListener(ToggleStars);
        DayCyclus day = (DayCyclus)tempManager.mData.mDayTime;

        if(mStars)
        {
            //The stars should be fully visible if night or dawn
            if (day == DayCyclus.night || day == DayCyclus.dawn)
            {
                var tempPS = mStars.main;
                tempPS.prewarm = true;
                mStars.Play();
                tempPS.prewarm = false;
            }
            if (day == DayCyclus.dawn || day == DayCyclus.dusk)
            {
                ToggleStars(day);
            }
        }
    }

    public void ToggleStars(DayCyclus day)
    {
        //Stop or start the particle system depending on the time of day
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
