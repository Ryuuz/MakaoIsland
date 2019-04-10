using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour
{
    public MapManager mMapManager;
    public PauseMenuScript mPauseMenu;

    private bool mInMenu = false;
    private bool mMapOpen = false;
    private bool mInCutscene = false;
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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mGameManager = GameManager.ManagerInstance();
        mPlayer = mGameManager.mPlayer;
        mCamera = mGameManager.mMainCamera;

        if(mPlayer)
        {
            mPlayerController = mPlayer.GetComponent<PlayerController>();
        }

        if(mCamera)
        {
            mCameraController = mCamera.GetComponent<CameraController>();
            if(!mCameraController)
            {
                mCameraController = mCamera.AddComponent<CameraController>();
            }
        }
        
    }
	
	void Update()
    {
        //Different handling of input depending on whether the player is in a menu
		if(!mInMenu && !mInCutscene)
        {
            HandlePlayInput();
        }
        else if(mInMenu && !mInCutscene)
        {
            HandlePauseInput();
        }
	}

    //Handling of input while the game is running
    private void HandlePlayInput()
    {
        if(mCameraController)
        {
            mCameraController.RotateCamera(Input.GetAxisRaw("Look X"), Input.GetAxisRaw("Look Y"));
        }
        
        if(mPlayerController)
        {
            mPlayerController.SetMovementDirection(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (Input.GetButtonDown("Jump"))
            {
                mPlayerController.Jump();
            }

            if (Input.GetButtonDown("Sprint"))
            {
                mPlayerController.TriggerSprint(true);
            }
            else if (Input.GetButtonUp("Sprint"))
            {
                mPlayerController.TriggerSprint(false);
            }

            if (Input.GetButtonDown("Special"))
            {
                mPlayerController.TriggerSpecialAction(true);
            }
            else if (Input.GetButtonUp("Special"))
            {
                mPlayerController.TriggerSpecialAction(false);
            }

            if (Input.GetButtonDown("Map") && mMapManager)
            {
                if(mMapManager.mMapAvailable)
                {
                    mPlayerController.SetMovementDirection(0f, 0f);
                    mMapManager.ShowMap();
                    mMapOpen = true;
                    ToggleMenu();
                }
            }

            if (Input.GetButtonDown("Pause"))
            {
                ToggleMenu();
                mPauseMenu.ShowPauseMenu();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0f;
            }
        }
    }

    //Handling of input while the game is paused
    private void HandlePauseInput()
    {
        if (Input.GetButtonDown("Map") && mMapManager && mMapOpen)
        {
            mMapManager.HideMap();
            mMapOpen = false;
            ToggleMenu();
        }

        if (Input.GetButtonDown("Pause") && !mMapOpen)
        {
            mPauseMenu.HidePauseMenu();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
            ToggleMenu();
        }
    }

    //Pause or unpause the game
    public void ToggleMenu()
    {
        mInMenu = !mInMenu;
    }

    public void StartCutscene(float duration)
    {
        if (!mInCutscene)
        {
            StartCoroutine(CutscenePlaying(duration));
        }
    }

    private IEnumerator CutscenePlaying(float duration)
    {
        mInCutscene = true;
        if(mPlayerController)
        {
            mPlayerController.SetMovementDirection(0f, 0f);
        }
        yield return new WaitForSeconds(duration);
        mInCutscene = false;
    }
}
