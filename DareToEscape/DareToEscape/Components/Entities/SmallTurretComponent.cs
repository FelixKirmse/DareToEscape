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
using DareToEscape.Providers;

namespace DareToEscape.Components.Entities
{
    class SmallTurretComponent : TurretComponent
    {
        public SmallTurretComponent()
        {
            texture = VariableProvider.Game.Content.Load<Texture2D>(@"textures/entities/smallturret");
        }

        protected override IEnumerator<float> ShootBehavior()
        {            
            for (int i = 0; i < 5; ++i)
            {
                Bullet newBullet = new Bullet(bulletOrigin);
                Vector2 direction = VariableProvider.CurrentPlayer.Position - bulletOrigin;
                direction.Normalize();
                newBullet.Shoot(direction);
                yield return 166f;               
            }
            yield return 2000f;
        }
    }
}
