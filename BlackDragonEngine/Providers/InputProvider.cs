using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BlackDragonEngine.Providers
{
    public static class InputProvider
    {
        public static KeyboardState KeyState { get; private set; }
        public static MouseState MouseState { get; private set; }
        public static GamePadState PadState { get; private set; }

        public static KeyboardState LastKeyState { get; private set; }
        public static MouseState LastMouseState { get; private set; }
        public static GamePadState LastPadState { get; private set; }

        public static void Update()
        {
            LastKeyState = KeyState;
            LastMouseState = MouseState;
            LastPadState = PadState;

            KeyState = Keyboard.GetState();
            MouseState = Mouse.GetState();
            PadState = GamePad.GetState(PlayerIndex.One);
        }
    }
}
