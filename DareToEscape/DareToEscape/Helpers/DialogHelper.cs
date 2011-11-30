using BlackDragonEngine.GameStates;
using DareToEscape.Providers;

namespace DareToEscape.Helpers
{
    internal static class DialogHelper
    {
        private static DialogManager _dialogManager;

        public static void Initialize(DialogManager dialogManager)
        {
            _dialogManager = dialogManager;
        }

        public static void PlayDialog(string dialogName)
        {
            switch (dialogName)
            {
                case "Tutorial1":
                    _dialogManager.PlayDialog(DialogDictionaryProvider.TutorialDialog1(), "Tutorial1");
                    break;

                case "Tutorial2":
                    _dialogManager.PlayDialog(DialogDictionaryProvider.TutorialDialog2(), "Tutorial2");
                    break;

                case "Tutorial3":
                    _dialogManager.PlayDialog(DialogDictionaryProvider.TutorialDialog3(), "Tutorial3");
                    break;

                case "Tutorial4":
                    _dialogManager.PlayDialog(DialogDictionaryProvider.TutorialDialog4(), "Tutorial4");
                    break;

                case "Tutorial5":
                    _dialogManager.PlayDialog(DialogDictionaryProvider.TutorialDialog5(), "Tutorial5");
                    break;

                case "Tutorial6":
                    _dialogManager.PlayDialog(DialogDictionaryProvider.TutorialDialog6(), "Tutorial6");
                    break;

                case "Tutorial7":
                    _dialogManager.PlayDialog(DialogDictionaryProvider.TutorialDialog7(), "Tutorial7");
                    break;

                case "Tutorial8":
                    _dialogManager.PlayDialog(DialogDictionaryProvider.TutorialDialog8(), "Tutorial8");
                    break;

                case "Tutorial9":
                    _dialogManager.PlayDialog(DialogDictionaryProvider.TutorialDialog9(), "Tutorial9");
                    break;
            }
        }
    }
}