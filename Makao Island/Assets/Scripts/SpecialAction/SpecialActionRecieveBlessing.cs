//Action for interacting with a spirit animal
public class SpecialActionRecieveBlessing : SpecialActionObject
{
    private SpiritAnimal mOwner;

    public SpecialActionRecieveBlessing(SpiritAnimal owner)
    {
        mOwner = owner;
    }

    public override void UseSpecialAction(bool active)
    {
        if(active)
        {
            mOwner.TriggerBlessing();
        }
    }
}
