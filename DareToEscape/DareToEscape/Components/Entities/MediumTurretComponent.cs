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
    class MediumTurretComponent : TurretComponent
    {
        
        public MediumTurretComponent()
        {
            texture = VariableProvider.Game.Content.Load<Texture2D>(@"textures/entities/mediumturret");
            waveTimer = 2000;
            bulletTimer = 250;
            waveCount = 10;
        }

        public override void Update(GameObject obj)
        {
            foreach (Bullet bullet in bullets)
            {
                Vector2 direction = VariableProvider.CurrentPlayer.CollisionCenter - bullet.Position;
                direction.Normalize();
                bullet.Shoot(direction);
            }
            base.Update(obj);
        }
        

        protected override void ShootWave()
        {
            Bullet newBullet = new Bullet();
            newBullet.Position = bulletOrigin;
            Vector2 direction = VariableProvider.CurrentPlayer.CollisionCenter - newBullet.Position;
            direction.Normalize();
            newBullet.Shoot(direction);
            bullets.Add(newBullet);  
        }        
    }
}
