using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float mJumpForce = 5f;
    public float mMaxFallSpeed = 10f;

    private Vector3 mMovementDirection = Vector3.zero;
    private float mCurrentSpeed = 0f;
    private float mCurrentFallSpeed = 0f;
    private CharacterController mCharacterController;

    private void Awake()
    {
        mCharacterController = GetComponent<CharacterController>();
    }
	
	void Update()
    {
        Gravity();
        MoveCharacter();
	}

    //Moves the character with different speed depending on whether it's on the ground or in the air
    void MoveCharacter()
    {
        if(mCharacterController.isGrounded)
        {
            mCharacterController.Move(((mMovementDirection * mCurrentSpeed) + new Vector3(0f, mCurrentFallSpeed, 0f)) * Time.deltaTime);
        }
        else
        {
            mCharacterController.Move(((mMovementDirection * (mCurrentSpeed*0.6f)) + new Vector3(0f, mCurrentFallSpeed, 0f)) * Time.deltaTime);
        }
    }

    //Applies gravity to the character
    void Gravity()
    {
        if(!mCharacterController.isGrounded)
        {
            mCurrentFallSpeed += Physics.gravity.y * Time.deltaTime;
            mCurrentFallSpeed = Mathf.Max(mCurrentFallSpeed, -mMaxFallSpeed);
        }
    }

    //Rotates the character to the given rotation
    public void RotateCharacter(Quaternion charRotation)
    {
        transform.rotation = charRotation;
    }

    //Makes the character jump if grounded
    public void Jump()
    {
        if (mCharacterController.isGrounded)
        {
            mCurrentFallSpeed = mJumpForce;
        }
    }

    //Sets the speed and makes sure it's within its limits
    public void SetSpeed(float speed)
    {
        mCurrentSpeed = speed;
    }

    //Sets the direction to move in
    public void SetDirection(Vector3 direction)
    {
        mMovementDirection = direction;
    }
}
