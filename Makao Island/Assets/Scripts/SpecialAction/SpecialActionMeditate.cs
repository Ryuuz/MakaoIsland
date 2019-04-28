public class SpecialActionMeditate : SpecialActionObject
{
    private ShrineScript mOwner;

    public SpecialActionMeditate(ShrineScript owner)
    {
        mOwner = owner;
    }

    //Set the game speed based on the parameter
    public override void UseSpecialAction(bool active)
    {
        if(active)
        {
            GameManager.ManagerInstance().SetGameSpeed(20f);
            mOwner.ShrineSound(true);
        }
        else
        {
            GameManager.ManagerInstance().SetGameSpeed(1f);
            mOwner.ShrineSound(false);
        }
    }
}
