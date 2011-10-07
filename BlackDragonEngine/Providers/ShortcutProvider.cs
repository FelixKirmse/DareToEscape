using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Management;

namespace BlackDragonEngine.Providers
{
    public static class ShortcutProvider
    {
        public static int GameWindowWidth 
        {
            get { return VariableProvider.Game.Window.ClientBounds.Width; }
        }

        public static int GameWindowHeight
        {
            get { return VariableProvider.Game.Window.ClientBounds.Height; }
        }

        public static Vector2 ScreenCenter
        {
            get { return new Vector2(GameWindowWidth / 2, GameWindowHeight / 2); }
        }

        public static Vector2 GetFontCenter(string fontName, string String)
        {
            return new Vector2(
                FontProvider.GetFont(fontName).MeasureString(String).X / 2,
                FontProvider.GetFont(fontName).MeasureString(String).Y / 2);
        }

        public static bool IsKeyDown(Keys key) {
            return InputProvider.KeyState.IsKeyDown(key);
        }

        public static bool KeyPressedNowButNotLastFrame(Keys key)
        {
            return (IsKeyDown(key) && InputProvider.LastKeyState.IsKeyUp(key));
        }

        public static Rectangle GetFontRectangle(Vector2 position, string fontName, string String)
        {
            return new Rectangle(
                (int)position.X,
                (int)position.Y,
                (int)FontProvider.GetFont(fontName).MeasureString(String).X,
                (int)FontProvider.GetFont(fontName).MeasureString(String).Y);
        }

        public static bool MouseIntersectsRectangle(Rectangle rectangle)
        {
            return rectangle.Intersects(new Rectangle(
                InputProvider.MouseState.X,
                InputProvider.MouseState.Y,
                1,
                1));
        }

        public static bool LeftButtonClicked()
        {
            return InputProvider.MouseState.LeftButton == ButtonState.Pressed;
        }

        public static bool LeftButtonClickedNowButNotLastFrame() {
            return (LeftButtonClicked() && (InputProvider.LastMouseState.LeftButton == ButtonState.Released));
        }

        public static bool RightButtonClicked()
        {
            return InputProvider.MouseState.RightButton == ButtonState.Pressed;
        }

        public static bool RightButtonClickedButNotLastFrame() {
            return (RightButtonClicked() && (InputProvider.LastMouseState.RightButton == ButtonState.Released));
        }

        public static bool AreAnyKeysDown(Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (IsKeyDown(key))
                    return true;
            }
            return false;
        }

        public static bool AreAnyKeysDown(Keys[] keys, bool strict)
        {
            if (!strict)
                return AreAnyKeysDown(keys);

            foreach (Keys key in keys)
            {
                if (KeyPressedNowButNotLastFrame(key))
                    return true;
            }
            return false;
        }

        public static bool LeftStickUp()
        {
            return InputProvider.PadState.ThumbSticks.Left.Y > 0.3f;
        }

        public static bool LeftStickDown()
        {
            return InputProvider.PadState.ThumbSticks.Left.Y < -0.3f;
        }

        public static bool LeftStickLeft()
        {
            return InputProvider.PadState.ThumbSticks.Left.X < -0.3f;
        }

        public static bool LeftStickRight()
        {
            return InputProvider.PadState.ThumbSticks.Left.X > 0.3f;
        }

        public static float ElapsedMilliseconds
        {
            get { return (float)VariableProvider.GameTime.ElapsedGameTime.TotalMilliseconds; }
        }

        public static float ElapsedSeconds
        {
            get { return (float)VariableProvider.GameTime.ElapsedGameTime.TotalSeconds; }
        }

        public static Vector2 Vector2Point(Vector2 vector)
        {
            return new Vector2((int)vector.X, (int)vector.Y);
        }

        public static Point GetMaximumScreenSizePrimary()
        {            
            var scope = new ManagementScope();
            var q = new ObjectQuery("SELECT * FROM CIM_VideoControllerResolution");

            var searcher = new ManagementObjectSearcher(scope, q);
            
            var results = searcher.Get();
            UInt32 maxHResolution = 0;
            UInt32 maxVResolution = 0;

            foreach (var item in results)
            {
                if ((UInt32)item["HorizontalResolution"] > maxHResolution)
                    maxHResolution = (UInt32)item["HorizontalResolution"];

                if ((UInt32)item["VerticalResolution"] > maxVResolution)
                    maxVResolution = (UInt32)item["VerticalResolution"];
            }                
            
            return new Point((int)maxHResolution, (int)maxVResolution);
        }

    }
}
