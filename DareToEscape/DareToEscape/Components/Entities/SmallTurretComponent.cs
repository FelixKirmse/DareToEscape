﻿using System.Collections.Generic;
using BlackDragonEngine.Providers;
using DareToEscape.Bullets;
using Microsoft.Xna.Framework.Graphics;

namespace DareToEscape.Components.Entities
{
    internal class SmallTurretComponent : TurretComponent
    {
        public SmallTurretComponent()
        {
            texture = VariableProvider.Game.Content.Load<Texture2D>(@"textures/entities/smallturret");
        }

        protected override IEnumerator<int> ShootBehavior(params float[] parameters)
        {
            for (int i = 0; i < 5; ++i)
            {
                var newBullet = new Bullet(BulletOrigin, 1);
                newBullet.Shoot(newBullet.DirectionAngleToPlayer, 2f);
                yield return 10;
            }
            yield return 120;
        }
    }
}