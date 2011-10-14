using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DareToEscape.Entities.BulletBehaviors
{
    class ButterflyMayhem : IBehavior
    {
        private int modifier;
        private float speedMod;
        private int frameCounter;

        public ButterflyMayhem(int modifier, float speedMod)
        {
            this.modifier = modifier;
            this.speedMod = speedMod;
            frameCounter = 0;
        }

        public void Update(Bullet bullet)
        {
            switch (frameCounter++)
            { 
                case 60:
                    bullet.LaunchSpeed = 2f;
                    bullet.BaseSpeed = 2f;
                    bullet.TurnSpeed = 1 * modifier;
                    bullet.Acceleration = .2f;
                    bullet.SpeedLimit = 2 + (speedMod / 3);
                    break;

                case 120:
                    bullet.TurnSpeed = 0f;
                    bullet.Acceleration = 0f;
                    bullet.SpeedLimit = 0f;
                    break;
            }            
            ReusableBehaviors.StandardBehavior.Update(bullet);
        }
    }
}
