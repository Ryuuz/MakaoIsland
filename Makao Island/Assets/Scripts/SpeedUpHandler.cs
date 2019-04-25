﻿using UnityEngine;

public class SpeedUpHandler : MonoBehaviour
{
    private AudioSource mAudio;
    private ParticleSystem mParticles;

    void Start()
    {
        GameManager.ManagerInstance().eSpeedChanged.AddListener(SpeedingUp);
        mAudio = GetComponent<AudioSource>();
        mParticles = GetComponent<ParticleSystem>();
    }

    public void SpeedingUp(float speed)
    {
        if(speed > 1f)
        {
            if (mAudio)
            {
                mAudio.pitch = 3f;
            }

            if (mParticles)
            {
                var main = mParticles.main;
                main.simulationSpeed = 3f;
            }
        }
        else
        {
            if (mAudio)
            {
                mAudio.pitch = 1f;
            }

            if (mParticles)
            {
                var main = mParticles.main;
                main.simulationSpeed = 1f;
            }
        }
        
    }
}