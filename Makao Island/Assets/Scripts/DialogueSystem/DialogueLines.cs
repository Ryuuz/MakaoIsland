
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

public class DialogueLines
{
    private Sentence[][] mConversations =
    {
        //0
        new Sentence[]
        {
            new Sentence(1, "Heya!"),
            new Sentence(2, "How's it hanging?"),
            new Sentence(1, "Pretty good. You?"),
            new Sentence(2, "Can't complain.")
        }
    };

    public Sentence[] GetConversation(int index)
    {
        return mConversations[index];
    }
}
