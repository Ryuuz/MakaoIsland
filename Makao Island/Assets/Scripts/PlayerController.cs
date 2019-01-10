using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float mSpeed = 5f;
    private Vector3 mDirection = Vector3.zero;
    private CharacterMovement mMovementController;

    private void Awake()
    {
        mMovementController = GetComponent<CharacterMovement>();
    }

    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
        mMovementController.SetSpeed(mSpeed);
    }
	
	void Update()
    {

	}

    public void MovePlayer(float horizontal, float vertical)
    {
        mDirection = transform.TransformDirection(new Vector3(horizontal, 0f, vertical));
        //mDirection.Normalize();
        mMovementController.SetDirection(mDirection);
    }

    public void TriggerJump()
    {
        mMovementController.Jump();
    }
}
