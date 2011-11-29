using System;
using System.Collections.Generic;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Providers;
using BlackDragonEngine.TileEngine;
using DareToEscape.Bullets.Behaviors;
using DareToEscape.Entities;
using DareToEscape.Providers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DareToEscape.Bullets
{
    public struct Bullet
    {
        #region Fields

        private readonly BlendState _blendState;
        private readonly Texture2D _texture;
        public float Acceleration;
        public bool Active;
        public bool AutomaticCollision;
        public IBehavior Behavior;
        public int KillTime;
        public float LaunchSpeed;
        public Vector2 Position;
        public int SpawnDelay;
        public float SpeedLimit;
        public float TurnSpeed;
        public float Velocity;
        private BCircle _collisionCircle;
        private float _directionInDegrees;
        private Vector2 _directionVector;
        private float _lastDirection;
        private Vector2 _lastPosition;
        private float _rotation;
        private Rectangle _sourceRect;

        #endregion

        #region Properties

        private BCircle CollisionCircle
        {
            get { return new BCircle(Position + _collisionCircle.Position, _collisionCircle.Radius); }
        }

        private Vector2 BCircleLocalCenter
        {
            get { return _collisionCircle.Position; }
        }

        public Vector2 CircleCollisionCenter
        {
            get { return Position + _collisionCircle.Position; }
        }

        public float Direction
        {
            get { return _directionInDegrees; }
            set
            {
                if (!ChangedDirection) return;
                float radian = MathHelper.ToRadians(value);
                _directionVector.X = (float) Math.Cos(radian);
                _directionVector.Y = (float) Math.Sin(radian);
                _directionInDegrees = value;
            }
        }


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

        private bool ChangedDirection
        {
            get { return Direction == _lastDirection; }
        }

        public bool ChangedPosition
        {
            get { return Position == _lastPosition; }
        }

        #endregion

        #region constructors

        private Bullet(int id)
            : this()
        {
            _collisionCircle = BulletInformationProvider.GetBCircle(id);
            _texture = BulletInformationProvider.BulletSheet;
            Behavior = ReusableBehaviors.StandardBehavior;
            Velocity = 1f;
            SpeedLimit = 1f;
            AutomaticCollision = true;
            KillTime = -1;
            _sourceRect = BulletInformationProvider.GetSourceRectangle(id);
            _blendState = BlendState.AlphaBlend;
        }


        public Bullet(Vector2 position, int id)
            : this(id)
        {
            Position = position - _collisionCircle.Position;
        }

        public Bullet(Vector2 position, int id, BlendState blendState)
            : this(position, id)
        {
            _blendState = blendState;
        }

        public Bullet(IBehavior behavior, Vector2 position, int id)
            : this(position, id)
        {
            Behavior = behavior;
        }

        public Bullet(IBehavior behavior, Vector2 position, int id, BlendState blendState)
            : this(behavior, position, id)
        {
            _blendState = blendState;
        }

        #endregion

        public Bullet Update(int id, List<int> bulletsToDelete)
        {
            --KillTime;
            if (KillTime == 0)
            {
                Active = false;
                bulletsToDelete.Add(id);
                Clear();
                return this;
            }

            if (SpawnDelay > 0)
            {
                --SpawnDelay;
            }
            if (!Active || SpawnDelay != 0) return this;

            Behavior.Update(ref this);

            if (AutomaticCollision)
            {
                if (!TileMap.CellIsPassableByPixel(CircleCollisionCenter) ||
                    !Camera.WorldRectangle.Contains((int) Position.X, (int) Position.Y))
                {
                    Active = false;
                    bulletsToDelete.Add(id);
                    Clear();
                    return this;
                }
            }

            if (CollisionCircle.Intersects(((Player) VariableProvider.CurrentPlayer).PlayerBulletCollisionCircle))
            {
                Active = false;
                bulletsToDelete.Add(id);
                Clear();
                return this;
                //VariableProvider.CurrentPlayer.Send<string>("KILL", null);
            }

            _lastDirection = Direction;
            _lastPosition = Position;
            _rotation = MathHelper.ToRadians(_directionInDegrees + 90f);
            return this;
        }

        public void Draw()
        {
            DrawHelper.AddNewJob(_blendState,
                                 _texture,
                                 Camera.WorldToScreen(Position + BCircleLocalCenter),
                                 _sourceRect,
                                 Color.White,
                                 Velocity < 0 ? _rotation += MathHelper.Pi : _rotation,
                                 new Vector2((float) _sourceRect.Width/2, (float) _sourceRect.Height/2),
                                 1f,
                                 SpriteEffects.None,
                                 0);
        }

        public void SetNewSpeedRules(float newSpeed, float speedLimit)
        {
            Velocity = newSpeed;
            LaunchSpeed = newSpeed;
            SpeedLimit = speedLimit;
        }

        public void Clear()
        {
            Behavior.FreeRessources();
        }

        public void SetParameters(Parameters p)
        {
            SetParameters(p.NewSpeed, p.NewAngle, p.NewTurnSpeed, p.NewAcceleration, p.NewSpeedLimit);
        }

        public void SetParameters(float? newSpeed, float? angle, float turnSpeed, float acceleration, float speedLimit)
        {
            if (newSpeed != null)
            {
                Velocity = (float) newSpeed;
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

        public void Shoot(float direction, float startingSpeed)
        {
            LaunchSpeed = startingSpeed;
            Velocity = startingSpeed;
            Active = true;
            Direction = direction;
            GameVariableProvider.BulletManager.AddBullet(this);
        }

        public override string ToString()
        {
            //return "Position: " + Position +" BaseSpeed: " + BaseSpeed + " LaunchSpeed: " + LaunchSpeed + " Acceleration: " + Acceleration +
            //     " SpeedLimit: " + SpeedLimit + " TurnSpeed: " + TurnSpeed;
            return ((ParameterQueue) Behavior).ID.ToString();
        }
    }
}