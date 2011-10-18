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
                Vector2 direction = ((Player)VariableProvider.CurrentPlayer).PlayerBulletCollisionCircleCenter - CircleCollisionCenter;
                float angle = (float)Math.Atan2(direction.Y, direction.X);
                return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            }
        }

        public float DirectionAngleToPlayer
        {
            get 
            {
                Vector2 direction = ((Player)VariableProvider.CurrentPlayer).PlayerBulletCollisionCircleCenter - CircleCollisionCenter;
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

        public Bullet(int id)
        {
            collisionCircle = BulletInformationProvider.GetBCircle(id);
            components.Add(new BulletGraphicsComponent(BulletInformationProvider.BulletSheet));
            Active = false;
            behavior = ReusableBehaviors.StandardBehavior;
            TurnSpeed = 0f;
            BaseSpeed = 1f;
            Acceleration = 0f;
            SpeedLimit = 1f;
            AutomaticCollision = true;
            KillTime = -1;
            Send("GRAPHICS_BULLETID", id);
        }

        public Bullet()
            : this(1)
        { 
        }

        public Bullet(Vector2 position, int id)
            : this(id)
        {
            Position = position;
        }        

        public Bullet(IBehavior behavior, Vector2 position, int id)
            : this(position, id)
        {
            this.behavior = behavior;
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
                    if (!TileMap.CellIsPassableByPixel(CircleCollisionCenter) || !Camera.WorldRectangle.Contains((int)Position.X, (int)Position.Y))
                    {
                        Active = false;
                    }
                }
                /*else if (!Camera.WorldRectangle.Contains((int)Position.X, (int)Position.Y))
                {
                    Active = false;
                }*/
                    
                if (CollisionCircle.Intersects(((Player)VariableProvider.CurrentPlayer).PlayerBulletCollisionCircle))
                {
                    Active = false;
                    VariableProvider.CurrentPlayer.Send<string>("KILL", null);
                }              

                lastDirection = Direction;
                lastPosition = Position;

                Send("GRAPHICS_ROTATION", Direction);
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
