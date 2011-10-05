using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackDragonEngine.Providers;
using Microsoft.Xna.Framework;

namespace DareToEscape.Entities.BulletBehaviors
{
    class Boss1Behavior : Behavior
    {
        private float timer;

        public void Update(Bullet bullet)
        {
            timer += ShortcutProvider.ElapsedMilliseconds;

            if (timer < 500f)
            {
                bullet.Position += bullet.Direction * bullet.BaseSpeed * 2;
            }            
            else if (timer < 1500f)
            {
                if (bullet.ChangedPosition)
                {
                    Vector2 direction = Player.GetNormalizedDirectionVector(bullet.Position);
                    bullet.Direction = direction;
                }
            }
            else
            {
                ReusableBehaviors.StandardBehavior.Update(bullet);
            }
        }
    }
}
