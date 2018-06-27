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

        public static void Update(GameWindow window = null)
        {
            LastKeyState = KeyState;
            LastMouseState = MouseState;
            LastPadState = PadState;

            KeyState = Keyboard.GetState();
            MouseState = window == null ? Mouse.GetState() : Mouse.GetState(window);
            PadState = GamePad.GetState(PlayerIndex.One);
        }
    }
}