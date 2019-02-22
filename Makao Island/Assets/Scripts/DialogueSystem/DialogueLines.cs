
public class DialogueLines
{
    private Sentence[][] mConversations =
    {
        //0
        new Sentence[]
        {
            new Sentence(1, "The flacans have been acting up lately. I wonder if something happened."),
            new Sentence(2, "It's probably nothin'. They're just foolin' around and bein' flacans is all."),
            new Sentence(1, "If you say so."),
            new Sentence(1, "But...they usually don't gather on that plateau."),
            new Sentence(2, "Which one now?"),
            new Sentence(1, "The one to the east. The southernmost of them."),
            new Sentence(2, "Hmm, that is unusual. Probably nothin' either way, I reckon.")
        },

        //1
        new Sentence[]
        {
            new Sentence(1, "Please lead me to the bridge."),
        },

        //2
        new Sentence[]
        {
            new Sentence(1, "Thank you."),
        }
    };

    //Retrieve the conversation numbered n
    public Sentence[] GetConversation(int index)
    {
        return mConversations[index];
    }
}
