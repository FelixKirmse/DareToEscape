using BlackDragonEngine.Dialogue;

namespace DareToEscape.Dialog.Tutorial
{
    internal sealed class TutorialDialog : DialogScript
    {
        public TutorialDialog()
        {
            SpeakerName = "AI-Voice";
            Text = "Welcome to Dare To Escape!\n"
                   + "This tutorial will guide you and\n"
                   + "Introduce you to the basic mechanics of the game\n"
                   + "Advance this box by pressing Enter or E";
            NextDialog = "Tutoriala";
        }
    }

    internal sealed class TutorialDialoga : DialogScript
    {
        public TutorialDialoga()
        {
            SpeakerName = "AI-Voice";
            Text = "To move your character left and right\n"
                   + "use A and D or the arrow keys, to jump press Space\n"
                   + "and to read the signs stay infront of them and\n"
                   + "press Enter or E";
            NextDialog = "STOPDIALOG";
        }
    }

    internal sealed class TutorialDialog1 : DialogScript
    {
        public TutorialDialog1()
        {
            SpeakerName = "AI-Voice";
            Text = "You jump higher the longer you press the jump button.\n"
                   + "You can also do a doublejump to reach greater heights.\n";
            NextDialog = "STOPDIALOG";
        }
    }

    internal sealed class TutorialDialog2 : DialogScript
    {
        public TutorialDialog2()
        {
            SpeakerName = "AI-Voice";
            Text = "To open locks you must first find the corresponding key.\n"
                   + "Once you find it you can simply walk into the locks and\n"
                   + "they dissappear";
            NextDialog = "STOPDIALOG";
        }
    }

    internal sealed class TutorialDialog3 : DialogScript
    {
        public TutorialDialog3()
        {
            SpeakerName = "AI-Voice";
            Text = "Careful! The orange fluid will kill you if you touch it!\n"
                   + "It is generally advised to avoid it.";
            NextDialog = "STOPDIALOG";
        }
    }

    internal sealed class TutorialDialog4 : DialogScript
    {
        public TutorialDialog4()
        {
            SpeakerName = "AI-Voice";
            Text = "These thin platforms are special: You can jump on them\n"
                   + "from below, but can't go down once you're ontop.\n"
                   + "This type of platform doesn't block line of sight.";
            NextDialog = "STOPDIALOG";
        }
    }

    internal sealed class TutorialDialog5 : DialogScript
    {
        public TutorialDialog5()
        {
            SpeakerName = "AI-Voice";
            Text = "These arrows work much like the platforms \nyou just encountered.\n"
                   + "You can only walk in the direction they point to once \nyou enter them.\n"
                   + "These also don't block LoS.";
            NextDialog = "STOPDIALOG";
        }
    }

    internal sealed class TutorialDialog6 : DialogScript
    {
        public TutorialDialog6()
        {
            SpeakerName = "AI-Voice";
            Text = "You just walked through a checkpoint. These enable you\n"
                   + "to continue at their location should you happen to die.\n";
            NextDialog = "Tutorial6a";
        }
    }

    internal sealed class TutorialDialog6a : DialogScript
    {
        public TutorialDialog6a()
        {
            SpeakerName = "AI-Voice";
            Text = "When you respawn all keys and locks will respawn, too\n"
                   + "But don't worry: you can still open the locks you \n"
                   + "got the key for";
            NextDialog = "STOPDIALOG";
        }
    }

    internal sealed class TutorialDialog7 : DialogScript
    {
        public TutorialDialog7()
        {
            SpeakerName = "AI-Voice";
            Text = "The blue thing in the next room is a small turret.\n"
                   + "They shoot 5 bullets at you with a 2 second pause after\n"
                   + "each wave of bullets. Try to get to the next checkpoint\n"
                   + "without dieing! Turrets shoot when you are in their LoS.";
            NextDialog = "STOPDIALOG";
        }
    }

    internal sealed class TutorialDialog8 : DialogScript
    {
        public TutorialDialog8()
        {
            SpeakerName = "AI-Voice";
            Text = "The turret in the next room is a medium turret.\n"
                   + "They shoot 10 bullets that chase you around.\n"
                   + "They are easy to avoid when there's only one,\n"
                   + "But dont underestimate them when in numbers!";
            NextDialog = "STOPDIALOG";
        }
    }

    internal sealed class TutorialDialog9 : DialogScript
    {
        public TutorialDialog9()
        {
            SpeakerName = "AI-Voice";
            Text = "The next room is a little harder then the previous.\n"
                   + "You'll face a medium turret and a boss turret.\n"
                   + "Boss turrets shoot walls of bullets everywhere and start\n"
                   + "shooting as soon as you enter the boss area\n";
            NextDialog = "Tutorial9a";
        }
    }

    internal sealed class TutorialDialog9a : DialogScript
    {
        public TutorialDialog9a()
        {
            SpeakerName = "AI-Voice";
            Text = "These are the only turrets you can and must disable.\n"
                   + "To do this you have to touch the orb inside the room.\n"
                   + "Not only will this deactivate the boss turret, but you\n"
                   + "also get a special key that allows you to open the locks";
            NextDialog = "Tutorial9b";
        }
    }

    internal sealed class TutorialDialog9b : DialogScript
    {
        public TutorialDialog9b()
        {
            SpeakerName = "AI-Voice";
            Text = "To complete the tutorial you have to defeat the boss and\n"
                   + "exit through the gate that is surrounded by boss-locks.\n"
                   + "Good Luck!";
            NextDialog = "STOPDIALOG";
        }
    }

    internal sealed class TutorialDialogFinish : DialogScript
    {
        public TutorialDialogFinish()
        {
            SpeakerName = "AI-Voice";
            Text = "Well done, you completed the tutorial!\n"
                   + "As you get to later levels the difficutly will rise.\n"
                   + "There will not only be more turrets, but the bullets\n"
                   + "they fire will also be faster";
            NextDialog = "TutorialFinisha";
        }
    }

    internal sealed class TutorialDialogFinisha : DialogScript
    {
        public TutorialDialogFinisha()
        {
            SpeakerName = "AI-Voice";
            Text = "Here's a tip for the rest of your escape:\n"
                   + "Be fast! It only gets harder if you just stand around!";
            NextDialog = "STOPDIALOG";
        }
    }

    internal sealed class Gratz : DialogScript
    {
        public Gratz()
        {
            SpeakerName = "AI-Voice";
            Text = "Gratz you won the game!\n"
                   + "Sadly didnt have time for more content\n"
                   + "or even a proper ending =(";
            NextDialog = "STOPDIALOG";
        }
    }
}