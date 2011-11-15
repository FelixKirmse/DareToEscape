using System.Collections.Generic;

namespace DareToEscape.Entities.BulletBehaviors
{
    internal class ParameterQueue : IBehavior
    {
        public readonly int ID;
        private readonly Queue<Parameters> _paramQueue;
        private IBehavior _behavior;
        private int _frameCounter;

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
           
            if (_paramQueue.Count > 0)
            {
                if (_frameCounter == _paramQueue.Peek().ModOnFrame)
                {
                    var p = _paramQueue.Dequeue();
                    bullet.SetParameters(p);
                    _frameCounter = 0;
                    _behavior = p.NewBehavior;
                    if (_paramQueue.Count == 0)
                    {
                        bullet.Behavior = _behavior;
                        ParameterQueueFactory.SetInactive(this);
                        _behavior.Update(ref bullet);
                        return;
                    }
                }
            }
            ++_frameCounter;
            _behavior.Update(ref bullet);
        }

        public void FreeRessources()
        {
            _paramQueue.Clear();
            _frameCounter = 0;
            _behavior = ReusableBehaviors.StandardBehavior;
            ParameterQueueFactory.SetInactive(this);
        }

        #endregion

        public void AddTask(int modOnFrame, float? newSpeed, float? newAngle, float newTurnSpeed, float newAcceleration,
                            float newSpeedLimit)
        {
            var newParams = new Parameters(modOnFrame, newSpeed, newAngle, newTurnSpeed, newAcceleration, newSpeedLimit);
            _paramQueue.Enqueue(newParams);
        }

        public override string ToString()
        {
            return ID.ToString();
        }
    }

    public struct Parameters
    {
        public readonly int ModOnFrame;
        public readonly float NewAcceleration;
        public readonly IBehavior NewBehavior;
        public readonly float NewSpeedLimit;
        public readonly float NewTurnSpeed;
        public float? NewAngle;
        public float? NewSpeed;

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