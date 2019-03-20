
[System.Serializable]
public class GameData
{
    public bool[] mSpiritAnimalsStatus = { false, false, false };
    public bool mMapStatus = false;
    public bool mSpiritGirlStatus = false;
    public float[] mPlayerPosition;
    public int mDayTime = 0;
    public float mCyclusTime = 0;
    public float[][] mAIPositions;
    public string mCheckPoint = "RespawnPoint";
}

public struct Sentence
{
    public int speaker;
    public string text;

    public Sentence(int n, string s)
    {
        speaker = n;
        text = s;
    }
}
