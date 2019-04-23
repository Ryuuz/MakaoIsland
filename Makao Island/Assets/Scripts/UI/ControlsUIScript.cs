using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class ControlsUIScript : MonoBehaviour
{
    public List<ControlInfoObject> mControlsList = new List<ControlInfoObject>();

    [SerializeField]
    private Image mDefault;
    [SerializeField]
    private TextMeshProUGUI mDescription;

    private CanvasGroup mCanvasGroup;
    private InputHandler mInputHandler;
    private ControlAction mCurrentType = ControlAction.actions;
    private bool mShowingGamepad = false;
    private bool mActive = false;

    private void Awake()
    {
        mCanvasGroup = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        //Sort list
        mControlsList.Sort((obj1, obj2) => obj1.mControlType.CompareTo(obj2.mControlType));

        mInputHandler = InputHandler.InputInstance();
        HideControlUI();

        //Give the game manager access to the object
        if(!GameManager.ManagerInstance().mControlUI)
        {
            GameManager.ManagerInstance().mControlUI = this;
        }
    }

    private void Update()
    {
        if(mActive && (int)mCurrentType < mControlsList.Count)
        {
            if (mInputHandler.mGamepad && !mShowingGamepad)
            {
                mDefault.overrideSprite = mControlsList[(int)mCurrentType].mGamepadControl;
                mShowingGamepad = true;
            }
            else if (!mInputHandler.mGamepad && mShowingGamepad)
            {
                mDefault.overrideSprite = mControlsList[(int)mCurrentType].mDefaultControl;
                mShowingGamepad = false;
            }
        }
    }

    public void ShowControlUI(ControlAction type)
    {
        //Make sure 'type' is within range of the list
        if((int)type < mControlsList.Count)
        {
            mCurrentType = type;

            if(mInputHandler && mInputHandler.mGamepad)
            {
                mDefault.overrideSprite = mControlsList[(int)type].mGamepadControl;
                mShowingGamepad = true;
            }
            else
            {
                mDefault.overrideSprite = mControlsList[(int)type].mDefaultControl;
                mShowingGamepad = false;
            }
            mDescription.text = mControlsList[(int)type].mControlText;

            if(mCanvasGroup)
            {
                mCanvasGroup.alpha = 1f;
                mActive = true;
            }
        }
    }

    public void HideControlUI()
    {
        if(mCanvasGroup)
        {
            mCanvasGroup.alpha = 0f;
            mActive = false;
        }
    }
}
