using System;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Providers;
using BlackDragonEngine.TileEngine;
using DareToEscape.Entities.BulletBehaviors;
using DareToEscape.Providers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DareToEscape.Entities
{
    public struct Bullet
    {
        #region Fields
        private readonly IBehavior _behavior;
        private Vector2 _lastPosition;
        private float _lastDirection;
        public float TurnSpeed;
        public float Acceleration;
        public float SpeedLimit;
        public float LaunchSpeed;
        public bool AutomaticCollision;
        public int SpawnDelay;
        public int KillTime;
        private float _directionInDegrees;
        private Vector2 _directionVector;
        public float BaseSpeed;
        public bool Active;
        public Vector2 Position;
        private BCircle _collisionCircle;
        private readonly BlendState _blendState;
        private readonly Texture2D _texture;
        private Rectangle _sourceRect;
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
                var radian = MathHelper.ToRadians(value);
                _directionVector.X = (float) Math.Cos(radian);
                _directionVector.Y = (float) Math.Sin(radian);
                _directionInDegrees = value;
            }
        }
        

        public Vector2 DirectionVectorToPlayer
        {
            get
            {
                var direction = ((Player) VariableProvider.CurrentPlayer).PlayerBulletCollisionCircleCenter -
                                    CircleCollisionCenter;
                var angle = (float) Math.Atan2(direction.Y, direction.X);
                return new Vector2((float) Math.Cos(angle), (float) Math.Sin(angle));
            }
        }

        public float DirectionAngleToPlayer
        {
            get
            {
                var direction = ((Player) VariableProvider.CurrentPlayer).PlayerBulletCollisionCircleCenter -
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
            Active = false;
            _behavior = ReusableBehaviors.StandardBehavior;
            TurnSpeed = 0f;
            BaseSpeed = 1f;
            Acceleration = 0f;
            SpeedLimit = 1f;
            AutomaticCollision = true;
            KillTime = -1;
            _sourceRect = BulletInformationProvider.GetSourceRectangle(id);
            _directionVector = new Vector2();
            _blendState = BlendState.AlphaBlend;
        }


        public Bullet(Vector2 position, int id)
            : this(id)
        {
            Position = position;
        }

        public Bullet(IBehavior behavior, Vector2 position, int id)
            : this(position, id)
        {
            _behavior = behavior;
        }

        public Bullet(IBehavior behavior, Vector2 position, int id, BlendState blendState)
            : this(behavior, position, id)
        {
            _blendState = blendState;
        }
        #endregion

        public Bullet Update()
        {
            --KillTime;
            if (KillTime == 0)
                Active = false;
            if (SpawnDelay > 0)
            {
                --SpawnDelay;
            }
            if (!Active || SpawnDelay != 0) return this;

            _behavior.Update(ref this);
            
            if (AutomaticCollision)
            {
                if (!TileMap.CellIsPassableByPixel(CircleCollisionCenter) ||
                    !Camera.WorldRectangle.Contains((int) Position.X, (int) Position.Y))
                {
                    Active = false;
                }
            }

            if (CollisionCircle.Intersects(((Player) VariableProvider.CurrentPlayer).PlayerBulletCollisionCircle))
            {
                Active = false;
                //VariableProvider.CurrentPlayer.Send<string>("KILL", null);
            }

            _lastDirection = Direction;
            _lastPosition = Position;
            _rotation = MathHelper.ToRadians(_directionInDegrees);
            return this;
        }

        public void Draw()
        {
            DrawHelper.AddNewJob(_blendState,
                                 _texture,
                                 Camera.WorldToScreen(Position + BCircleLocalCenter),
                                 _sourceRect,
                                 Color.White,
                                 BaseSpeed < 0 ? _rotation += MathHelper.PiOver2 : _rotation,
                                 new Vector2((float)_sourceRect.Width / 2, (float)_sourceRect.Height / 2),
                                 1f,
                                 SpriteEffects.None,
                                 0);
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
            return "Position: " + Position +" BaseSpeed: " + BaseSpeed + " LaunchSpeed: " + LaunchSpeed + " Acceleration: " + Acceleration +
                   " SpeedLimit: " + SpeedLimit + " TurnSpeed: " + TurnSpeed;
        }
    }
}