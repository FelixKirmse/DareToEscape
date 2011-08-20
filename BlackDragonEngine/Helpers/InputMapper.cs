using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public static bool UP 
        {
            get
            {
                return ShortcutProvider.AreAnyKeysDown(UpKeys) || ShortcutProvider.LeftStickUp();
            }
        }

        public static bool DOWN
        {
            get
            {
                return ShortcutProvider.AreAnyKeysDown(DownKeys) || ShortcutProvider.LeftStickDown();
            }
        }

        public static bool LEFT
        {
            get
            {
                return ShortcutProvider.AreAnyKeysDown(LeftKeys) || ShortcutProvider.LeftStickLeft();
            }
        }
        public static bool RIGHT
        {
            get
            {
                return ShortcutProvider.AreAnyKeysDown(RightKeys) || ShortcutProvider.LeftStickRight();
            }
        }
        public static bool JUMP
        {
            get
            {
                return ShortcutProvider.AreAnyKeysDown(JumpKeys);
            }
        }
        public static bool ACTION
        {
            get
            {
                return ShortcutProvider.AreAnyKeysDown(ActionKeys);
            }
        }
        public static bool CANCEL
        {
            get 
            {
                return ShortcutProvider.AreAnyKeysDown(CancelKeys);
            }
        }
        #endregion

        #region Strict Actionchecks
        public static bool STRICTUP
        {
            get
            {
                return ShortcutProvider.AreAnyKeysDown(UpKeys, true) || ShortcutProvider.LeftStickUp();
            }
        }

        public static bool STRICTDOWN
        {
            get
            {
                return ShortcutProvider.AreAnyKeysDown(DownKeys, true) || ShortcutProvider.LeftStickDown();
            }
        }

        public static bool STRICTLEFT
        {
            get
            {
                return ShortcutProvider.AreAnyKeysDown(LeftKeys, true) || ShortcutProvider.LeftStickLeft();
            }
        }
        public static bool STRICTRIGHT
        {
            get
            {
                return ShortcutProvider.AreAnyKeysDown(RightKeys, true) || ShortcutProvider.LeftStickRight();
            }
        }
        public static bool STRICTJUMP
        {
            get
            {
                return ShortcutProvider.AreAnyKeysDown(JumpKeys, true);
            }
        }
        public static bool STRICTACTION
        {
            get
            {
                return ShortcutProvider.AreAnyKeysDown(ActionKeys, true);
            }
        }
        public static bool STRICTCANCEL
        {
            get
            {
                return ShortcutProvider.AreAnyKeysDown(CancelKeys, true);
            }
        }
        #endregion
    }    
}
