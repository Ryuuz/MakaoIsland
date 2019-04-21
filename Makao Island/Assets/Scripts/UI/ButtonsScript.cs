using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ButtonsScript : MonoBehaviour
{
    [SerializeField]
    private Button mLoadButton;
    [SerializeField]
    private EventSystem mEventSystem;

    private bool mDeactivated = false;

    void Start()
    {
        //Make sure the cursor is visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f;

        string path = Application.persistentDataPath + "/gamedata.dat";

        //Set the load game button to inactive if there is no game to load or if there is no assigned button
        if (File.Exists(path) && mLoadButton)
        {
            mLoadButton.interactable = true;
        }
    }

    private void Update()
    {
        if(mEventSystem)
        {
            if (!mDeactivated && (Input.GetAxisRaw("LookX") != 0f || Input.GetAxisRaw("LookY") != 0f))
            {
                mEventSystem.SetSelectedGameObject(null);
                mDeactivated = true;
            }
            else if (mDeactivated && (Input.GetAxis("GP Horizontal") != 0f || Input.GetAxis("GP Vertical") != 0f))
            {
                mLoadButton.Select();
                mDeactivated = false;
            }
        }
    }

    //Start a new game
    public void NewGame()
    {
        PlayerPrefs.SetInt("Load", 0);
        SceneManager.LoadScene(1);
    }

    //Load an existing game
    public void LoadGame()
    {
        PlayerPrefs.SetInt("Load", 1);
        SceneManager.LoadScene(1);
    }

    //Go to the main menu
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    //Quit the application
    public void QuitGame()
    {
        Application.Quit();
    }
}
