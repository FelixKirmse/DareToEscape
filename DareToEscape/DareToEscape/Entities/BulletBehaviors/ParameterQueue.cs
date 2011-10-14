using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DareToEscape.Entities.BulletBehaviors
{   
    class ParameterQueue : IBehavior
    {
        private readonly Queue<Parameters> paramQueue;
        private int frameCounter;
        private IBehavior behavior;

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

        public void AddTask(int modOnFrame, float? newSpeed, float? newAngle, float newTurnSpeed, float newAcceleration, float newSpeedLimit)
        {
            Parameters newParams = new Parameters(modOnFrame, newSpeed, newAngle, newTurnSpeed, newAcceleration, newSpeedLimit);
            paramQueue.Enqueue(newParams);
        }

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
    }

    public struct Parameters
    {
        public int ModOnFrame;
        public float? NewSpeed;
        public float? NewAngle;
        public float NewTurnSpeed;
        public float NewAcceleration;
        public float NewSpeedLimit;
        public IBehavior NewBehavior;

        public Parameters(int modOnFrame, float? newSpeed, float? newAngle, float newTurnSpeed, float newAcceleration, float newSpeedLimit)
        {
            ModOnFrame = modOnFrame;
            NewSpeed = newSpeed;
            NewAngle = newAngle;
            NewTurnSpeed = newTurnSpeed;
            NewAcceleration = newAcceleration;
            NewSpeedLimit = newSpeedLimit;
            NewBehavior = ReusableBehaviors.StandardBehavior;
        }

        public Parameters(int modOnFrame, float? newSpeed, float? newAngle, float newTurnSpeed, float newAcceleration, float newSpeedLimit, IBehavior newBehavior)
            : this(modOnFrame, newSpeed, newAngle, newTurnSpeed, newAcceleration, newSpeedLimit)
        {
            NewBehavior = newBehavior;
        }
    }
}
