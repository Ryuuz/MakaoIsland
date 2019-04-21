public class SpecialActionSkip : SpecialActionObject
{
    private DialogueTrigger mOwner;

    public SpecialActionSkip(DialogueTrigger owner)
    {
        mOwner = owner;
    }

    public override void UseSpecialAction(bool active)
    {
        if(active)
        {
            mOwner.SkipDialogue();
        }
    }
}
