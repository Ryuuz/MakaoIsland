using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(CanvasGroup))]
public class DialogueManager : MonoBehaviour
{
    public DialogueLines mDialogueLines = new DialogueLines();

    [SerializeField]
    private TextMeshProUGUI mNameField;
    [SerializeField]
    private TextMeshProUGUI mTextField;
    [SerializeField]
    private Image mIconField;

    private bool mHidden;
    private CanvasGroup mCanvasGroup;

    private void Awake()
    {
        //Give the Game Manager a reference to the Dialogue Manager for easy access
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
