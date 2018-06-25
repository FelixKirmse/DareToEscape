using BlackDragonEngine.GameStates;
using BlackDragonEngine.Providers;
using DareToEscape.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DareToEscape.GameStates
{
    internal sealed class Titlescreen : IDrawableGameState, IUpdateableGameState
    {
        private readonly SpriteBatch _spriteBatch;

        public Titlescreen()
        {
            _spriteBatch = VariableProvider.SpriteBatch;
        }

        public static Texture2D TitleTexture { private get; set; }

        #region IDrawableGameState Members

        public bool DrawCondition
        {
            get { return UpdateCondition; }
        }

        public void Draw()
        {
            _spriteBatch.Draw(
                TitleTexture,
                Vector2.Zero,
                Color.White);
        }

        #endregion

        #region IUpdateableGameState Members

        public bool UpdateCondition
        {
            get { return GameStateManager.State == States.Titlescreen; }
        }

        public bool Update()
        {
            if (InputProvider.KeyState.IsKeyDown(Keys.Enter))
                GameStateManager.State = States.Menu;
            return false;
        }

        #endregion
    }
}