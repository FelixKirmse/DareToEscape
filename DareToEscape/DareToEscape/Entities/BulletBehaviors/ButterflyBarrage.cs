using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DareToEscape.Entities.BulletBehaviors
{
    class ButterflyBarrage : IBehavior
    {
        private int frameCounter;
        private int modifier;
        private float iterator;

        public ButterflyBarrage(int modifier, float iterator)
        {
            this.modifier = modifier;
            this.iterator = iterator;
        }

        public void Update(Bullet bullet)
        {
            switch (frameCounter++)
            { 
                case 60:
                    bullet.BaseSpeed = 2;
                    bullet.LaunchSpeed = 2;
                    bullet.TurnSpeed = .5f * modifier;
                    bullet.Acceleration = .1f;
                    bullet.SpeedLimit = 1 + iterator / 4;
                    break;

                case 120:
                    bullet.TurnSpeed = 0;
                    bullet.Acceleration = 0;
                    bullet.SpeedLimit = 0;
                    break;
            }
            ReusableBehaviors.StandardBehavior.Update(bullet);
        }
    }
}
