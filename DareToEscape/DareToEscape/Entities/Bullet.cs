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
using DareToEscape.Providers;
using DareToEscape.Entities.BulletBehaviors;
using BlackDragonEngine.Helpers;

namespace DareToEscape.Entities
{
    public class Bullet : GameObject
    {
        private static Texture2D bulletTexture;
        public static float SpeedModifier = 1.0f;
        public float BaseSpeed = 2f;        
        public bool Active { get; set; }
        public Vector2 Direction;
        private Vector2 lastDirection;
        private Vector2 lastPosition;

        public bool ChangedDirection
        {
            get
            {
                return Direction == lastDirection;            
            }
        }

        public bool ChangedPosition
        {
            get
            {
                return Position == lastPosition;
            }
        }

        private Behavior behavior;

        public Bullet()
        {
            collisionRectangle = new Rectangle(0, 0, 7, 7);
            bulletTexture = VariableProvider.Game.Content.Load<Texture2D>(@"textures/entities/bullet");
            components.Add(new GraphicsComponent(bulletTexture));
            Active = false;
            behavior = ReusableBehaviors.StandardBehavior;
        }

        public Bullet(Vector2 position)
            : this()
        {
            Position = position;
        }

        public Bullet(Behavior behavior)
            : this()
        {
            this.behavior = behavior;
        }

        public Bullet(Behavior behavior, Vector2 position)
            : this(position)
        {
            this.behavior = behavior;
        }

        public override void Update()
        {
            if (Active)
            {
                behavior.Update(this);

                if (!TileMap.CellIsPassableByPixel(CollisionCenter) || !Camera.WorldRectangle.Contains((int)Position.X, (int)Position.Y))
                {
                    Active = false;   
                }
                    
                if (CollisionRectangle.Intersects(((Player)VariableProvider.CurrentPlayer).PlayerBulletCollisionRect))
                {
                    Active = false;
                    VariableProvider.CurrentPlayer.Send<string>("KILL", null);
                }

                lastDirection = Direction;
                lastPosition = Position;
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
            this.Direction = direction;
            GameVariableProvider.BulletManager.AddBullet(this);
        }
    }
}
