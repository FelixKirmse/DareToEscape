using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using BlackDragonEngine.Providers;
using DareToEscape.Entities;
using DareToEscape.Entities.BulletBehaviors;
using System;
using Microsoft.Xna.Framework;

namespace DareToEscape.Components.Entities
{
    class MediumTurretComponent : TurretComponent
    {
        
        public MediumTurretComponent()
        {
            texture = VariableProvider.Game.Content.Load<Texture2D>(@"textures/entities/mediumturret");            
        }

        protected override IEnumerator<int> ShootBehavior()
        {
            float frequency = .6f;
            for (int i = 0; i < 25; ++i)
            {
                int red = (int)(Math.Sin(frequency * i + 0) * 127 + 128);
                int green = (int)(Math.Sin(frequency * i + (2 * Math.PI / 3)) * 127 + 128);
                int blue = (int)(Math.Sin(frequency * i + (4 * Math.PI / 3)) * 127 + 128);
                Bullet bullet = new Bullet(ReusableBehaviors.StandardBehavior, bulletOrigin, new Color(red, green, blue));
                bullet.Shoot(VariableProvider.RandomSeed.Next(-1, 361), 4);                
            } 
            yield return 1;
        }        
    }
}
