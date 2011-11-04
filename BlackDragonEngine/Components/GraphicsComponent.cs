using BlackDragonEngine.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlackDragonEngine.Components
{
    public class GraphicsComponent : Component
    {
        protected float drawDepth = .82f;
        protected Texture2D texture;

        public GraphicsComponent()
        {
        }

        /// <summary>
        /// Directly sets the texture, useful for Entity that only have a GraphicsComponent
        /// </summary>
        /// <param name="texture"></param>
        public GraphicsComponent(Texture2D texture)
        {
            this.texture = texture;
        }

        public virtual void Draw(GameObject obj, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture,
                obj.ScreenPosition,
                null,
                Color.White,
                0f,
                Vector2.Zero,
                1f,
                SpriteEffects.None,
                drawDepth);
        }

        public override void Update(GameObject obj)
        {
        }

        public override void Receive<T>(string message, T obj)
        {
        }
    }
}