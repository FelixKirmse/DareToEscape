using System.Collections.Generic;
using BlackDragonEngine.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BlackDragonEngine.Providers;
using DareToEscape.Entities;
using BlackDragonEngine.Managers;
using DareToEscape.Helpers;
using DareToEscape.Entities.BulletBehaviors;
using System;



namespace DareToEscape.Components.Entities
{
    class Boss1Component : TurretComponent
    {
        private bool shoot = false;
        private bool active = true;
        protected float waveTimer;
        protected float bulletTimer;
        protected int waveCount;

        public Boss1Component()
        {
            texture = VariableProvider.Game.Content.Load<Texture2D>(@"textures/entities/boss1");
            waveCount = 5;
            waveTimer = 1500;
            bulletTimer = 125;
        }

        public override void Update(GameObject obj)
        {
            if (active || !SaveManager<SaveState>.CurrentSaveState.BossDead)
                base.Update(obj);
        }

        public override void Draw(GameObject obj, SpriteBatch spriteBatch)
        {
            if (active || !SaveManager<SaveState>.CurrentSaveState.BossDead)
                base.Draw(obj, spriteBatch);
        }


        private int angle = 0;
        private int angle2 = 1;
        protected override IEnumerator<float> ShootBehavior()
        {
            float frequency = .6f;
            
                for (int i = 0; i < 20; ++i)
                {
                    int red = (int)(Math.Sin(frequency * i + 0) * 127 + 128);
                    int green = (int)(Math.Sin(frequency * i + (2 * Math.PI / 3)) * 127 + 128);
                    int blue = (int)(Math.Sin(frequency * i + (4 * Math.PI / 3)) * 127 + 128);
                    Bullet bullet = new Bullet(ReusableBehaviors.StandardBehavior, bulletOrigin, new Color(red, green, blue, 255));
                    bullet.BaseSpeed = 2.8f;
                    bullet.Shoot(angle);
                    angle += 360 / 20;
                }
                angle += 4;
                for (int i = 0; i < 20; ++i)
                {
                    int red = (int)(Math.Sin(frequency * i + 0) * 127 + 128);
                    int green = (int)(Math.Sin(frequency * i + (2 * Math.PI / 3)) * 127 + 128);
                    int blue = (int)(Math.Sin(frequency * i + (4 * Math.PI / 3)) * 127 + 128);
                    Bullet bullet = new Bullet(ReusableBehaviors.StandardBehavior, bulletOrigin, new Color(red, green, blue, 255));
                    bullet.BaseSpeed = 2.8f;
                    bullet.Shoot(angle2);
                    angle2 -= 360 / 20;
                }
                angle2 -= 4;
                yield return 0;
                        
            //yield return 2000f;
        }

        

        protected override bool ShootCondition(Vector2 playerPosition, GameObject turret)
        {
            return shoot;
        }

        public override void Receive<T>(string message, T obj)
        {
            if (message == "SHOOT")
                shoot = true;
            if (message == "INACTIVE")
            {
                SaveManager<SaveState>.CurrentSaveState.BossDead = true;
                SaveManager<SaveState>.CurrentSaveState.Keys.Add("BOSS");
                active = false;
            }
            base.Receive<T>(message, obj);
        }
    }
}
