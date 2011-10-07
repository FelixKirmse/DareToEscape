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
using BlackDragonEngine.Managers;
using DareToEscape.Helpers;
using DareToEscape.Entities.BulletBehaviors;

using PlayerEnt = DareToEscape.Entities.Player;


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

        protected override IEnumerator<float> ShootBehavior()
        {
            int bulletNumber = 45;
            float winkel = 0;
            for (int i = 0; i < bulletNumber; ++i)
            {
                Bullet bullet = new Bullet(ReusableBehaviors.StandardBehavior, bulletOrigin, Color.Red);
                bullet.Shoot(winkel + bullet.DirectionAngleToPlayer);
                winkel += 360 / bulletNumber;               
            }            
            yield return 125f;
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
