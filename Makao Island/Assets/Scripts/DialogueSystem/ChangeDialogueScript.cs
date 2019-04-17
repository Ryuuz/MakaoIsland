using UnityEngine;

public class ChangeDialogueScript : MonoBehaviour
{
    public DialoguePair[] mDialoguePairs;

    private void OnDisable()
    {
        foreach(DialoguePair pair in mDialoguePairs)
        {
            pair.mTrigger.SwapDialogue(pair.mConversation);
        }
    }
}
