using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackDragonEngine.Entities;
using Microsoft.Xna.Framework;
using BlackDragonEngine.Components;
using Microsoft.Xna.Framework.Graphics;
using BlackDragonEngine.Providers;
using DareToEscape.Entities;
using DareToEscape.Entities.BulletBehaviors;

namespace DareToEscape.Components.Entities
{
    class MediumTurretComponent : TurretComponent
    {
        
        public MediumTurretComponent()
        {
            texture = VariableProvider.Game.Content.Load<Texture2D>(@"textures/entities/mediumturret");            
        }

        protected override IEnumerator<float> ShootBehavior()
        {
            for (int i = 0; i < 10; ++i)
            {
                Bullet newBullet = new Bullet(ReusableBehaviors.TracingBehavior, bulletOrigin);                
                newBullet.Shoot(newBullet.DirectionAngleToPlayer);
                yield return 250f;
            }
            yield return 2000f; 
        }        
    }
}
