//All the dialogue in the game
public class DialogueLines
{
    private Sentence[][] mConversations =
    {
        //0
        new Sentence[]
        {
                    //Speaker //Animation       //Dialogue
            new Sentence(1, TalkAnimation.none, "I hear they still haven’t found the body of that girl."),
            new Sentence(2, TalkAnimation.none, "So you think she’s dead?"),
            new Sentence(1, TalkAnimation.nod, "She must be. Wasn’t she playing close to the waterfall? I bet her body washed out to sea."),
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
            new Sentence(1, TalkAnimation.none, "I dare you to go to the bottom of the waterfall tonight! Unless you’re a scaredy huffalo!"),
            new Sentence(2, TalkAnimation.none, "No way! Haven’t you heard those cries coming from there? What if there’s an evil ghost?"),
            new Sentence(1, TalkAnimation.none, "Hah! Ghosts aren’t real!"),
            new Sentence(2, TalkAnimation.none, "Then you go."),
            new Sentence(1, TalkAnimation.none, "Wha… F-forget it."),
            new Sentence(2, TalkAnimation.none, "Who’s the huffalo now?")
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
            new Sentence(1, TalkAnimation.none, "*Mumbles* There she is. She’s beautiful today as well."),
            new Sentence(1, TalkAnimation.none, "Should I call out to her? No… I wouldn’t want to scare her away.")
        },

        //6
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.none, "They’ve been gathering up near the peak every morning lately. It’s impossible to retrieve the nets."),
            new Sentence(2, TalkAnimation.none, "Who has?"),
            new Sentence(1, TalkAnimation.hands, "The flacans!"),
            new Sentence(3, TalkAnimation.none, "Don’t they do that every year around this time? You shouldn’t have put your nets there."),
            new Sentence(1, TalkAnimation.none, "But it’s the best fishing spot."),
            new Sentence(2, TalkAnimation.none, "Not if you can’t get the fish.")
        },

        //7
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.none, "I heard the flacans have been acting up again recently."),
            new Sentence(2, TalkAnimation.nod, "Aye, it’s some weird stuff going on there. They flock together every morning and eat all the fish, almost as if they’re celebrating."),
            new Sentence(1, TalkAnimation.none, "Maybe they’re having their own little festival to honor the Great Flacan."),
            new Sentence(2, TalkAnimation.none, "Haha! Now wouldn’t that be grand. I just wish they’d leave some more fish for us.")
        },

        //8
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.none, "They say that mermaids come to the surface at dusk to collect the reflections of the stars in the water."),
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
            new Sentence(3, TalkAnimation.hands, "Don’t jinx it. My family is depending on these crops to get us through to the next harvest.")
        },

        //10
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.none, "My grandparents used to tell me that there lived a Great Animal in this lake."),
            new Sentence(2, TalkAnimation.nod, "I’ve heard something similar. A giant turtle, I think?"),
            new Sentence(1, TalkAnimation.none, "I loved those stories. Whenever I had the chance, I would sneak out here in the hopes of seeing that turtle."),
            new Sentence(2, TalkAnimation.none, "*cuckles* So that’s what you were doing when I first met you here."),
            new Sentence(1, TalkAnimation.none, "In some ways, that turtle brought us together."),
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
            new Sentence(1, TalkAnimation.none, "They became known as the Great Animals.")
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
            new Sentence(2, TalkAnimation.hands, "What? You don’t believe in the shrines’ power?"),
            new Sentence(1, TalkAnimation.none, "Stop joking already.")
        },

        //14
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.none, "Come and buy this year’s statuettes! There’s the great pandamoose, creator of the world."),
            new Sentence(1, TalkAnimation.none, "And, of course, the chosen guardians: the lizard, the flacan, and the turtle. Don’t miss this great opportunity!")
        },

        //15
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.none, "Thank you.")
        },

        //16
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.cry, "She’s gone. She’s really gone. *Sobs*"),
            new Sentence(2, TalkAnimation.hands, "Don’t give up hope. I’ll find her, so just wait a little longer."),
            new Sentence(1, TalkAnimation.cry, "It’s over. She came to me in a dream. She told us to move on. That she… that she loves us."),
            new Sentence(2, TalkAnimation.none, "No, I refuse to accept it. I’m going to keep looking for as long as it takes.")
        },

        //17
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.none, "Grandpa… have you been setting out food for the animals again?"),
            new Sentence(2, TalkAnimation.hands, "I’ve told you before, haven’t I? It’s an offering to the Great Lizard. But… today is the last day."),
            new Sentence(1, TalkAnimation.none, "Last day? What are you talking about? Did you finally realise it’s just a made-up tale?"),
            new Sentence(2, TalkAnimation.none, "Silly boy. Can’t you feel it? The Great Lizard is no more. We are no longer worthy.")
        },

        //18
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.none, "They’re finally gone, the annoying little things."),
            new Sentence(2, TalkAnimation.none, "Who?"),
            new Sentence(1, TalkAnimation.hands, "The flacans!"),
            new Sentence(3, TalkAnimation.nod, "Ah, you're right."),
            new Sentence(2, TalkAnimation.none, "That’s unusual. They’ve always stayed for several more days in previous years. I wonder if something changed.")
        },

        //19
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.none, "I heard the flacans finally calmed down."),
            new Sentence(2, TalkAnimation.nod, "Right you are. I guess they had eaten their fill of fish."),
            new Sentence(1, TalkAnimation.none, "I can’t put my finger on it, but something feels different."),
        },

        //20
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.none, "It’s gone. I knew it was weak, but I didn’t think it would happen this soon."),
            new Sentence(2, TalkAnimation.hands, "What do you mean? Who are you talking about?"),
            new Sentence(1, TalkAnimation.cry, "The Great Turtle. It has left."),
            new Sentence(2, TalkAnimation.none, "I… I’m so sorry. I knew it meant a lot to you."),
            new Sentence(2, TalkAnimation.none, "Let us make sure its memory lives on by continuing to pass down the story. Just like your grandparents did."),
            new Sentence(1, TalkAnimation.nod, "You’re right. Thank you.")
        },

        //21
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.none, "I wonder what’s on the other side of the ocean."),
            new Sentence(2, TalkAnimation.none, "A book I read told of a strange man who washed up on the shores here, so there must be other islands."),
            new Sentence(1, TalkAnimation.none, "My mom says there’s nothing to… um… to prove that story is true."),
            new Sentence(3, TalkAnimation.none, "The adults are just scared. One day, I’ll travel across the ocean myself."),
            new Sentence(2, TalkAnimation.none, "An expedition? Count me in!"),
            new Sentence(1, TalkAnimation.none, "I think I’d rather stay here. On dry land.")
        },

        //22
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.none, "Ugh, I hate this stupid mask!"),
            new Sentence(2, TalkAnimation.none, "Don’t take it off! The evil spirits will eat you if they see your face."),
            new Sentence(1, TalkAnimation.none, "You sound like my grandpa. There is no such thing as evil spirits."),
            new Sentence(2, TalkAnimation.none, "But what if you’re wrong? Do you really want to risk it?"),
            new Sentence(2, TalkAnimation.none, "Besides, it’s only for as long as the festival lasts."),
            new Sentence(1, TalkAnimation.none, "Fine! I won’t take it off. But only because you’ll keep nagging me about it otherwise.")
        },

        //23
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.none, "Tell me the story about the lizard again!"),
            new Sentence(2, TalkAnimation.nod, "All right, all right. Settle down."),
            new Sentence(2, TalkAnimation.none, "Let’s see… Every afternoon and evening, the little lizard would climb to the top of the cave so it could be as close to the surface as possible."),
            new Sentence(2, TalkAnimation.none, "It would sit there and listen to the sounds of the humans going about their daily life."),
            new Sentence(2, TalkAnimation.none, "The little lizard wished it could join them, but its skin and eyes weren’t made for being in the sunlight so it couldn't leave the cave."),
            new Sentence(2, TalkAnimation.none, "One day, when the Pandamoose was visiting all the animals, it saw the lizard and took pity on it."),
            new Sentence(2, TalkAnimation.none, "With the blessing of the Pandamoose, the lizard became one of the Great Animals and was able to venture outside the cave."),
            new Sentence(1, TalkAnimation.none, "But why would a lizard want to be among the humans? I mean… it’s a lizard."),
            new Sentence(2, TalkAnimation.none, "Hmm, I wonder.")
        },

        //24
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.none, "I found this old scroll the other day. It contains stories of the Great Turtle."),
            new Sentence(2, TalkAnimation.none, "Woah, that’s amazing! I’ve never seen anything like it."),
            new Sentence(1, TalkAnimation.none, "It says the Turtle was the first Great Animal chosen by the Pandamoose. It would appear every night and morning to herald the coming day."),
            new Sentence(2, TalkAnimation.nod, "Fascinating. I knew the Great Turtle was connected to dawn, but to think that it symbolised the start of a new day."),
            new Sentence(1, TalkAnimation.none, "I wonder if the other animals had a similar role. Too many of these stories have been lost to time.")
        },

        //25
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.none, "I know you’re growing weak, but please don’t give up on us just yet. We still need your guidance and—"),
            new Sentence(1, TalkAnimation.hands, "Who’s there? I can… sense you. Mei? Is it you?")
        },

        //26
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.none, "Thank you for always coming up here. These old bones aren’t what they used to be, I’m afraid."),
            new Sentence(2, TalkAnimation.none, "It’s no problem at all. I’m just glad my herbal tea can be of help to you."),
            new Sentence(1, TalkAnimation.nod, "And it is a great help indeed. We are lucky to have you and your herbs.")
        },

        //27
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.none, "I can’t believe they want me to work the field. Aren’t I doing enough by making sure we have fresh fish every day?"),
            new Sentence(2, TalkAnimation.none, "It’s always been like this, hasn’t it? Everyone takes turns working the field, and everyone shares the harvest."),
            new Sentence(1, TalkAnimation.none, "You aren’t wrong, but I’m getting too old for all this. I’ll work myself to death at this rate."),
            new Sentence(2, TalkAnimation.none, "I’m sure the other villagers will understand if you talk to them about it."),
            new Sentence(1, TalkAnimation.none, "And look like some lazy old fool? No thank you!")
        },

        //28
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.none, "These huffalos really are impressive animals."),
            new Sentence(2, TalkAnimation.none, "I’ve watched over this flock since they were calves. They’ve done some good work in the fields over the years.")
        },

        //29
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.none, "What are you doing?"),
            new Sentence(2, TalkAnimation.none, "I’m trying to speed up time. Be quiet so I can concentrate."),
            new Sentence(1, TalkAnimation.none, "...I don’t think it’s working.")
        },

        //30
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.none, "I can’t believe they’re still teaching children about those silly animals."),
            new Sentence(2, TalkAnimation.none, "Uh-huh."),
            new Sentence(1, TalkAnimation.hands, "Hey, are you listening to me?"),
            new Sentence(2, TalkAnimation.none, "Uh-huh."),
            new Sentence(1, TalkAnimation.none, "Hmph.")
        },

        //31
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.none, "So beautiful. I wish I could become a firefly. I’d be able to dance all night surrounded by friends.")
        },

        //32
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.none, "Mei! Where are you?"),
            new Sentence(1, TalkAnimation.none, "Can you hear me?"),
            new Sentence(1, TalkAnimation.none, "Meeeeeeeei!")
        },

        //33
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.hands, "Please say you’ve heard something about my daughter."),
            new Sentence(2, TalkAnimation.none, "I really am sorry, but I’ve yet to hear any new information. I’ll keep asking my customers, though."),
            new Sentence(1, TalkAnimation.nod, "Thank you, I really appreciate it.")
        },

        //34
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.none, "Is this today’s catch?"),
            new Sentence(2, TalkAnimation.none, "Not good enough for you?"),
            new Sentence(1, TalkAnimation.hands, "Oh no, it looks wonderful. I’ll have three.")
        },

        //35
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.none, "How are you holding up? I can’t imagine how it must be to go through all this."),
            new Sentence(2, TalkAnimation.cry, "It’s… I don’t know what to do. *Sobs*"),
            new Sentence(1, TalkAnimation.none, "Keep taking one day at a time. We all miss her, but you have to stay strong. If you ever need to talk, I’m here for you."),
            new Sentence(2, TalkAnimation.cry, "I’ll try, but it’s just so hard.")
        },

        //36
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.hands, "Father, do you know where my son has disappeared to this time?"),
            new Sentence(2, TalkAnimation.none, "Oh, I wouldn’t worry about that boy. He’s capable of taking care of himself."),
            new Sentence(1, TalkAnimation.none, "He’s avoiding me, isn’t he? Ever since the accident…"),
            new Sentence(2, TalkAnimation.none, "Capable, but still young. You need to give him time.")
        },

        //37
        new Sentence[]
        {
            new Sentence(1, TalkAnimation.none, "Stupid Mei. It’s no fun when you’re not here."),
            new Sentence(2, TalkAnimation.none, "Why did you have to go and disappear without me?")
        }
    };

    //Retrieve the conversation at 'index'
    public Sentence[] GetConversation(int index)
    {
        return mConversations[index];
    }
}
