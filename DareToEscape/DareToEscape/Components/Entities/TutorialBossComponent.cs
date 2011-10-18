using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using BlackDragonEngine.Providers;
using Microsoft.Xna.Framework;
using DareToEscape.Entities;
using DareToEscape.Entities.BulletBehaviors;

namespace DareToEscape.Components.Entities
{
    class TutorialBossComponent : Boss1Component
    {
       
        
        public TutorialBossComponent()                 
        {
            texture = VariableProvider.Game.Content.Load<Texture2D>(@"textures/entities/boss1");            
        }

        int alpha = 0;
        int beta = 90;
        int gamma = 180;
        int delta = 270;        
        int deg = 66;
        // 66 + 200 rocken
        protected override IEnumerator<int> ShootBehavior(params float[] parameters)
        {
            for (int k = 0; k < 300; ++k)
            {
                Vector2 center = ShortcutProvider.ScreenCenter;
                for (int i = 0; i < 2; ++i)
                {
                    Color bulletColor = i == 0 ? Color.Red : new Color(0, 255, 0, 255);
                    float angle = i == 0 ? alpha += deg : gamma += deg;                    
                    Bullet bullet = new Bullet(ReusableBehaviors.StandardBehavior, new Vector2(center.X - 200, center.Y), 1);
                    bullet.Shoot(angle, 2f);
                }

                for (int i = 0; i < 2; ++i)
                {
                    Color bulletColor = i == 0 ? Color.Blue : Color.Orange;
                    float angle = i == 0 ? beta -= deg : delta -= deg;                    
                    Bullet bullet = new Bullet(ReusableBehaviors.StandardBehavior, new Vector2(center.X + 200, center.Y),1);
                    bullet.Shoot(angle, 2f);
                }
                yield return 1;
            }
            yield return 30;

            for (int i = 0; i < 20; ++i)
            {
                for (int j = 0; j < 20; ++j)
                {
                    Bullet newBullet = new Bullet(ReusableBehaviors.StandardBehavior, bulletOrigin,1);                    
                    float direction = newBullet.DirectionAngleToPlayer + VariableProvider.RandomSeed.Next(-40, 40);
                    newBullet.Shoot(direction, VariableProvider.RandomSeed.Next(2, 4));                    
                }
                yield return 15;   
            }
            yield return 90;
        }
    }
}
