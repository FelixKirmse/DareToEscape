using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DareToEscape.Entities.BulletBehaviors
{
    public interface Behavior
    {
        void Update(Bullet bullet);
    }
}
