
public class SpecialActionMeditate : SpecialActionObject
{
    //Set the game speed based on the parameter
    public override void UseSpecialAction(bool active)
    {
        if(active)
        {
            GameManager.ManagerInstance().SetGameSpeed(4f);
        }
        else
        {
            GameManager.ManagerInstance().SetGameSpeed(1f);
        }
    }
}
