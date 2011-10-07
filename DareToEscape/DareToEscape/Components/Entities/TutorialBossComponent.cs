using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using BlackDragonEngine.Providers;
using Microsoft.Xna.Framework;
using DareToEscape.Entities;
using DareToEscape.Entities.BulletBehaviors;
using BlackDragonEngine.Helpers;
using PlayerEnt = DareToEscape.Entities.Player;

namespace DareToEscape.Components.Entities
{
    class TutorialBossComponent : Boss1Component
    {
        private int waveCounter;
        
        public TutorialBossComponent()                 
        {
            texture = VariableProvider.Game.Content.Load<Texture2D>(@"textures/entities/boss1");
            waveCount = 8;
            waveTimer = 1000;
            bulletTimer = 64;
        }

        int alpha = 0;
        int beta = 90;
        int gamma = 180;
        int delta = 270;
        int modifier = 1;
        int deg = 66;
        // 66 + 200 rocken
        protected override IEnumerator<float> ShootBehavior()
        {
            for (int k = 0; k < 300; ++k)
            {
                Vector2 center = ShortcutProvider.ScreenCenter;
                for (int i = 0; i < 2; ++i)
                {
                    Color bulletColor = i == 0 ? Color.Red : new Color(0, 255, 0, 255);
                    float angle = i == 0 ? alpha += deg : gamma += deg;                    
                    Bullet bullet = new Bullet(ReusableBehaviors.StandardBehavior, new Vector2(center.X - 200, center.Y), bulletColor);
                    bullet.Shoot(angle);
                }

                for (int i = 0; i < 2; ++i)
                {
                    Color bulletColor = i == 0 ? Color.Blue : Color.Orange;
                    float angle = i == 0 ? beta -= deg : delta -= deg;                    
                    Bullet bullet = new Bullet(ReusableBehaviors.StandardBehavior, new Vector2(center.X + 200, center.Y), bulletColor);
                    bullet.Shoot(angle);
                }
                yield return 1f;
            }
            yield return 500f;

            for (int i = 0; i < 20; ++i)
            {
                for (int j = 0; j < 20; ++j)
                {
                    Bullet newBullet = new Bullet(ReusableBehaviors.StandardBehavior, bulletOrigin, Color.Red);                    
                    float direction = newBullet.DirectionAngleToPlayer + VariableProvider.RandomSeed.Next(-40, 40);
                    newBullet.Shoot(direction);
                    newBullet.BaseSpeed = VariableProvider.RandomSeed.Next(2, 4);
                }
                yield return 250f;   
            }
            yield return 1500f;
        }
    }
}
