using System;
using System.Collections.Generic;
using BlackDragonEngine.Components;
using BlackDragonEngine.Providers;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using BlackDragonEngine.Entities;
using BlackDragonEngine.TileEngine;
using BlackDragonEngine.Helpers;

namespace DareToEscape.Components.Entities
{
    class TurretComponent : GraphicsComponent
    {
        protected float rotation;
        protected Vector2 RotationOrigin { get { return new Vector2(texture.Width / 2, texture.Height / 2); } }                
        protected Vector2 bulletOrigin;

        public override void Update(GameObject obj)
        {
            if (ShootCondition(VariableProvider.CurrentPlayer.CollisionCenter, obj))
            {
                if (!VariableProvider.ScriptEngine.IsScriptRunning(ShootBehavior))
                {
                    VariableProvider.ScriptEngine.ExecuteSript(ShootBehavior);
                }
            }            
        }

        public override void Draw(GameObject obj, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture,
                Camera.WorldToScreen(obj.Position + RotationOrigin),
                null,
                Color.White,
                rotation,
                RotationOrigin,
                1f,
                SpriteEffects.None,
                drawDepth);
        }

        protected virtual bool ShootCondition(Vector2 playerPosition, GameObject turret)
        {
            float startX = Camera.Position.X;
            float endX = Camera.Position.X + Camera.ViewPortWidth;
            float startY = Camera.Position.Y;
            float endY = Camera.Position.Y + Camera.ViewPortHeight;

            Vector2 pos = turret.Position;

            if(pos.X >= startX && pos.X <= endX && pos.Y >= startY && pos.Y <= endY)
            {
                Vector2 direction = playerPosition - bulletOrigin;
                direction /= (TileMap.TileWidth * 32);
                Vector2 particlePosition = bulletOrigin;

                while (particlePosition != playerPosition)
                {
                    if (!TileMap.CellIsPassableByPixel(particlePosition))
                        return false;
                    particlePosition += direction;
                }
                return true;
            }
            return false;            
        }

        protected virtual IEnumerator<int> ShootBehavior()
        {
            throw new NotImplementedException();
        }

        protected void SetDown(GameObject obj)
        {
            if(texture.Height != 16)
                obj.Position += new Vector2(0, -texture.Height / 2);
            if (texture.Height == 64)
                obj.Position += new Vector2(0, -texture.Height / 4);
            bulletOrigin = obj.Position + RotationOrigin + new Vector2(0, -texture.Height / 2 );
            rotation = MathHelper.ToRadians(180);
        }

        protected void SetLeft(GameObject obj)
        {
            bulletOrigin = obj.Position + RotationOrigin + new Vector2(texture.Width / 2, 0);
            rotation = MathHelper.ToRadians(270);
        }

        protected void SetRight(GameObject obj)
        {
            if (texture.Height != 16) 
                obj.Position += new Vector2(-texture.Width / 2, 0);
            if(texture.Height == 64)
                obj.Position += new Vector2(-texture.Width / 4, 0);
            bulletOrigin = obj.Position + RotationOrigin + new Vector2(-texture.Width / 2, 0);
            rotation = MathHelper.ToRadians(90);
        }

        protected void SetUp(GameObject obj)
        {
            bulletOrigin = obj.Position + RotationOrigin + new Vector2(0, texture.Height / 2);
            rotation = MathHelper.ToRadians(0);
        }

        public override void Receive<T>(string message, T obj)
        {
            string[] messageParts = message.Split('_');
            if (messageParts[0] == "SET")
            {
                switch (messageParts[1])
                { 
                    case "UP":
                        SetUp((GameObject)(object)obj);
                        break;
                    case "DOWN":
                        SetDown((GameObject)(object)obj);
                        break;
                    case "LEFT":
                        SetLeft((GameObject)(object)obj);
                        break;
                    case "RIGHT":
                        SetRight((GameObject)(object)obj);
                        break;
                }
            }
        }
    }
}
