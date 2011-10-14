using System;
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
        public float BaseSpeed { get; set; }     
        public bool Active { get; set; }
        public float Direction { get; set; }
        private float lastDirection { get; set; }
        private Vector2 lastPosition;
        public float TurnSpeed { get; set; }
        public float Acceleration { get; set; }
        public float SpeedLimit { get; set; }
        public float LaunchSpeed { get; set; }
        public bool AutomaticCollision { get; set; }
        public int SpawnDelay { get; set; }
        public int KillTime { get; set; }

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

        private IBehavior behavior;

        public Bullet()
        {
            collisionRectangle = new Rectangle(1, 1, 5, 5);
            bulletTexture = VariableProvider.Game.Content.Load<Texture2D>(@"textures/entities/bullet");
            components.Add(new BulletGraphicsComponent(bulletTexture));
            Active = false;
            behavior = ReusableBehaviors.StandardBehavior;
            TurnSpeed = 0f;
            BaseSpeed = 1f;
            Acceleration = 0f;
            SpeedLimit = 1f;
            AutomaticCollision = false;
            KillTime = -1;
        }

        public Bullet(Vector2 position)
            : this()
        {
            Position = position;
        }

        public Bullet(IBehavior behavior, Vector2 position)
            : this(position)
        {
            this.behavior = behavior;
        }

        public Bullet(IBehavior behavior, Vector2 position, Color color)
            : this(behavior, position)
        {
            Send("GRAPHICS_DRAWCOLOR", color);
        }

        public override void Update()
        {
            --KillTime;
            if (KillTime == 0)
                Active = false;            
            if (SpawnDelay > 0)
            {
                --SpawnDelay;                
            }
            if (Active && SpawnDelay == 0)
            {
                behavior.Update(this);
                if (AutomaticCollision)
                {
                    if (!TileMap.CellIsPassableByPixel(CollisionCenter) || !Camera.WorldRectangle.Contains((int)Position.X, (int)Position.Y))
                    {
                        Active = false;
                    }
                }
                /*else if (!Camera.WorldRectangle.Contains((int)Position.X, (int)Position.Y))
                {
                    Active = false;
                }*/
                    
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

        public void SetNewSpeedRules(float newSpeed, float speedLimit)
        {
            BaseSpeed = newSpeed;
            LaunchSpeed = newSpeed;
            SpeedLimit = speedLimit;
        }

        public void SetParameters(Parameters p)
        {
            SetParameters(p.NewSpeed, p.NewAngle, p.NewTurnSpeed, p.NewAcceleration, p.NewSpeedLimit);
        }

        public void SetParameters(float? newSpeed, float? angle, float turnSpeed, float acceleration, float speedLimit)
        {
            if (newSpeed != null)
            {
                BaseSpeed = (float)newSpeed;
                LaunchSpeed = (float)newSpeed;
            }
            if (angle != null)
            {
                Direction = (float)angle;
            }

            TurnSpeed = turnSpeed;
            Acceleration = acceleration;
            SpeedLimit = speedLimit;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Active)
                base.Draw(spriteBatch);
        }

        public void Shoot(float direction, float startingSpeed)
        {
            LaunchSpeed = startingSpeed;
            BaseSpeed = startingSpeed;
            Active = true;
            this.Direction = direction;
            GameVariableProvider.BulletManager.AddBullet(this);
        }

        public override string ToString()
        {
            return "BaseSpeed: " + BaseSpeed + " LaunchSpeed: " + LaunchSpeed + " Acceleration: " + Acceleration + " SpeedLimit: " + SpeedLimit + " TurnSpeed: " + TurnSpeed;
        }
    }
}
