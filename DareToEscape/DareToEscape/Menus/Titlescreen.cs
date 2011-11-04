using BlackDragonEngine.Providers;
using DareToEscape.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DareToEscape.Menus
{
    internal static class Titlescreen
    {
        public static Texture2D TitleTexture { get; set; }

        public static void Update()
        {
            if (InputProvider.KeyState.GetPressedKeys().Length > 0)
                StateManager.GameState = GameStates.Menu;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                TitleTexture,
                Vector2.Zero,
                Color.White);
        }
    }
}