using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
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

    public void HidePauseMenu()
    {
        mCanvasGroup.alpha = 0f;
        mCanvasGroup.blocksRaycasts = false;
        mCanvasGroup.interactable = false;
    }

    public void ShowPauseMenu()
    {
        mCanvasGroup.alpha = 1f;
        mCanvasGroup.blocksRaycasts = true;
        mCanvasGroup.interactable = true;
    }

    //Function for Resume button
    public void ResumeGame()
    {
        HidePauseMenu();
        Time.timeScale = 1f;
        InputHandler.InputInstance().ToggleMenu();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //Function for Quit button
    public void QuitGame()
    {
        Application.Quit();
    }
}
