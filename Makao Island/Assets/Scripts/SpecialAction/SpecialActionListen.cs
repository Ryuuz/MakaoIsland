public class SpecialActionListen : SpecialActionObject
{
    //The one responsible for giving the action
    private DialogueTrigger mOwner;

    public SpecialActionListen(DialogueTrigger owner)
    {
        mOwner = owner;
    }

    public override void UseSpecialAction(bool active)
    {
        if (active)
        {
            //Start playing the dialogue of the owner
            mOwner.PlayDialogue();
        }
    }
}
