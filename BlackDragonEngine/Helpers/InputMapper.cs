using System.Collections.Generic;
using BlackDragonEngine.Providers;
using Microsoft.Xna.Framework.Input;

namespace BlackDragonEngine.Helpers
{
    public static class InputMapper
    {
        public static Keys[] UpKeys = new Keys[2];
        public static Keys[] DownKeys = new Keys[2];
        public static Keys[] LeftKeys = new Keys[2];
        public static Keys[] RightKeys = new Keys[2];
        public static Keys[] JumpKeys = new Keys[2];
        public static Keys[] ActionKeys = new Keys[2];
        public static Keys[] CancelKeys = new Keys[2];

        private static readonly Dictionary<string, List<Keys>> CustomActions = new Dictionary<string, List<Keys>>();

        #region Normal Actionchecks

        public static bool Up => ShortCuts.AreAnyKeysDown(UpKeys) || ShortCuts.LeftStickUp();

        public static bool Down => ShortCuts.AreAnyKeysDown(DownKeys) || ShortCuts.LeftStickDown();

        public static bool Left => ShortCuts.AreAnyKeysDown(LeftKeys) || ShortCuts.LeftStickLeft();

        public static bool Right => ShortCuts.AreAnyKeysDown(RightKeys) || ShortCuts.LeftStickRight();

        public static bool Jump => ShortCuts.AreAnyKeysDown(JumpKeys);

        public static bool Action => ShortCuts.AreAnyKeysDown(ActionKeys);

        public static bool Cancel => ShortCuts.AreAnyKeysDown(CancelKeys);

        public static bool LeftClick => ShortCuts.LeftMouseClicked();

        public static bool RightClick => ShortCuts.RightMouseClicked();

        #endregion

        #region Strict Actionchecks

        public static bool StrictUp => ShortCuts.AreAnyKeysDown(UpKeys, true) || ShortCuts.LeftStickUp();

        public static bool StrictDown => ShortCuts.AreAnyKeysDown(DownKeys, true) || ShortCuts.LeftStickDown();

        public static bool StrictLeft => ShortCuts.AreAnyKeysDown(LeftKeys, true) || ShortCuts.LeftStickLeft();

        public static bool StrictRight => ShortCuts.AreAnyKeysDown(RightKeys, true) || ShortCuts.LeftStickRight();

        public static bool StrictJump => ShortCuts.AreAnyKeysDown(JumpKeys, true);

        public static bool StrictAction => ShortCuts.AreAnyKeysDown(ActionKeys, true);

        public static bool StrictCancel => ShortCuts.AreAnyKeysDown(CancelKeys, true);

        public static bool StrictLeftClick => ShortCuts.LeftButtonClickedNowButNotLastFrame();

        public static bool StrictRightClick => ShortCuts.RightButtonClickedButNotLastFrame();

        #endregion

        #region CustomActionHandling

        public static void AddNewAction(string name, List<Keys> keys)
        {
            CustomActions.Add(name, keys);
        }

        public static bool TriggeredAction(string name)
        {
            var keys = CustomActions[name].ToArray();
            return ShortCuts.AreAnyKeysDown(keys);
        }

        public static bool TriggeredStrictAction(string name)
        {
            var keys = CustomActions[name].ToArray();
            return ShortCuts.AreAnyKeysDown(keys, true);
        }

        #endregion
    }
}