using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackDragonEngine.Dialogue;
using DareToEscape.Dialog.Tutorial;

namespace DareToEscape.Providers
{
    static class DialogDictionaryProvider
    {
        public static Dictionary<string, DialogScript> TutorialDialog()
        {
            Dictionary<string, DialogScript> dialog = new Dictionary<string, DialogScript>();
            dialog.Add("Tutorial", new TutorialDialog());
            dialog.Add("Tutoriala", new TutorialDialoga());
            return dialog;
        }

        public static Dictionary<string, DialogScript> TutorialDialog1()
        {
            Dictionary<string, DialogScript> dialog = new Dictionary<string, DialogScript>();
            dialog.Add("Tutorial1", new TutorialDialog1());            
            return dialog;
        }

        public static Dictionary<string, DialogScript> TutorialDialog2()
        {
            Dictionary<string, DialogScript> dialog = new Dictionary<string, DialogScript>();
            dialog.Add("Tutorial2", new TutorialDialog2());
            return dialog;
        }

        public static Dictionary<string, DialogScript> TutorialDialog3()
        {
            Dictionary<string, DialogScript> dialog = new Dictionary<string, DialogScript>();
            dialog.Add("Tutorial3", new TutorialDialog3());
            return dialog;
        }

        public static Dictionary<string, DialogScript> TutorialDialog4()
        {
            Dictionary<string, DialogScript> dialog = new Dictionary<string, DialogScript>();
            dialog.Add("Tutorial4", new TutorialDialog4());
            return dialog;
        }

        public static Dictionary<string, DialogScript> TutorialDialog5()
        {
            Dictionary<string, DialogScript> dialog = new Dictionary<string, DialogScript>();
            dialog.Add("Tutorial5", new TutorialDialog5());
            return dialog;
        }

        public static Dictionary<string, DialogScript> TutorialDialog6()
        {
            Dictionary<string, DialogScript> dialog = new Dictionary<string, DialogScript>();
            dialog.Add("Tutorial6", new TutorialDialog6());
            dialog.Add("Tutorial6a", new TutorialDialog6a());
            return dialog;
        }

        public static Dictionary<string, DialogScript> TutorialDialog7()
        {
            Dictionary<string, DialogScript> dialog = new Dictionary<string, DialogScript>();
            dialog.Add("Tutorial7", new TutorialDialog7());
            return dialog;
        }

        public static Dictionary<string, DialogScript> TutorialDialog8()
        {
            Dictionary<string, DialogScript> dialog = new Dictionary<string, DialogScript>();
            dialog.Add("Tutorial8", new TutorialDialog8());
            return dialog;
        }

        public static Dictionary<string, DialogScript> TutorialDialog9()
        {
            Dictionary<string, DialogScript> dialog = new Dictionary<string, DialogScript>();
            dialog.Add("Tutorial9", new TutorialDialog9());
            dialog.Add("Tutorial9a", new TutorialDialog9a());
            dialog.Add("Tutorial9b", new TutorialDialog9b());
            return dialog;
        }

        public static Dictionary<string, DialogScript> TutorialDialogFinish()
        {
            Dictionary<string, DialogScript> dialog = new Dictionary<string, DialogScript>();
            dialog.Add("TutorialFinish", new TutorialDialogFinish());
            dialog.Add("TutorialFinisha", new TutorialDialogFinisha());
            return dialog;
        }

        public static Dictionary<string, DialogScript> Gratz()
        {
            Dictionary<string, DialogScript> dialog = new Dictionary<string, DialogScript>();
            dialog.Add("Gratz", new Gratz());
            return dialog;
        }
    }
}
