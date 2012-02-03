using BlackDragonEngine.Entities;
using BlackDragonEngine.Providers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlackDragonEngine.Components
{
    public class GraphicsComponent : IComponent
    {
        protected const float DrawDepth = .82f;
        protected SpriteBatch SpriteBatch;
        protected Texture2D Texture;

        public GraphicsComponent()
        {
            SpriteBatch = VariableProvider.SpriteBatch;
        }

        /// <summary>
        ///   Directly sets the texture, useful for Entity that only have a GraphicsComponent
        /// </summary>
        /// <param name = "texture"></param>
        public GraphicsComponent(Texture2D texture)
            : this()
        {
            Texture = texture;
        }

        #region IComponent Members

        public virtual void Update(GameObject obj)
        {
        }

        public virtual void Receive<T>(string message, T obj)
        {
        }

        #endregion

        public virtual void Draw(GameObject obj)
        {
            SpriteBatch.Draw(
                Texture,
                obj.ScreenPosition,
                null,
                Color.White,
                0f,
                Vector2.Zero,
                1f,
                SpriteEffects.None,
                DrawDepth);
        }
    }
}