using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using BlackDragonEngine.Providers;
using DareToEscape.Entities;

namespace DareToEscape.Entities.BulletBehaviors
{
    class TracingBehavior : Behavior
    {
        public void Update(Bullet bullet)
        {                        
            bullet.Position += bullet.DirectionVectorToPlayer * bullet.BaseSpeed;
        }
    }
}
