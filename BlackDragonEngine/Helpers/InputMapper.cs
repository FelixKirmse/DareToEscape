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

        #region Normal Actionchecks
        public static bool Up 
        {
            get
            {
                return ShortcutProvider.AreAnyKeysDown(UpKeys) || ShortcutProvider.LeftStickUp();
            }
        }

        public static bool Down
        {
            get
            {
                return ShortcutProvider.AreAnyKeysDown(DownKeys) || ShortcutProvider.LeftStickDown();
            }
        }

        public static bool Left
        {
            get
            {
                return ShortcutProvider.AreAnyKeysDown(LeftKeys) || ShortcutProvider.LeftStickLeft();
            }
        }
        public static bool Right
        {
            get
            {
                return ShortcutProvider.AreAnyKeysDown(RightKeys) || ShortcutProvider.LeftStickRight();
            }
        }
        public static bool Jump
        {
            get
            {
                return ShortcutProvider.AreAnyKeysDown(JumpKeys);
            }
        }
        public static bool Action
        {
            get
            {
                return ShortcutProvider.AreAnyKeysDown(ActionKeys);
            }
        }
        public static bool Cancel
        {
            get 
            {
                return ShortcutProvider.AreAnyKeysDown(CancelKeys);
            }
        }
        #endregion

        #region Strict Actionchecks
        public static bool StrictUp
        {
            get
            {
                return ShortcutProvider.AreAnyKeysDown(UpKeys, true) || ShortcutProvider.LeftStickUp();
            }
        }

        public static bool StrictDown
        {
            get
            {
                return ShortcutProvider.AreAnyKeysDown(DownKeys, true) || ShortcutProvider.LeftStickDown();
            }
        }

        public static bool StrictLeft
        {
            get
            {
                return ShortcutProvider.AreAnyKeysDown(LeftKeys, true) || ShortcutProvider.LeftStickLeft();
            }
        }
        public static bool StrictRight
        {
            get
            {
                return ShortcutProvider.AreAnyKeysDown(RightKeys, true) || ShortcutProvider.LeftStickRight();
            }
        }
        public static bool StrictJump
        {
            get
            {
                return ShortcutProvider.AreAnyKeysDown(JumpKeys, true);
            }
        }
        public static bool StrictAction
        {
            get
            {
                return ShortcutProvider.AreAnyKeysDown(ActionKeys, true);
            }
        }
        public static bool StrictCancel
        {
            get
            {
                return ShortcutProvider.AreAnyKeysDown(CancelKeys, true);
            }
        }
        #endregion
    }    
}
