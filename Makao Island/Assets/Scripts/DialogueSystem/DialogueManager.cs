using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        if(GameManager.ManagerInstance().mDialogueManager == null)
        {
            GameManager.ManagerInstance().mDialogueManager = gameObject;
        }

        HideDialogueBox();
    }

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
        gameObject.SetActive(true);
        mHidden = false;
    }

    public void HideDialogueBox()
    {
        gameObject.SetActive(false);
        mHidden = true;
    }
}
