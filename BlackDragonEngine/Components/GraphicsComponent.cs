using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Providers;

namespace BlackDragonEngine.Components
{
    public class GraphicsComponent : Component
    {
        protected Texture2D texture;
        protected float drawDepth = .82f;

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
        public override void Update(GameObject obj) { }
        public override void Receive<T>(string message, T obj)
        {
        }
    }
}
