using UnityEngine;

public class ChangeDialogueScript : MonoBehaviour
{
    public DialoguePair[] mDialoguePairs;

    //When the object is destroyed the dialogues assigned to the given dialogue triggers are changed
    private void OnDisable()
    {
        foreach(DialoguePair pair in mDialoguePairs)
        {
            pair.mTrigger.SwapDialogue(pair.mConversation);
        }
    }
}
