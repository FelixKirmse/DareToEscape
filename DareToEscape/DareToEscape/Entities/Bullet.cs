using System;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Providers;
using BlackDragonEngine.TileEngine;
using DareToEscape.Components.Entities;
using DareToEscape.Entities.BulletBehaviors;
using DareToEscape.Providers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DareToEscape.Entities
{
    public class Bullet : GameObject
    {
        public static float SpeedModifier = 1.0f;
        private readonly IBehavior behavior;
        private Vector2 lastPosition;

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
            AutomaticCollision = false;
            KillTime = -1;
            Send("GRAPHICS_BULLETID", id);
            _directionVector = new Vector2();
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

        private float _directionInDegrees;
        private Vector2 _directionVector;
        public float BaseSpeed { get; set; }
        public bool Active { get; set; }
        public float Direction
        {
            get { return _directionInDegrees; }
            set
            {
                if (!ChangedDirection) return;
                var radian = MathHelper.ToRadians(value);
                _directionVector.X = (float) Math.Cos(radian);
                _directionVector.Y = (float) Math.Sin(radian);
                _directionInDegrees = value;
            }
        }
        private float LastDirection { get; set; }
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
                Vector2 direction = ((Player) VariableProvider.CurrentPlayer).PlayerBulletCollisionCircleCenter -
                                    CircleCollisionCenter;
                var angle = (float) Math.Atan2(direction.Y, direction.X);
                return new Vector2((float) Math.Cos(angle), (float) Math.Sin(angle));
            }
        }

        public float DirectionAngleToPlayer
        {
            get
            {
                Vector2 direction = ((Player) VariableProvider.CurrentPlayer).PlayerBulletCollisionCircleCenter -
                                    CircleCollisionCenter;
                var radians = (float) Math.Atan2(direction.Y, direction.X);
                return MathHelper.ToDegrees(radians);
            }
        }

        public Vector2 DirectionVector
        {
            get { return _directionVector; }
        }

        public bool ChangedDirection
        {
            get { return Direction == LastDirection; }
        }

        public bool ChangedPosition
        {
            get { return Position == lastPosition; }
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
                    if (!TileMap.CellIsPassableByPixel(CircleCollisionCenter) ||
                        !Camera.WorldRectangle.Contains((int) Position.X, (int) Position.Y))
                    {
                        Active = false;
                    }
                }
                /*else if (!Camera.WorldRectangle.Contains((int)Position.X, (int)Position.Y))
                {
                    Active = false;
                }*/

                if (CollisionCircle.Intersects(((Player) VariableProvider.CurrentPlayer).PlayerBulletCollisionCircle))
                {
                    Active = false;
                    //VariableProvider.CurrentPlayer.Send<string>("KILL", null);
                }

                LastDirection = Direction;
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
                BaseSpeed = (float) newSpeed;
                LaunchSpeed = (float) newSpeed;
            }
            if (angle != null)
            {
                Direction = (float) angle;
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
            Direction = direction;
            GameVariableProvider.BulletManager.AddBullet(this);
        }

        public override string ToString()
        {
            return "BaseSpeed: " + BaseSpeed + " LaunchSpeed: " + LaunchSpeed + " Acceleration: " + Acceleration +
                   " SpeedLimit: " + SpeedLimit + " TurnSpeed: " + TurnSpeed;
        }
    }
}