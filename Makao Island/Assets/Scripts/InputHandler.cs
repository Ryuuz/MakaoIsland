using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private bool mInMenu = false;
    private GameObject mPlayer;
    private GameObject mCamera;

    private PlayerController mPlayerController;
    private CameraController mCameraController;

    private void Awake()
    {
        mPlayer = GameObject.Find("Player");
        mCamera = GameObject.Find("Main Camera");
    }

    void Start()
    {
        mPlayerController = mPlayer.GetComponent<PlayerController>();
        mCameraController = mCamera.GetComponent<CameraController>();
	}
	
	void Update()
    {
		if(!mInMenu)
        {
            HandlePlayInput();
        }
        else
        {
            HandlePauseInput();
        }
	}

    private void HandlePlayInput()
    {
        mPlayerController.MovePlayer(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        mCameraController.RotateCamera(Input.GetAxisRaw("Look X"), Input.GetAxisRaw("Look Y"));

        if (Input.GetButtonDown("Jump"))
        {
            mPlayerController.TriggerJump();
        }
    }

    private void HandlePauseInput()
    {

    }

    public void TogglePause()
    {
        mInMenu = !mInMenu;
    }
}
