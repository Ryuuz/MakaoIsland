//All the dialogue in the game
public class DialogueLines
{
    private Sentence[][] mConversations =
    {
        //0
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.none, "I hear they still haven’t found the body of that poor girl."),
            new Sentence(2, TalkAnimation.none, "So you think she’s dead?"),
            new Sentence(1, TalkAnimation.nod, "She must be. Too much time has passed. Wasn’t she playing close to the waterfall? Her body must have been washed out to sea."),
            new Sentence(2, TalkAnimation.hands, "Shush! Don’t let her parents hear you talk like that. They’re still holding out hope of finding her alive."),
        },

        //1
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.cry, "Where could she be? It’s been so long… She should have found her way home by now."),
            new Sentence(2, TalkAnimation.hands, "Don’t give up. She’s a brave and smart little girl. I’m sure she’s fine. I’ll go looking for her again tomorrow. I promise I’ll find her."),
            new Sentence(1, TalkAnimation.cry, "I miss her so much. *Sobs*")
        },

        //2
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.none, "I dare you to go to the bottom of the waterfall tonight! Unless you’re a scaredy peakitten!"),
            new Sentence(2, TalkAnimation.none, "No way! Haven’t you heard those cries coming from there? What if there’s an evil ghost?"),
            new Sentence(1, TalkAnimation.none, "Hah! Ghosts aren’t real!"),
            new Sentence(2, TalkAnimation.none, "Then you go."),
            new Sentence(1, TalkAnimation.none, "Wha… F-forget it."),
            new Sentence(2, TalkAnimation.none, "Who’s the peakitten now?")
        },

        //3
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.none, "Grandpa… have you been setting out food for the animals again?"),
            new Sentence(2, TalkAnimation.none, "Silly boy. It’s an offering to the Great Lizard living in the caves below us."),
            new Sentence(1, TalkAnimation.none, "Grandpa… there’s no such thing as a great lizard spirit or hidden caves. Stop wasting our food."),
            new Sentence(2, TalkAnimation.none, "Why, when I was a young boy I fell into one of those caves. I was stuck there for days when a lizard showed up and led me to the exit."),
            new Sentence(2, TalkAnimation.nod, "No doubt a child of the Great Lizard. Back in my days, we actually worshipped the Great Animals. And they helped us in return."),
            new Sentence(1, TalkAnimation.none, "Yes, yes. You’ve told that story a million of times already."),
            new Sentence(2, TalkAnimation.hands, "Now you listen here boy! That Great Lizard is still down there. You had better respect it, or you’ll come to regret it."),
            new Sentence(1, TalkAnimation.none, "*Sigh*")
        },

        //4
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.none, "I fell… It’s dark. I can’t see. I want to go home, but I can’t find the way."),
            new Sentence(1, TalkAnimation.none, "Who are you? I can see your light. Will… Will you help me? Can you guide me out of here?")
        },

        //5
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.none, "*mumbles* Where is she? I haven’t seen her for days. Could she have been trapped in one of the fishing nets?")
        },

        //6
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.none, "They’ve been gathering up near those poles every morning lately. It’s impossible to retrieve the nets."),
            new Sentence(2, TalkAnimation.none, "Who has?"),
            new Sentence(1, TalkAnimation.hands, "The flacans!"),
            new Sentence(3, TalkAnimation.none, "Don’t they do that every year around this time? You shouldn’t have put your nets there."),
            new Sentence(1, TalkAnimation.none, "But it’s the best fishing spot."),
            new Sentence(2, TalkAnimation.none, "Not if you can’t get the fish, it isn’t.")
        },

        //7
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.none, "I heard you’ve been having a hard time again lately because of the flacans."),
            new Sentence(2, TalkAnimation.nod, "Aye, it’s some weird stuff going on there. They flock together every morning and eat all the fish, almost as if they’re celebrating."),
            new Sentence(1, TalkAnimation.none, "Maybe they’re having their own little festival to honor the Great Flacan."),
            new Sentence(2, TalkAnimation.none, "Haha! Now wouldn’t that be grand. I just wish they’d leave some more fish for us.")
        },

        //8
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.none, "They say that mermaids come to the surface at dusk to collect the reflection of the stars in the water as they appear."),
            new Sentence(2, TalkAnimation.hands, "Mermaids? Isn’t that just an old fisherman’s tale?"),
            new Sentence(1, TalkAnimation.none, "My father used to tell me that he caught a mermaid once when he was fishing late in the evening. She gave him a star in exchange for letting her go."),
            new Sentence(2, TalkAnimation.none, "A star? What did he do with it?"),
            new Sentence(1, TalkAnimation.none, "He put it under his pillow, but when he woke up, it was gone. He said that the dream he had that night led him to my mother."),
            new Sentence(2, TalkAnimation.none, "That is so sweet, but… I’m glad I didn’t have to go through all that to find you.")
        },

        //9
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.nod, "It’s going to be a good harvest this year, no doubt about that."),
            new Sentence(2, TalkAnimation.none, "We’ve been lucky with the weather. Not a single storm so far."),
            new Sentence(3, TalkAnimation.hands, "Don’t jinx it. My family is depending on this wheat to get us through to the next harvest.")
        },

        //10
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.none, "My grandparents used to tell me that there lived a Great Animal in this lake."),
            new Sentence(2, TalkAnimation.nod, "I’ve heard something similar. A giant turtle, I think?"),
            new Sentence(1, TalkAnimation.none, "I loved those stories. Whenever I had the chance, I would sneak out here in the hopes of seeing that turtle."),
            new Sentence(2, TalkAnimation.none, "*cuckles* So that’s what you were doing when I first met you here."),
            new Sentence(1, TalkAnimation.none, "It might only be a story, but that turtle brought us together."),
            new Sentence(2, TalkAnimation.nod, "You’re right. Maybe it’s more than just a legend after all.")
        },

        //11
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.none, "The pandamoose, the great God Animal, created the world in his image, but he soon grew lonely."),
            new Sentence(1, TalkAnimation.none, "The world was beautiful, but also lifeless…"),
            new Sentence(2, TalkAnimation.none, "So then he made humans and animals to fill it!"),
            new Sentence(1, TalkAnimation.hands, "That’s right. He molded the souls of every living creature and even now keeps the flow of life going."),
            new Sentence(3, TalkAnimation.none, "You tell us this story every year."),
            new Sentence(1, TalkAnimation.hands, "Then maybe you can tell us what happened next?"),
            new Sentence(3, TalkAnimation.none, "Uh…"),
            new Sentence(2, TalkAnimation.none, "He chose the Great animals!"),
            new Sentence(1, TalkAnimation.nod, "Exactly. He saw that the humans needed guidance. Someone to guard them. And so he shared some of his powers with three animals."),
            new Sentence(1, TalkAnimation.none, "It is in their honor that we hold this yearly festival.")
        },

        //12
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.none, "Why do we have this festival?"),
            new Sentence(2, TalkAnimation.none, "It’s in celebration of the Great Animals. To thank them for watching over us."),
            new Sentence(3, TalkAnimation.none, "At least that’s how it started. These days, it’s more about tradition and having a good time."),
            new Sentence(1, TalkAnimation.none, "Can I be a Great Animal?"),
            new Sentence(2, TalkAnimation.nod, "Of course you can! All hail the newest Great Animal!")
        },

        //13
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.hands, "What’s up with these old shrines? I nearly walked straight into one the other night."),
            new Sentence(2, TalkAnimation.none, "Don’t you know? It’s for the great animals."),
            new Sentence(1, TalkAnimation.nod, "Of course I know that, but why are they still there? Only those old fools still believe in that stuff."),
            new Sentence(2, TalkAnimation.hands, "What? You don’t believe in the shrines’ power to make time flow faster?"),
            new Sentence(1, TalkAnimation.none, "Stop joking already."),
            new Sentence(2, TalkAnimation.none, "*Laughs*")
        },

        //14
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.none, "Come and buy this year’s statuettes! There’s the great pandamoose, the creator of the world."),
            new Sentence(1, TalkAnimation.none, "And, of course, the chosen guardians: the lizard, the flacan, and the turtle. Don’t miss this great opportunity!")
        },

        //15
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.none, "Thank you.")
        }
    };

    //Retrieve the conversation numbered n
    public Sentence[] GetConversation(int index)
    {
        return mConversations[index];
    }
}
