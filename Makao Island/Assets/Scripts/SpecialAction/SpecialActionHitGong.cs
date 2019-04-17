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
            mOwner.PlayGongAnimation();
        }
    }
}
