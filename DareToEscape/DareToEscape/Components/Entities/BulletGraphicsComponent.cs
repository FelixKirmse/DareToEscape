﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Components;
using DareToEscape.Providers;
using BlackDragonEngine.Helpers;
using DareToEscape.Entities;
using DareToEscape.Managers;

namespace DareToEscape.Components.Entities
{
    class BulletGraphicsComponent : GraphicsComponent
    {
        private Color drawColor;
        private int bulletID;
        private Rectangle sourceRect;
        private BlendState blendState;

        private float rotation;
        private float Rotation
        { 
            get
            { 
                return rotation; 
            } 
            set 
            {
                rotation = value; 
            }
        }

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
            //spriteBatch.Draw(BlackDragonEngine.Providers.VariableProvider.WhiteTexture, Camera.WorldToScreen(obj.CircleCollisionCenter), new Rectangle(0, 0, 1, 1), Color.White);

            if (((Bullet)obj).BaseSpeed < 0)
            {
                Rotation += MathHelper.Pi;
            }

            blendState = bulletID == 172 ? BlendState.Additive : BlendState.AlphaBlend;
            DrawHelper.AddNewJob(blendState,
                texture,
                Camera.WorldToScreen(obj.Position + obj.BCircleLocalCenter),
                sourceRect,
                drawColor,
                rotation,
                new Vector2((float)sourceRect.Width / 2, (float)sourceRect.Height / 2),
                1f,
                SpriteEffects.None,
                BulletManager.CurrentDrawDepth);

            /*if (bulletID == 172)
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.Additive);                
            }

            spriteBatch.Draw(
                texture,
                Camera.WorldToScreen(obj.Position + obj.BCircleLocalCenter),
                sourceRect,
                drawColor,
                rotation,
                new Vector2((float)sourceRect.Width / 2,(float)sourceRect.Height / 2),  
                1f,
                SpriteEffects.None,
                BulletManager.CurrentDrawDepth);

            if (bulletID == 172)
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            }*/
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

                if (messageArray[1] == "BULLETID")
                {
                    if (obj is int)
                    {
                        bulletID = (int)(object)obj;
                        sourceRect = BulletInformationProvider.GetSourceRectangle(bulletID);
                    }
                }

                if (messageArray[1] == "ROTATION")
                {
                    if (obj is float)
                        Rotation = MathHelper.ToRadians((float)(object)obj + 90);
                }
            }
        }
    }
}
