using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackDragonEngine.Entities;
using Microsoft.Xna.Framework;
using BlackDragonEngine.Components;
using Microsoft.Xna.Framework.Graphics;
using BlackDragonEngine.Providers;
using BlackDragonEngine.TileEngine;

namespace DareToEscape.Entities
{
    class Bullet : GameObject
    {
        private static Texture2D bulletTexture;
        public static float SpeedModifier = 1.0f;
        private float baseSpeed = 2f;
        public bool Active { get; private set; }
        private Vector2 direction;

        public Bullet()
        {
            collisionRectangle = new Rectangle(0, 0, 7, 7);
            bulletTexture = VariableProvider.Game.Content.Load<Texture2D>(@"textures/entities/bullet");
            components.Add(new GraphicsComponent(bulletTexture));
            Active = false;
        }

        public override void Update()
        {
            if (Active)
            {
                position += direction * baseSpeed;
                if (!TileMap.CellIsPassableByPixel(CollisionCenter))
                    Active = false;
                if (CollisionRectangle.Intersects(VariableProvider.CurrentPlayer.CollisionRectangle))
                {
                    Active = false;
                    VariableProvider.CurrentPlayer.Send<string>("KILL", null);
                }
            }
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Active)
                base.Draw(spriteBatch);
        }

        public void Shoot(Vector2 direction)
        {
            Active = true;
            this.direction = direction;
        }
    }
}
