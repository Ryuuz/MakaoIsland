using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public float mMaxSpeed = 10f;
    public float mJumpForce = 5f;
    public float mMaxFallSpeed = 20f;
    public float mTurnSpeed = 2f;

    private Vector3 mMovementDirection = Vector3.zero;
    private float mCurrentSpeed = 0f;
    private float mCurrentFallSpeed = 0f;
    private CharacterController mCharacterController;

    private void Awake()
    {
        mCharacterController = GetComponent<CharacterController>();
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Gravity();
        MoveCharacter();
	}

    void Jump()
    {
        if(mCharacterController.isGrounded)
        {
            mCurrentFallSpeed += mJumpForce;
        }
    }

    void MoveCharacter()
    {
        mCharacterController.Move(((mMovementDirection * mCurrentSpeed) + new Vector3(0f, mCurrentFallSpeed, 0f)) * Time.deltaTime);
    }

    void RotateCharacter()
    {

    }

    void Gravity()
    {
        if(!mCharacterController.isGrounded)
        {
            mCurrentFallSpeed -= Physics.gravity.y * Time.deltaTime;

            mCurrentFallSpeed = Mathf.Max(mCurrentFallSpeed, mMaxFallSpeed);
            mCurrentFallSpeed = Mathf.Min(mCurrentFallSpeed, -mMaxFallSpeed);
        }
        else if(mCharacterController.isGrounded && mCurrentFallSpeed != 0f)
        {
            mCurrentFallSpeed = 0f;
        }
    }

    public void SetSpeed(float speed)
    {
        mCurrentSpeed = speed;

        mCurrentSpeed = Mathf.Max(mCurrentSpeed, mMaxSpeed);
        mCurrentSpeed = Mathf.Min(mCurrentSpeed, 0f);
    }

    public void SetDirection(Vector3 direction)
    {
        mMovementDirection = direction;
    }
}
