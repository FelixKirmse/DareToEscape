using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Components;
namespace DareToEscape.Components.Entities
{
    class BulletGraphicsComponent : GraphicsComponent
    {
        private Color drawColor;

        public BulletGraphicsComponent() 
            : base()
        {
            drawColor = Color.White;
        }

        public BulletGraphicsComponent(Texture2D texture)
            : base(texture)
        {
            drawColor = Color.White;
        }

        public override void Draw(GameObject obj, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture,
                obj.ScreenPosition,
                null,
                drawColor,
                0f,
                Vector2.Zero,
                1f,
                SpriteEffects.None,
                drawDepth);
        }
        
        public override void Receive<T>(string message, T obj)
        {
            string[] messageArray = message.Split('_');
            if (messageArray[0] == "GRAPHICS")
            {
                if (messageArray[1] == "DRAWCOLOR")
                {
                    if (obj is Color)
                    {
                        drawColor = (Color)(object)obj;
                    }
                }
            }
        }
    }
}
