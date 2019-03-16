using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public DialogueLines mDialogueLines = new DialogueLines();

    [SerializeField]
    private Text mNameField;
    [SerializeField]
    private Text mTextField;
    [SerializeField]
    private Image mIconField;

    private bool mHidden;
    private CanvasGroup mCanvasGroup;

    private void Awake()
    {
        if(GameManager.ManagerInstance().mDialogueManager == null)
        {
            GameManager.ManagerInstance().mDialogueManager = gameObject;
        }

        mCanvasGroup = GetComponent<CanvasGroup>();
        mCanvasGroup.blocksRaycasts = false;
        mCanvasGroup.interactable = false;
        HideDialogueBox();
    }

    //Fill the dialogue box with the relevant data
    public void FillDialogueBox(string name, string text, Sprite image)
    {
        if(mHidden)
        {
            ShowDialogueBox();
        }

        mNameField.text = name;
        mTextField.text = text;
        mIconField.sprite = image;
    }

    public void ShowDialogueBox()
    {
        if(mCanvasGroup)
        {
            mCanvasGroup.alpha = 1f;
            mHidden = false;
        }
    }

    public void HideDialogueBox()
    {
        if(mCanvasGroup)
        {
            mCanvasGroup.alpha = 0f;
            mHidden = true;
        }
    }
}
