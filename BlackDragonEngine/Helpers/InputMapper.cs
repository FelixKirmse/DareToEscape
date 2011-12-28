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

        public static bool Up
        {
            get { return ShortCuts.AreAnyKeysDown(UpKeys) || ShortCuts.LeftStickUp(); }
        }

        public static bool Down
        {
            get { return ShortCuts.AreAnyKeysDown(DownKeys) || ShortCuts.LeftStickDown(); }
        }

        public static bool Left
        {
            get { return ShortCuts.AreAnyKeysDown(LeftKeys) || ShortCuts.LeftStickLeft(); }
        }

        public static bool Right
        {
            get { return ShortCuts.AreAnyKeysDown(RightKeys) || ShortCuts.LeftStickRight(); }
        }

        public static bool Jump
        {
            get { return ShortCuts.AreAnyKeysDown(JumpKeys); }
        }

        public static bool Action
        {
            get { return ShortCuts.AreAnyKeysDown(ActionKeys); }
        }

        public static bool Cancel
        {
            get { return ShortCuts.AreAnyKeysDown(CancelKeys); }
        }

        public static bool LeftClick
        {
            get { return ShortCuts.LeftMouseClicked(); }
        }

        public static bool RightClick
        {
            get { return ShortCuts.RightMouseClicked(); }
        }

        #endregion

        #region Strict Actionchecks

        public static bool StrictUp
        {
            get { return ShortCuts.AreAnyKeysDown(UpKeys, true) || ShortCuts.LeftStickUp(); }
        }

        public static bool StrictDown
        {
            get { return ShortCuts.AreAnyKeysDown(DownKeys, true) || ShortCuts.LeftStickDown(); }
        }

        public static bool StrictLeft
        {
            get { return ShortCuts.AreAnyKeysDown(LeftKeys, true) || ShortCuts.LeftStickLeft(); }
        }

        public static bool StrictRight
        {
            get { return ShortCuts.AreAnyKeysDown(RightKeys, true) || ShortCuts.LeftStickRight(); }
        }

        public static bool StrictJump
        {
            get { return ShortCuts.AreAnyKeysDown(JumpKeys, true); }
        }

        public static bool StrictAction
        {
            get { return ShortCuts.AreAnyKeysDown(ActionKeys, true); }
        }

        public static bool StrictCancel
        {
            get { return ShortCuts.AreAnyKeysDown(CancelKeys, true); }
        }

        public static bool StrictLeftClick
        {
            get { return ShortCuts.LeftButtonClickedNowButNotLastFrame(); }
        }

        public static bool StrictRightClick
        {
            get { return ShortCuts.RightButtonClickedButNotLastFrame(); }
        }

        #endregion

        #region CustomActionHandling

        public static void AddNewAction(string name, List<Keys> keys)
        {
            CustomActions.Add(name, keys);
        }

        public static bool TriggeredAction(string name)
        {
            Keys[] keys = CustomActions[name].ToArray();
            return ShortCuts.AreAnyKeysDown(keys);
        }

        public static bool TriggeredStrictAction(string name)
        {
            Keys[] keys = CustomActions[name].ToArray();
            return ShortCuts.AreAnyKeysDown(keys, true);
        }

        #endregion
    }
}