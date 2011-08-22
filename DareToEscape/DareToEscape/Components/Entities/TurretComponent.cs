using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackDragonEngine.Components;
using BlackDragonEngine.Providers;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using DareToEscape.Entities;
using BlackDragonEngine.Entities;
using BlackDragonEngine.HelpMaps;
using BlackDragonEngine.Helpers;

namespace DareToEscape.Components.Entities
{
    class TurretComponent : GraphicsComponent
    {
        protected float rotation;
        protected Vector2 RotationOrigin { get { return new Vector2(texture.Width / 2, texture.Height / 2); } }
                
        protected Vector2 bulletOrigin;

        protected float waveTimer = 2000f;
        protected float bulletTimer = 166f;

        protected float elapsedWaveTime;
        protected float elapsedBulletTime;

        protected int waveCount = 5;
        protected int counter;

        protected List<Bullet> bullets = new List<Bullet>();

        public override void Update(GameObject obj)
        {
            if (ShootCondition(VariableProvider.CurrentPlayer.CollisionCenter, obj))
            {
                elapsedWaveTime += ShortcutProvider.ElapsedMilliseconds;
                if (elapsedWaveTime >= waveTimer)
                {
                    elapsedBulletTime += ShortcutProvider.ElapsedMilliseconds;
                    if (elapsedBulletTime >= bulletTimer)
                    {
                        ShootWave();
                        counter++;
                        elapsedBulletTime = 0;
                        if (counter == waveCount)
                        {
                            elapsedWaveTime = 0;
                            counter = 0;
                        }
                    }
                }
            }
            else
            {
                elapsedBulletTime = bulletTimer;
                elapsedWaveTime = waveTimer;
            }

            for (int i = 0; i < bullets.Count; ++i)
            {
                bullets[i].Update();
                if (!bullets[i].Active)
                {
                    bullets.RemoveAt(i);
                    --i;
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

            foreach (Bullet bullet in bullets)
            {
                bullet.Draw(spriteBatch);
            }
        }

        protected virtual bool ShootCondition(Vector2 playerPosition, GameObject turret)
        {
            Vector2 direction = playerPosition - bulletOrigin;
            direction /= (TileMap.TileWidth * 4);
            Vector2 particlePosition = bulletOrigin;

            while (particlePosition != playerPosition)
            {
                if (!TileMap.CellIsPassableByPixel(particlePosition))
                    return false;
                particlePosition += direction;
            }
            return true;
        }

        protected virtual void ShootWave()
        {
            throw new NotImplementedException();
        }

        protected void SetDown(GameObject obj)
        {
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
