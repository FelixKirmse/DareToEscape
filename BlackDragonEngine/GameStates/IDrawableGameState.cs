using Microsoft.Xna.Framework.Graphics;

namespace BlackDragonEngine.GameStates
{
    public interface IDrawableGameState
    {
        bool DrawCondition { get; }
        void Draw(SpriteBatch spriteBatch);
    }
}