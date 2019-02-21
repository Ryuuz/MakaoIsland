using System.Collections;
using System.Collections.Generic;
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
    private Text mDescription;

    private CanvasGroup mCanvasGroup;

    void Start()
    {
        //Sort list
        mControlsList.Sort((obj1, obj2) => obj1.mControlType.CompareTo(obj2.mControlType));

        mCanvasGroup = GetComponent<CanvasGroup>();
        HideControlUI();

        if(!GameManager.ManagerInstance().mControlUI)
        {
            GameManager.ManagerInstance().mControlUI = this;
        }
    }

    public void ShowControlUI(ControlAction type)
    {
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
