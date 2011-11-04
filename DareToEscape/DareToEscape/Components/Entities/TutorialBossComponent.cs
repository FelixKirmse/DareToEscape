using System.Collections.Generic;
using BlackDragonEngine.Providers;
using DareToEscape.Entities;
using DareToEscape.Entities.BulletBehaviors;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DareToEscape.Components.Entities
{
    internal class TutorialBossComponent : Boss1Component
    {
        private int alpha;
        private int beta = 90;
        private int deg = 66;
        private int delta = 270;
        private int gamma = 180;

        public TutorialBossComponent()
        {
            texture = VariableProvider.Game.Content.Load<Texture2D>(@"textures/entities/boss1");
        }

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
                    var bullet = new Bullet(ReusableBehaviors.StandardBehavior, new Vector2(center.X - 200, center.Y), 1);
                    bullet.Shoot(angle, 2f);
                }

                for (int i = 0; i < 2; ++i)
                {
                    Color bulletColor = i == 0 ? Color.Blue : Color.Orange;
                    float angle = i == 0 ? beta -= deg : delta -= deg;
                    var bullet = new Bullet(ReusableBehaviors.StandardBehavior, new Vector2(center.X + 200, center.Y), 1);
                    bullet.Shoot(angle, 2f);
                }
                yield return 1;
            }
            yield return 30;

            for (int i = 0; i < 20; ++i)
            {
                for (int j = 0; j < 20; ++j)
                {
                    var newBullet = new Bullet(ReusableBehaviors.StandardBehavior, bulletOrigin, 1);
                    float direction = newBullet.DirectionAngleToPlayer + VariableProvider.RandomSeed.Next(-40, 40);
                    newBullet.Shoot(direction, VariableProvider.RandomSeed.Next(2, 4));
                }
                yield return 15;
            }
            yield return 90;
        }
    }
}