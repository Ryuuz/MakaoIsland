using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public float mMaxSpeed = 10f;
    public float mJumpForce = 5f;
    public float mMaxFallSpeed = 20f;
    public float mTurnSpeed = 2f;

    private Vector3 mMovementDirection;
    private float mCurrentSpeed = 0f;
    private float mCurrentFallSpeed = 0f;
    private CharacterController mCharacterController;

    private void Awake()
    {
        
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void Jump()
    {

    }

    void MoveCharacter()
    {

    }

    void MoveCharacter(Vector3 pos)
    {

    }

    void RotateCharacter()
    {

    }

    void Gravity()
    {

    }
}
