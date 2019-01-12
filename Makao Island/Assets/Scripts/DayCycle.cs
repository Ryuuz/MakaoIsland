using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    public float dawnLength = 10f;
    public float dayLength = 20f;
    public float duskLength = 10f;
    public float nightLength = 20f;

    private float currentTime;
    private DayCyclus currentCyclusStep;
    private float currentCyclusTime;
    private float currentRotation;
    private float timeStep;
    private float timeOfDay;

    enum DayCyclus
    {
        dawn, day, dusk, night
    };

	void Start()
    {
        currentCyclusStep = DayCyclus.dawn;
        currentTime = 0f;
        timeOfDay = 0f;
        currentCyclusTime = CyclusStepLength();
        timeStep = 360 / (dawnLength + dayLength + duskLength + nightLength);
        currentRotation = 0f;
    }
	
	void Update()
    {
        currentTime += Time.deltaTime;
        timeOfDay += Time.deltaTime;
        currentRotation = ((timeStep) * timeOfDay) % 360;
        transform.eulerAngles = new Vector3(currentRotation, 0f, 0f);

        if(currentTime >= currentCyclusTime)
        {
            currentCyclusStep = NextCyclusStep();
            currentCyclusTime = CyclusStepLength();
            currentTime = 0f;

            if(currentCyclusStep == DayCyclus.dawn)
            {
                timeOfDay = 0f;
            }
        }
	}

    float CyclusStepLength()
    {
        switch(currentCyclusStep)
        {
            case DayCyclus.dawn: return dawnLength;
            case DayCyclus.day: return dayLength;
            case DayCyclus.dusk: return duskLength;
            case DayCyclus.night: return nightLength;
        }

        return 0f;
    }

    DayCyclus NextCyclusStep()
    {
        switch (currentCyclusStep)
        {
            case DayCyclus.dawn: return DayCyclus.day;
            case DayCyclus.day: return DayCyclus.dusk;
            case DayCyclus.dusk: return DayCyclus.night;
            case DayCyclus.night: return DayCyclus.dawn;
        }

        return DayCyclus.dawn;
    }
}
