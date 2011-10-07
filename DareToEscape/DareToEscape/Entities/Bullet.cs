using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackDragonEngine.Entities;
using Microsoft.Xna.Framework;
using DareToEscape.Components.Entities;
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
        private Texture2D bulletTexture;
        public static float SpeedModifier = 1.0f;
        public float BaseSpeed = 2f;        
        public bool Active { get; set; }
        public float Direction { get; set; }
        private float lastDirection { get; set; }
        private Vector2 lastPosition;

        public Vector2 DirectionVectorToPlayer
        {
            get 
            {
                Vector2 direction = ((Player)VariableProvider.CurrentPlayer).PlayerBulletCollisionRectCenter - CollisionCenter;
                float angle = (float)Math.Atan2(direction.Y, direction.X);
                return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            }
        }

        public float DirectionAngleToPlayer
        {
            get 
            {
                Vector2 direction = ((Player)VariableProvider.CurrentPlayer).PlayerBulletCollisionRectCenter - CollisionCenter;
                float radians = (float)Math.Atan2(direction.Y, direction.X);
                return MathHelper.ToDegrees(radians);
            }
        }

        public Vector2 DirectionVector
        {
            get 
            {
                float radians = MathHelper.ToRadians(Direction);
                return new Vector2((float)Math.Cos(radians), (float)Math.Sin(radians));
            }
        }

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
            collisionRectangle = new Rectangle(1, 1, 5, 5);
            bulletTexture = VariableProvider.Game.Content.Load<Texture2D>(@"textures/entities/bullet");
            components.Add(new BulletGraphicsComponent(bulletTexture));
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

        public Bullet(Behavior behavior, Vector2 position, Color color)
            : this(behavior, position)
        {
            Send("GRAPHICS_DRAWCOLOR", color);
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

        public void Shoot(float direction)
        {
            Active = true;
            this.Direction = direction;
            GameVariableProvider.BulletManager.AddBullet(this);
        }
    }
}
