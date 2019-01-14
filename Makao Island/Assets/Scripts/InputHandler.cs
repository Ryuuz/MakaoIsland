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

    //Singleton to insure only one instance of the class
    private static InputHandler sInputHandler;

    //Returns the instance of the class if there is one. Otherwise creates an instance
    public static InputHandler InputInstance()
    {
        if (sInputHandler == null)
        {
            GameObject manager = GameObject.Find("Manager");

            if (manager)
            {
                sInputHandler = manager.AddComponent<InputHandler>();
            }
            else
            {
                manager = new GameObject("Manager");
                sInputHandler = manager.AddComponent<InputHandler>();
            }
        }

        return sInputHandler;
    }

    private void Awake()
    {
        //Check if an instance of the class already exists
        if (sInputHandler == null)
        {
            sInputHandler = this;
        }
        else if(sInputHandler != this)
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
        //Different handling on input depending on whether the player is in a menu
		if(!mInMenu)
        {
            HandlePlayInput();
        }
        else
        {
            HandlePauseInput();
        }
	}

    //Handling of input while the game is running
    private void HandlePlayInput()
    {
        mPlayerController.SetMovementDirection(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
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

        if(Input.GetButtonDown("Special"))
        {
            Debug.Log("Special action triggered");
        }

        if(Input.GetButtonDown("Map"))
        {
            Debug.Log("Map opened");
        }

        if(Input.GetButtonDown("Pause"))
        {
            Debug.Log("Game paused");
        }
    }

    //Handling of input while the game is paused
    private void HandlePauseInput()
    {

    }

    //Pause or unpause the game
    public void TogglePause()
    {
        mInMenu = !mInMenu;
    }
}
