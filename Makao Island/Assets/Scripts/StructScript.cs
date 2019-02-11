
public struct GameProgressData
{
    public bool[] mSpiritAnimalsStatus;
    public bool mMapStatus;
}

public struct GameStatusData
{
    public DayCyclus mDayTime;
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
