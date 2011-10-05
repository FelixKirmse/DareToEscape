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
            Vector2 center = ShortcutProvider.ScreenCenter;
            for (int i = 0; i < 2; ++i)
            {
                Color bulletColor = i == 0 ? Color.Red : i == 1 ? new Color(0, 255, 0, 255) : i == 2 ? Color.Blue : Color.Orange;                
                float angle = i == 0 ? alpha += deg : gamma += deg;
                angle = MathHelper.ToRadians(angle);
                Bullet bullet = new Bullet(new Boss1Behavior(),new Vector2(center.X - 200, center.Y) , bulletColor);                
                bullet.Shoot(new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)));                
            }

            for (int i = 0; i < 2; ++i)
            {
                Color bulletColor = i == 0 ? Color.Blue : Color.Orange;
                float angle = i == 0 ? beta -= deg :delta -= deg;
                angle = MathHelper.ToRadians(angle);
                Bullet bullet = new Bullet(new Boss1Behavior(), new Vector2(center.X + 200, center.Y), bulletColor);
                bullet.Shoot(new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)));
            }
            yield return 0f;
        }
    }
}
