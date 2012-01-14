using System;
using System.Collections.Generic;
using BlackDragonEngine.Providers;
using DareToEscape.Bullets;
using DareToEscape.Bullets.Behaviors;
using Microsoft.Xna.Framework.Graphics;

namespace DareToEscape.Components.Entities
{
    internal class MediumTurretComponent : TurretComponent
    {
        public MediumTurretComponent()
        {
            Texture = VariableProvider.Game.Content.Load<Texture2D>(@"textures/entities/mediumturret");
        }

        protected override IEnumerator<int> ShootBehavior(params float[] parameters)
        {
            float frequency = .6f;
            for (int i = 0; i < 25; ++i)
            {
                var red = (int) (Math.Sin(frequency*i + 0)*127 + 128);
                var green = (int) (Math.Sin(frequency*i + (2*Math.PI/3))*127 + 128);
                var blue = (int) (Math.Sin(frequency*i + (4*Math.PI/3))*127 + 128);
                var bullet = new Bullet(ReusableBehaviors.StandardBehavior, BulletOrigin, 1);
                bullet.Shoot(VariableProvider.RandomSeed.Next(-1, 361), 4);
            }
            yield return 1;
        }
    }
}