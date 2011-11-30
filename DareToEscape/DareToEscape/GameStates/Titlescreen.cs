using BlackDragonEngine.GameStates;
using BlackDragonEngine.Providers;
using DareToEscape.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DareToEscape.GameStates
{
    internal class Titlescreen : IDrawableGameState, IUpdateableGameState
    {
        public static Texture2D TitleTexture { private get; set; }

        public bool UpdateCondition
        {
            get { return GameStateManager.State == States.Titlescreen; }
        }

        public bool DrawCondition
        {
            get { return UpdateCondition; }
        }

        public void Update()
        {
            if (InputProvider.KeyState.GetPressedKeys().Length > 0)
                GameStateManager.State = States.Menu;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                TitleTexture,
                Vector2.Zero,
                Color.White);
        }
    }
}