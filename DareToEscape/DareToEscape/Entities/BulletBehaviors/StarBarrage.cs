using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DareToEscape.Entities.BulletBehaviors
{
    class StarBarrage : IBehavior
    {
        private int counter;
        private int modifier;

        public StarBarrage(int modifier)
        {
            this.modifier = modifier;
            counter = 0;
        }

        public void Update(Bullet bullet)
        {
            if (counter == modifier)
            {
                bullet.Acceleration = .1f;
                bullet.SpeedLimit = 3f;
                bullet.BaseSpeed = 1f;
                bullet.LaunchSpeed = 1f;
            }            
            ReusableBehaviors.StandardBehavior.Update(bullet);
            counter++;
        }
    }
}
