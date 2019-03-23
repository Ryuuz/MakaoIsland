using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlsUIScript : MonoBehaviour
{
    public List<ControlInfoObject> mControlsList = new List<ControlInfoObject>();

    [SerializeField]
    private Image mDefault;
    [SerializeField]
    private Image mGamepad;
    [SerializeField]
    private TextMeshProUGUI mDescription;

    private CanvasGroup mCanvasGroup;

    void Start()
    {
        //Sort list
        mControlsList.Sort((obj1, obj2) => obj1.mControlType.CompareTo(obj2.mControlType));

        mCanvasGroup = GetComponent<CanvasGroup>();
        HideControlUI();

        //Give the game manager access to object
        if(!GameManager.ManagerInstance().mControlUI)
        {
            GameManager.ManagerInstance().mControlUI = this;
        }
    }

    public void ShowControlUI(ControlAction type)
    {
        //Make sure 'type' is within range of the list
        if((int)type < mControlsList.Count && mCanvasGroup)
        {
            mDefault.overrideSprite = mControlsList[(int)type].mDefaultControl;
            mGamepad.overrideSprite = mControlsList[(int)type].mGamepadControl;
            mDescription.text = mControlsList[(int)type].mControlText;

            mCanvasGroup.alpha = 1f;
        }
    }

    public void HideControlUI()
    {
        if(mCanvasGroup)
        {
            mCanvasGroup.alpha = 0f;
        }
    }
}
