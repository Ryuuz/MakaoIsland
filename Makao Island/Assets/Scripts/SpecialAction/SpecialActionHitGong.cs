//Action for interacting with the gong asset
public class SpecialActionHitGong : SpecialActionObject
{
    private GongScript mOwner;

    public SpecialActionHitGong(GongScript owner)
    {
        mOwner = owner;
    }

    public override void UseSpecialAction(bool active)
    {
        if(active)
        {
            //Plays the gong's animation and sound
            mOwner.PlayGongAnimation();
        }
    }
}
