using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour
{
    public MapManager mMapManager;
    public PauseMenuScript mPauseMenu;
    public float mJoystickDeadzone = 0.25f;
    public bool mGamepad { get; set; } 

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
        mGamepad = false;

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
            if(Input.GetAxisRaw("LookX") != 0f || Input.GetAxisRaw("LookY") != 0f)
            {
                mCameraController.RotateCamera(new Vector2(Input.GetAxisRaw("LookX"), Input.GetAxisRaw("LookY")));
                mGamepad = false;
            }
            else if (Input.GetAxis("GP LookX") != 0f || Input.GetAxis("GP LookY") != 0f)
            {
                mCameraController.RotateCamera(JoystickInputHandler(new Vector2(Input.GetAxis("GP LookX"), Input.GetAxis("GP LookY"))));
                mGamepad = true;
            }
        }
        
        if(mPlayerController)
        {
            //Movement
            if(Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
            {
                mPlayerController.SetMovementDirection(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
                mGamepad = false;
            }
            else if(Input.GetAxis("GP Horizontal") != 0f || Input.GetAxis("GP Vertical") != 0f)
            {
                mPlayerController.SetMovementDirection(JoystickInputHandler(new Vector2(Input.GetAxis("GP Horizontal"), Input.GetAxis("GP Vertical"))));
                mGamepad = true;
            }
            else
            {
                mPlayerController.SetMovementDirection(Vector2.zero);
            }

            //Jump
            if (Input.GetButtonDown("Jump"))
            {
                mPlayerController.Jump();
                mGamepad = false;
            }
            else if(Input.GetButton("GP Jump"))
            {
                mPlayerController.Jump();
                mGamepad = true;
            }

            //Sprint
            if (Input.GetButtonDown("Sprint"))
            {
                mPlayerController.TriggerSprint(true);
                mGamepad = false;
            }
            else if (Input.GetButtonUp("Sprint"))
            {
                mPlayerController.TriggerSprint(false);
                mGamepad = false;
            }
            else if(Input.GetButtonDown("GP Sprint"))
            {
                mPlayerController.TriggerSprint(true);
                mGamepad = true;
            }
            else if(Input.GetButtonUp("GP Sprint"))
            {
                mPlayerController.TriggerSprint(false);
                mGamepad = true;
            }

            //Use special action
            if (Input.GetButtonDown("Special"))
            {
                mPlayerController.TriggerSpecialAction(true);
                mGamepad = false;
            }
            else if (Input.GetButtonUp("Special"))
            {
                mPlayerController.TriggerSpecialAction(false);
                mGamepad = false;
            }
            else if(Input.GetButtonDown("GP Special"))
            {
                mPlayerController.TriggerSpecialAction(true);
                mGamepad = true;
            }
            else if(Input.GetButtonUp("GP Special"))
            {
                mPlayerController.TriggerSpecialAction(false);
                mGamepad = true;
            }

            //Open map
            if (Input.GetButtonDown("Map") && mMapManager)
            {
                ToggleMap();
                mGamepad = false;
            }
            else if(Input.GetButtonDown("GP Map") && mMapManager)
            {
                ToggleMap();
                mGamepad = true;
            }

            //Open pause menu
            if (Input.GetButtonDown("Pause"))
            {
                TogglePause();
                mGamepad = false;
            }
            else if(Input.GetButtonDown("GP Pause"))
            {
                TogglePause();
                mGamepad = true;
            }
        }
    }

    //Handling of input while the game is paused
    private void HandlePauseInput()
    {
        if(mGamepad)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }

        //Close map
        if (Input.GetButtonDown("Map") && mMapOpen)
        {
            ToggleMap();
            mGamepad = false;
        }
        else if(Input.GetButtonDown("GP Map") && mMapOpen)
        {
            ToggleMap();
            mGamepad = true;
        }

        //Close pause menu
        if (Input.GetButtonDown("Pause") && !mMapOpen)
        {
            TogglePause();
            mGamepad = false;
        }
        else if(Input.GetButtonDown("GP Pause") && !mMapOpen)
        {
            TogglePause();
            mGamepad = true;
        }

        //Can go out of the map and pause menu on gampad by pressing B
        if(Input.GetButtonDown("Cancel"))
        {
            if(mMapOpen)
            {
                ToggleMap();
            }
            else
            {
                TogglePause();
            }

            mGamepad = true;
        }

        //Checking which input method is being used
        if(Input.GetAxisRaw("LookX") != 0f || Input.GetAxisRaw("LookY") != 0f)
        {
            mGamepad = false;
        }
        else if(Input.GetAxis("GP Horizontal") != 0f || Input.GetAxis("GP Vertical") != 0f)
        {
            mGamepad = true;
        }

        if(Input.GetButtonDown("GP Jump"))
        {
            mGamepad = true;
        }
    }

    //Deadzone for joysticks
    //http://www.third-helix.com/2013/04/12/doing-thumbstick-dead-zones-right.html
    private Vector2 JoystickInputHandler(Vector2 rawInput)
    {
        float inputMagnitude = rawInput.magnitude;

        if(inputMagnitude < mJoystickDeadzone)
        {
            return Vector2.zero;
        }
        else
        {
            return rawInput.normalized * ((inputMagnitude - mJoystickDeadzone) / (1f - mJoystickDeadzone));
        }
    }

    private void ToggleMap()
    {
        if(mMapManager.mMapAvailable)
        {
            //Opens map if not open
            if(mMapManager.mMapAvailable && !mMapOpen)
            {
                mPlayerController.SetMovementDirection(Vector2.zero);
                mMapManager.ShowMap();
                mMapOpen = true;
                mInMenu = true;
            }
            //Closes map
            else
            {
                mMapManager.HideMap();
                mMapOpen = false;
                mInMenu = false;
            }
        }
    }

    public void TogglePause()
    {
        //Closes pause menu
        if(Time.timeScale == 0f)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            mPauseMenu.HidePauseMenu();
            Time.timeScale = 1f;
            mInMenu = false;
        }
        //Opens pause menu
        else
        {
            mInMenu = true;
            mPauseMenu.ShowPauseMenu();
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
        }
    }

    public void StartCutscene(float duration)
    {
        if (!mInCutscene)
        {
            StartCoroutine(CutscenePlaying(duration));
        }
    }

    //Ignores all user inputs while in a cutscene
    private IEnumerator CutscenePlaying(float duration)
    {
        mInCutscene = true;
        if(mPlayerController)
        {
            mPlayerController.SetMovementDirection(Vector2.zero);
        }
        yield return new WaitForSeconds(duration);
        mInCutscene = false;
    }
}
