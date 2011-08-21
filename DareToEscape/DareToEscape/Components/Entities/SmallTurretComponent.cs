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

namespace DareToEscape.Components.Entities
{
    class SmallTurretComponent : TurretComponent
    {
        public SmallTurretComponent()
        {
            texture = VariableProvider.Game.Content.Load<Texture2D>(@"textures/entities/smallturret");
        }

        protected override void ShootWave()
        {
            Bullet newBullet = new Bullet();
            newBullet.Position = bulletOrigin;
            Vector2 direction = VariableProvider.CurrentPlayer.Position - bulletOrigin;
            direction.Normalize();
            bullets.Add(newBullet);
            newBullet.Shoot(direction);
        }
    }
}
