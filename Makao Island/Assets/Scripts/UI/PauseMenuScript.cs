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
    private StandaloneInputModule mModule;

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
        if (mModule && !mHidden)
        {
            if (!mDeactivated && (Input.GetAxisRaw("LookX") != 0f || Input.GetAxisRaw("LookY") != 0f))
            {
                mModule.DeactivateModule();
                mDeactivated = true;
            }
            else if (mDeactivated && (Input.GetAxis("GP Horizontal") != 0f || Input.GetAxis("GP Vertical") != 0f))
            {
                mModule.ActivateModule();
                mDeactivated = false;
            }
        }
    }

    public void HidePauseMenu()
    {
        mCanvasGroup.alpha = 0f;
        mCanvasGroup.blocksRaycasts = false;
        mCanvasGroup.interactable = false;
        mHidden = true;
    }

    public void ShowPauseMenu()
    {
        mCanvasGroup.alpha = 1f;
        mCanvasGroup.blocksRaycasts = true;
        mCanvasGroup.interactable = true;
        mHidden = false;

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

    private IEnumerator SavedGameMessage()
    {
        mSavedMessageText.text = "The game has been saved!";
        yield return new WaitForSeconds(5f);
        mSavedMessageText.text = "";
    }
}
