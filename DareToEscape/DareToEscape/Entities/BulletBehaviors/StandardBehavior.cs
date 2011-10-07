using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DareToEscape.Entities.BulletBehaviors
{
    class StandardBehavior : Behavior
    {
        public void Update(Bullet bullet)
        {
            bullet.Position += bullet.DirectionVector * bullet.BaseSpeed;
        }
    }
}
