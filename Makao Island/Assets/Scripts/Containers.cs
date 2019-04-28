using UnityEngine;
using UnityEngine.Events;

//The data that is stored when saving the game
[System.Serializable]
public class GameData
{
    public bool[] mSpiritAnimalsStatus = { false, false, false, false };
    public bool mMapStatus = true;
    public bool mSpiritGirlStatus = false;
    public float[] mPlayerPosition;
    public int mDayTime = 0;
    public float mCyclusTime = 0;
    public float[][] mAIPositions;
    public string mCheckPoint = "RespawnPoint";
    public string[] mDeletedObjects;
}

public struct Sentence
{
    public int speaker;
    public TalkAnimation gesture;
    public string text;

    public Sentence(int n, TalkAnimation g, string s)
    {
        speaker = n;
        gesture = g;
        text = s;
    }
}

public struct TalkingAIData
{
    public AITalking mTalkingScript;
    public VillagerAnimationScript mAnimationScript;
    public Transform mAITransform;
    public bool mAIPresent;
}

[System.Serializable]
public class DialoguePair
{
    public DialogueTrigger mTrigger;
    public int mConversation;
}

public class NextPoint : MonoBehaviour
{
    public GameObject mNext;
}

[System.Serializable]
public class AILeavingEvent : UnityEvent<GameObject>
{

}

[System.Serializable]
public class SpeedChangeEvent : UnityEvent<float>
{

}

[System.Serializable]
public class TimeChangeEvent : UnityEvent<DayCyclus>
{

}

[System.Serializable]
public class SpiritAnimalEvent : UnityEvent<int>
{

}