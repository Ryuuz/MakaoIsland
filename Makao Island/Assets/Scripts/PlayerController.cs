﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float mWalkSpeed = 5f;
    public float mRunSpeed = 10f;

    private Vector3 mDirection = Vector3.zero;
    private CharacterMovement mMovementController;

    private void Awake()
    {
        mMovementController = GetComponent<CharacterMovement>();
    }

    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
        mMovementController.SetSpeed(mWalkSpeed);
    }
	
	void Update()
    {

	}

    public void MovePlayer(float horizontal, float vertical)
    {
        mDirection = transform.TransformDirection(new Vector3(horizontal, 0f, vertical));
        mMovementController.SetDirection(mDirection);
    }

    public void TriggerJump()
    {
        mMovementController.Jump();
    }

    public void TriggerSprint(bool sprinting)
    {
        if(sprinting)
        {
            mMovementController.SetSpeed(mRunSpeed);
        }
        else
        {
            mMovementController.SetSpeed(mWalkSpeed);
        }
    }
}
