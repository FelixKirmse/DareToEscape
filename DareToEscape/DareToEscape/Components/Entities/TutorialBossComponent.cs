using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using BlackDragonEngine.Providers;
using Microsoft.Xna.Framework;
using DareToEscape.Entities;
using DareToEscape.Entities.BulletBehaviors;

namespace DareToEscape.Components.Entities
{
    class TutorialBossComponent : Boss1Component
    {
        private int waveCounter;
        
        public TutorialBossComponent()                 
        {
            texture = VariableProvider.Game.Content.Load<Texture2D>(@"textures/entities/boss1");
            waveCount = 8;
            waveTimer = 500;
            bulletTimer = 64;
        }              

        protected override IEnumerator<float> ShootBehavior()
        {
            float speed = 1 + waveCounter % 3;
            for (int x = 0; x < 360; x = x + 10)
            {
                Bullet bullet = new Bullet(new Boss1Behavior(), bulletOrigin);
                bullet.BaseSpeed = speed;
                float radianValue = MathHelper.ToRadians(x + waveCounter);
                bullet.Shoot(new Vector2((float)Math.Cos(radianValue), (float)Math.Sin(radianValue)));
            }
            waveCounter++;
            yield return 24f;           
        }
    }
}
