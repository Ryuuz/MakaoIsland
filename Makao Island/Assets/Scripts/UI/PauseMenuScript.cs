using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class PauseMenuScript : MonoBehaviour
{
    [SerializeField]
    private Button mSelectedButton;
    [SerializeField]
    private TextMeshProUGUI mSavedMessageText;
    [SerializeField]
    private EventSystem mEventSystem;
    [SerializeField]
    private AudioSource mBackgroundMusic;

    private bool mDeactivated = false;
    private bool mHidden = true;
    private CanvasGroup mCanvasGroup;

    void Start()
    {
        mCanvasGroup = GetComponent<CanvasGroup>();

        //Give the input handler access to this script if needed
        if(InputHandler.InputInstance().mPauseMenu == null)
        {
            InputHandler.InputInstance().mPauseMenu = this;
        }

        HidePauseMenu();
    }

    private void Update()
    {
        if (mEventSystem && !mHidden)
        {
            if (!mDeactivated && (Input.GetAxisRaw("LookX") != 0f || Input.GetAxisRaw("LookY") != 0f))
            {
                mEventSystem.SetSelectedGameObject(null);
                mDeactivated = true;
            }
            else if (mDeactivated && (Input.GetAxisRaw("GP Horizontal") != 0f || Input.GetAxisRaw("GP Vertical") != 0f))
            {
                mSelectedButton.Select();
                mDeactivated = false;
            }
        }
    }

    public void HidePauseMenu()
    {
        if(mCanvasGroup)
        {
            mCanvasGroup.alpha = 0f;
            mCanvasGroup.blocksRaycasts = false;
            mCanvasGroup.interactable = false;
            mHidden = true;
        }
    }

    public void ShowPauseMenu()
    {
        if(mCanvasGroup)
        {
            mCanvasGroup.alpha = 1f;
            mCanvasGroup.blocksRaycasts = true;
            mCanvasGroup.interactable = true;
            mHidden = false;
        }

        if(mSelectedButton)
        {
            mSelectedButton.Select();
        }
    }

    //Function for Resume button
    public void ResumeGame()
    {
        InputHandler.InputInstance().TogglePause();
    }

    //Saves the game data
    public void SaveCurrentGame()
    {
        GameManager.ManagerInstance().StoreData();
        StartCoroutine(SavedGameMessage());
    }

    //Return to the main menu
    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void MuteMusic()
    {
        if(mBackgroundMusic)
        {
            if(mBackgroundMusic.isPlaying)
            {
                mBackgroundMusic.Stop();
            }
            else
            {
                mBackgroundMusic.Play();
            }
        }
    }

    private IEnumerator SavedGameMessage()
    {
        mSavedMessageText.text = "The game has been saved!";
        yield return new WaitForSecondsRealtime(5f);
        mSavedMessageText.text = "";
    }
}
