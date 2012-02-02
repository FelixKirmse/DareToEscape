using System;
using System.Collections.Concurrent;
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
        private static ulong _currentID;
        private static readonly ConcurrentStack<ulong> UsableIDs = new ConcurrentStack<ulong>();
        private static readonly ConcurrentDictionary<ulong, Dictionary<string, AnimationStripStruct>> Dictionaries = new ConcurrentDictionary<ulong, Dictionary<string, AnimationStripStruct>>();

        private static ulong GetID()
        {
            ulong id;
            if (!UsableIDs.TryPop(out id))
            {
                lock(UsableIDs)
                {
                    ++_currentID;
                }
                return _currentID;
            }
            return id;
        }

        private static Dictionary<string, AnimationStripStruct> GetDictionary(ulong id)
        {
            if (Dictionaries.ContainsKey(id))
                return Dictionaries[id];
            var dict = new Dictionary<string, AnimationStripStruct>();
            Dictionaries.TryAdd(id, dict);
            return dict;
        }

        private static void SetInactive(ulong id)
        {
            lock(Dictionaries)
            {
                Dictionaries[id].Clear();
            }
            UsableIDs.Push(id);
        }

        #region Constants

        private const string Create = "Create";
        private const string Loop = "Loop";
        private const string Death = "Death";
        private const string Static = "Static";

        #endregion

        #region Fields

        private readonly BlendState _blendState;
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
        private Dictionary<string, AnimationStripStruct> _animations;
        private BCircle _collisionCircle;
        private float _directionInDegrees;
        private Vector2 _directionVector;
        private float _lastDirection;
        private Vector2 _lastPosition;
        private readonly bool _staticAnimation;
        private string _currentAnimation;
        private bool _dieing;
        private readonly ulong _id;

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
            _id = GetID();
            _animations = GetDictionary(_id);
            //Not using foreach here in order to avoid unnecessary Heap allocations
            id = VariableProvider.RandomSeed.Next(0, 21);
            var tmp = BulletInformationProvider.GetAnimationStrip(id);
            if(tmp.Count == 1)
            {
                _animations.Add(Static, tmp[Static]);
                _currentAnimation = Static;
                _staticAnimation = true;
            }
            else
            {
                _animations.Add(Create, tmp[Create]);
                _animations.Add(Loop, tmp[Loop]);
                _animations.Add(Death, tmp[Death]);
                _currentAnimation = Create;
            }
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
            if(_dieing)
            {
                if(_staticAnimation)
                {
                    SetInactive(_id);
                    bulletsToDelete.Add(id);
                    return this;
                }
                UpdateAnimation();
                if(_animations[_currentAnimation].FinishedPlaying)
                {
                    SetInactive(_id);
                    bulletsToDelete.Add(id);
                }
                return this;
            }
            --KillTime;
            if (KillTime == 0)
            {
                Clear();
                return this;
            }

            if (SpawnDelay > 0)
            {
                --SpawnDelay;
            }
            if (SpawnDelay != 0) return this;

            if(!_staticAnimation)
            {
                UpdateAnimation();
            }
            Behavior.Update(ref this);

            if (AutomaticCollision)
            {
                if (!_tileMap.CellIsPassableByPixel(CircleCollisionCenter) ||
                    !Camera.WorldRectangle.Contains((int) Position.X, (int) Position.Y))
                {
                    Clear();
                    return this;
                }
            }

            if (CollisionCircle.Intersects(((Player) VariableProvider.CurrentPlayer).PlayerBulletCollisionCircle))
            {
                Clear();
                return this;
                //VariableProvider.CurrentPlayer.Send<string>("KILL", null);
            }
            _directionVector.Normalize();
            _lastDirection = Direction;
            _lastPosition = Position;
            return this;
        }

        public void Draw()
        {
            var animation = _animations[_currentAnimation];
            Rectangle rect = animation.FrameRectangle;
            DrawHelper.AddNewJob(_blendState,
                                 animation.Texture,
                                 Camera.WorldToScreen(Position + BCircleLocalCenter),
                                 rect,
                                 Color.White,
                                 0f,
                                 new Vector2((float) rect.Width/2, (float) rect.Height/2),
                                 1f,
                                 SpriteEffects.None,
                                 0);
        }

        private void UpdateAnimation()
        {
            var animation = _animations[_currentAnimation];
            animation.Update();
            if (animation.FinishedPlaying && animation.NextAnimation != null)
            {
                _currentAnimation = animation.NextAnimation;
                animation = _animations[_currentAnimation];
                animation.Play();
            }
            _animations[_currentAnimation] = animation;
        }

        private void Clear()
        {
            Behavior.FreeRessources();
            _dieing = true;
            if (_staticAnimation) return;
            var animation = _animations[_currentAnimation];
            animation.NextAnimation = Death;
            animation.LoopAnimation = false;
            _animations[_currentAnimation] = animation;
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