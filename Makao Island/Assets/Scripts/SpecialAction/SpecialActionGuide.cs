using UnityEngine;

public class SpecialActionGuide : SpecialActionObject
{
    //The NPC to be guided who is also responsible for giving the action
    private FollowGuideScript mOwner;
    private Transform mPlayer;

    public SpecialActionGuide(FollowGuideScript owner)
    {
        mOwner = owner;
        mPlayer = GameManager.ManagerInstance().mPlayer.transform;
    }

    //Have the owner go to the guide's location
    public override void UseSpecialAction(bool active)
    {
        if (active)
        {
            mOwner.FollowGuide(mPlayer.position);
        }
    }
}
