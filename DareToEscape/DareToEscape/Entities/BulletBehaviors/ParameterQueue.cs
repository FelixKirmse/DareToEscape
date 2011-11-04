using System.Collections.Generic;
using System.Linq;

namespace DareToEscape.Entities.BulletBehaviors
{
    internal class ParameterQueue : IBehavior
    {
        private readonly Queue<Parameters> paramQueue;
        private IBehavior behavior;
        private int frameCounter;

        public ParameterQueue()
        {
            paramQueue = new Queue<Parameters>();
            frameCounter = 0;
            behavior = ReusableBehaviors.StandardBehavior;
        }

        public ParameterQueue(IBehavior behavior)
            : this()
        {
            this.behavior = behavior;
        }

        #region IBehavior Members

        public void Update(Bullet bullet)
        {
            frameCounter++;
            if (paramQueue.Count > 0)
            {
                if (frameCounter == paramQueue.First().ModOnFrame)
                {
                    Parameters p = paramQueue.Dequeue();
                    bullet.SetParameters(p);
                    frameCounter = 0;
                    behavior = p.NewBehavior;
                }
            }
            behavior.Update(bullet);
        }

        #endregion

        public void AddTask(int modOnFrame, float? newSpeed, float? newAngle, float newTurnSpeed, float newAcceleration,
                            float newSpeedLimit)
        {
            var newParams = new Parameters(modOnFrame, newSpeed, newAngle, newTurnSpeed, newAcceleration, newSpeedLimit);
            paramQueue.Enqueue(newParams);
        }
    }

    public struct Parameters
    {
        public int ModOnFrame;
        public float NewAcceleration;
        public float? NewAngle;
        public IBehavior NewBehavior;
        public float? NewSpeed;
        public float NewSpeedLimit;
        public float NewTurnSpeed;

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