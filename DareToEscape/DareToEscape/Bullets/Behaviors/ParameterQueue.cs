using System.Collections.Generic;

namespace DareToEscape.Bullets.Behaviors
{
    internal class ParameterQueue : IBehavior
    {
        private static readonly Dictionary<int, ParameterQueue> ActivePqs = new Dictionary<int, ParameterQueue>(50000);
        private static readonly Stack<ParameterQueue> InActivePqs = new Stack<ParameterQueue>(10000);
        private static int _idCounter;
        public readonly int ID;
        private readonly Queue<Parameters> _paramQueue;
        private IBehavior _behavior;
        private int _frameCounter;

        private ParameterQueue(int id)
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
                    Parameters p = _paramQueue.Dequeue();
                    bullet.SetParameters(p);
                    _frameCounter = 0;
                    _behavior = p.NewBehavior;
                    if (_paramQueue.Count == 0)
                    {
                        bullet.Behavior = _behavior;
                        SetInactive(this);
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
            SetInactive(this);
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

        public static ParameterQueue GetInstance()
        {
            lock (ActivePqs)
            {
                lock (InActivePqs)
                {
                    ParameterQueue pq = InActivePqs.Count > 0 ? InActivePqs.Pop() : new ParameterQueue(_idCounter++);
                    ActivePqs.Add(pq.ID, pq);
                    return pq;
                }
            }
        }

        private static void SetInactive(ParameterQueue pq)
        {
            lock (ActivePqs)
            {
                lock (InActivePqs)
                {
                    ActivePqs.Remove(pq.ID);
                    InActivePqs.Push(pq);
                }
            }
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