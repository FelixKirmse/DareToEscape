using System;
using System.Collections.Generic;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Providers;
using BlackDragonEngine.TileEngine;
using DareToEscape.Bullets.Behaviors;
using DareToEscape.Entities;
using DareToEscape.Managers;
using DareToEscape.Providers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DareToEscape.Bullets
{
    public struct Bullet
    {
        #region Fields

        private readonly BlendState _blendState;
        private AnimationStripStruct _animations;
        private readonly TileMap<Map<TileCode>, TileCode> _tileMap;
        public float Acceleration;
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
            _animations = BulletInformationProvider.GetAnimationStrip(id);
            Behavior = ReusableBehaviors.StandardBehavior;
            Velocity = 1f;
            SpeedLimit = 1f;
            AutomaticCollision = true;
            KillTime = -1;
            _blendState = BlendState.AlphaBlend;
            _tileMap = TileMap<Map<TileCode>, TileCode>.GetInstance();
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
                bulletsToDelete.Add(id);
                Clear();
                return this;
            }

            if (SpawnDelay > 0)
            {
                --SpawnDelay;
            }
            if (SpawnDelay != 0) return this;

            _animations.Update();
            Behavior.Update(ref this);

            if (AutomaticCollision)
            {
                if (!_tileMap.CellIsPassableByPixel(CircleCollisionCenter) ||
                    !Camera.WorldRectangle.Contains((int) Position.X, (int) Position.Y))
                {
                    bulletsToDelete.Add(id);
                    Clear();
                    return this;
                }
            }

            if (CollisionCircle.Intersects(((Player) VariableProvider.CurrentPlayer).PlayerBulletCollisionCircle))
            {
                bulletsToDelete.Add(id);
                Clear();
                return this;
                //VariableProvider.CurrentPlayer.Send<string>("KILL", null);
            }
            _directionVector.Normalize();
            _lastDirection = Direction;
            _lastPosition = Position;
            _rotation = MathHelper.ToRadians(_directionInDegrees + 90f);
            return this;
        }

        public void Draw()
        {
            Rectangle rect = _animations.FrameRectangle;
            DrawHelper.AddNewJob(_blendState,
                                 _animations.Texture,
                                 Camera.WorldToScreen(Position + BCircleLocalCenter),
                                 rect,
                                 Color.White,
                                 Velocity < 0 ? _rotation += MathHelper.Pi : _rotation,
                                 new Vector2((float) rect.Width/2, (float) rect.Height/2),
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
            Direction = direction;
            BulletManager.GetInstance().AddBullet(this);
        }

        public override string ToString()
        {
            //return "Position: " + Position +" BaseSpeed: " + BaseSpeed + " LaunchSpeed: " + LaunchSpeed + " Acceleration: " + Acceleration +
            //     " SpeedLimit: " + SpeedLimit + " TurnSpeed: " + TurnSpeed;
            return ((ParameterQueue) Behavior).ID.ToString();
        }
    }
}