using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Vector3 mDirection = Vector3.zero;
    
    private CharacterMovement mMovementController;

    private void Awake()
    {
        mMovementController = GetComponent<CharacterMovement>();
    }

    // Use this for initialization
    void Start ()
    {
        mMovementController.SetSpeed(5f);
    }
	
	// Update is called once per frame
	void Update ()
    {
        mDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        mDirection = transform.TransformDirection(mDirection);
        mMovementController.SetDirection(mDirection);

        if(Input.GetButtonDown("Jump"))
        {
            mMovementController.Jump();
        }
	}
}
