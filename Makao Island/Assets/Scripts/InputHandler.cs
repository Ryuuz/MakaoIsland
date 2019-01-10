using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private bool mInMenu = false;

    private GameObject mPlayer;
    private GameObject mCamera;
    private GameManager mGameManager;

    private PlayerController mPlayerController;
    private CameraController mCameraController;

    private static InputHandler inputHandler;

    public static InputHandler InputInstance()
    {
        if (inputHandler == null)
        {
            GameObject manager = GameObject.Find("Manager");

            if (manager)
            {
                inputHandler = manager.AddComponent<InputHandler>();
            }
            else
            {
                manager = new GameObject("Manager");
                inputHandler = manager.AddComponent<InputHandler>();
            }
        }

        return inputHandler;
    }

    private void Awake()
    {
        if (inputHandler == null)
        {
            inputHandler = this;
        }
        else if(inputHandler != this)
        {
            Destroy(this);
        }
    }

    void Start()
    {
        mGameManager = GameManager.ManagerInstance();
        mPlayer = mGameManager.mPlayer;
        mCamera = mGameManager.mMainCamera;

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

        if(Input.GetButtonDown("Sprint"))
        {
            mPlayerController.TriggerSprint(true);
        }
        else if(Input.GetButtonUp("Sprint"))
        {
            mPlayerController.TriggerSprint(false);
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
