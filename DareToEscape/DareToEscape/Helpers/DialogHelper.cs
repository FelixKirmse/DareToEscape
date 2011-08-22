using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DareToEscape.Providers;
using BlackDragonEngine.Managers;
using BlackDragonEngine;
using BlackDragonEngine.Dialogue;
using DareToEscape.Dialog;

namespace DareToEscape.Helpers
{
    static class DialogHelper
    {
        public static void PlayDialog(string dialogName)
        {
            switch (dialogName)
            { 
                case "Tutorial1":
                    DialogManager.PlayDialog(DialogDictionaryProvider.TutorialDialog1(), "Start");
                    break;
            }
        }
    }
}
