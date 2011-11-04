using Microsoft.Xna.Framework.Graphics;

namespace BlackDragonEngine.Entities
{
    public interface IGameObject
    {
        void Update();
        void Draw(SpriteBatch spriteBatch);
    }
}