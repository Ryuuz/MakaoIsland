using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    private CanvasGroup mCanvasGroup;

    void Start()
    {
        mCanvasGroup = GetComponent<CanvasGroup>();

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

    public void ResumeGame()
    {
        HidePauseMenu();
        Time.timeScale = 1f;
        InputHandler.InputInstance().ToggleMenu();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
