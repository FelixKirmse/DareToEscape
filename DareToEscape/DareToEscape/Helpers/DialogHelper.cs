using BlackDragonEngine.Managers;
using DareToEscape.Providers;

namespace DareToEscape.Helpers
{
    internal static class DialogHelper
    {
        public static void PlayDialog(string dialogName)
        {
            switch (dialogName)
            {
                case "Tutorial1":
                    DialogManager.PlayDialog(DialogDictionaryProvider.TutorialDialog1(), "Tutorial1");
                    break;

                case "Tutorial2":
                    DialogManager.PlayDialog(DialogDictionaryProvider.TutorialDialog2(), "Tutorial2");
                    break;

                case "Tutorial3":
                    DialogManager.PlayDialog(DialogDictionaryProvider.TutorialDialog3(), "Tutorial3");
                    break;

                case "Tutorial4":
                    DialogManager.PlayDialog(DialogDictionaryProvider.TutorialDialog4(), "Tutorial4");
                    break;

                case "Tutorial5":
                    DialogManager.PlayDialog(DialogDictionaryProvider.TutorialDialog5(), "Tutorial5");
                    break;

                case "Tutorial6":
                    DialogManager.PlayDialog(DialogDictionaryProvider.TutorialDialog6(), "Tutorial6");
                    break;

                case "Tutorial7":
                    DialogManager.PlayDialog(DialogDictionaryProvider.TutorialDialog7(), "Tutorial7");
                    break;

                case "Tutorial8":
                    DialogManager.PlayDialog(DialogDictionaryProvider.TutorialDialog8(), "Tutorial8");
                    break;

                case "Tutorial9":
                    DialogManager.PlayDialog(DialogDictionaryProvider.TutorialDialog9(), "Tutorial9");
                    break;
            }
        }
    }
}