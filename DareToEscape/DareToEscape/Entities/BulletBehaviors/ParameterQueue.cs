using System.Collections.Generic;
using System.Linq;

namespace DareToEscape.Entities.BulletBehaviors
{
    internal class ParameterQueue : IBehavior
    {
        private readonly Queue<Parameters> _paramQueue;
        private IBehavior _behavior;
        private int _frameCounter;
        public readonly int ID;

        internal ParameterQueue(int id)
        {
            ID = id;
            _paramQueue = new Queue<Parameters>();
            _frameCounter = 0;
            _behavior = ReusableBehaviors.StandardBehavior;
        }

        #region IBehavior Members

        public void Update(ref Bullet bullet)
        {
            _frameCounter++;
            if (_paramQueue.Count > 0)
            {
                if (_frameCounter == _paramQueue.First().ModOnFrame)
                {
                    var p = _paramQueue.Dequeue();
                    bullet.SetParameters(p);
                    _frameCounter = 0;
                    _behavior = p.NewBehavior;
                    if(_paramQueue.Count == 0)
                    {
                        bullet.Behavior = _behavior;
                        ParameterQueueFactory.SetInactive(this);
                    }
                }
            }
            _behavior.Update(ref bullet);
        }

        #endregion

        public void AddTask(int modOnFrame, float? newSpeed, float? newAngle, float newTurnSpeed, float newAcceleration,
                            float newSpeedLimit)
        {
            var newParams = new Parameters(modOnFrame, newSpeed, newAngle, newTurnSpeed, newAcceleration, newSpeedLimit);
            _paramQueue.Enqueue(newParams);
        }
    }

    public struct Parameters
    {
        public readonly int ModOnFrame;
        public readonly float NewAcceleration;
        public float? NewAngle;
        public readonly IBehavior NewBehavior;
        public float? NewSpeed;
        public readonly float NewSpeedLimit;
        public readonly float NewTurnSpeed;

        public Parameters(int modOnFrame, float? newSpeed, float? newAngle, float newTurnSpeed, float newAcceleration,
                          float newSpeedLimit)
        {
            ModOnFrame = modOnFrame;
            NewSpeed = newSpeed;
            NewAngle = newAngle;
            NewTurnSpeed = newTurnSpeed;
            NewAcceleration = newAcceleration;
            NewSpeedLimit = newSpeedLimit;
            NewBehavior = ReusableBehaviors.StandardBehavior;
        }

        public Parameters(int modOnFrame, float? newSpeed, float? newAngle, float newTurnSpeed, float newAcceleration,
                          float newSpeedLimit, IBehavior newBehavior)
            : this(modOnFrame, newSpeed, newAngle, newTurnSpeed, newAcceleration, newSpeedLimit)
        {
            NewBehavior = newBehavior;
        }
    }
}