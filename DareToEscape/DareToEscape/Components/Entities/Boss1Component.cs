using System;
using System.Collections.Generic;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Managers;
using BlackDragonEngine.Providers;
using BlackDragonEngine.Scripting;
using DareToEscape.Entities;
using DareToEscape.Entities.BulletBehaviors;
using DareToEscape.Helpers;
using DareToEscape.Providers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DareToEscape.Components.Entities
{
    internal class Boss1Component : TurretComponent
    {
        private bool active = true;

        private float angle;
        private float angle2;
        private float angle3;
        private float angle4 = 90;
        private int frame;
        private int frame2;
        private int frame3;
        private int phase;
        private bool shoot;
        private double timeTracker;

        public Boss1Component()
        {
            texture = VariableProvider.Game.Content.Load<Texture2D>(@"textures/entities/boss1");
            phase = 6;
            phaseTimer = 90595;
        }

        private int phaseTimer
        {
            get { return (int) timeTracker; }
            set { timeTracker = value; }
        }

        public override void Update(GameObject obj)
        {
            if (shoot)
            {
                timeTracker -= VariableProvider.GameTime.ElapsedGameTime.TotalSeconds;
                if (timeTracker <= 0)
                {
                    ++phase;
                    GameVariableProvider.BulletManager.ClearAllBullets();
                    VariableProvider.ScriptEngine.StopAllScripts();
                    SwitchPhase();
                }
                switch (phase)
                {
                    case 2:
                        if (!VariableProvider.ScriptEngine.IsScriptRunning(OngoingBarrage))
                            VariableProvider.ScriptEngine.ExecuteScript(OngoingBarrage);
                        break;

                    case 3:
                        if (!VariableProvider.ScriptEngine.IsScriptRunning(CurtainBarrage))
                            VariableProvider.ScriptEngine.ExecuteScript(CurtainBarrage);
                        break;

                    case 4:
                        if (frame <= 80)
                            ++angle;
                        if (frame >= 80)
                            --angle;
                        if (frame == 240)
                            frame = 0;
                        ++frame;
                        ++frame2;
                        ++frame3;
                        break;
                }
            }
            if (active || !SaveManager<SaveState>.CurrentSaveState.BossDead)
            {
                base.Update(obj);
            }
        }

        public override void Draw(GameObject obj, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(FontProvider.GetFont("Mono14"), phaseTimer.ToString(), new Vector2(1000, 100),
                                   Color.White);
            if (active || !SaveManager<SaveState>.CurrentSaveState.BossDead)
                base.Draw(obj, spriteBatch);
        }

        protected void StartScript(Script script, params float[] parameters)
        {
            VariableProvider.ScriptEngine.ExecuteScript(script, parameters);
        }

        protected override IEnumerator<int> ShootBehavior(params float[] parameters)
        {
            switch (phase)
            {
                case 1:
                    yield return 20;
                    ButterflyCircleShot(1);
                    yield return 120;
                    Shoot3Circles();
                    yield return 120;
                    yield return 20;
                    ButterflyCircleShot(-1);
                    yield return 120;
                    Shoot3Circles();
                    yield return 120;
                    break;

                case 2:
                    yield return 120;
                    PlayerPrison();
                    yield return 220;
                    StartScript(AntiSafeSpotBarrage, 0);
                    StartScript(AntiSafeSpotBarrage, 5);
                    StartScript(AntiSafeSpotBarrage, -5);
                    StartScript(AntiSafeSpotBarrage, -10);
                    StartScript(AntiSafeSpotBarrage, 10);
                    StartScript(AntiSafeSpotBarrage, -15);
                    StartScript(AntiSafeSpotBarrage, 15);
                    StartScript(AntiSafeSpotBarrage, -20);
                    StartScript(AntiSafeSpotBarrage, 20);
                    yield return 400;
                    break;

                case 3:
                    for (int i = 0; i < 10; ++i)
                    {
                        for (int j = 0; j < 5; ++j)
                        {
                            var bullet = new Bullet(new StarBarrage(j*4), bulletOrigin, 51);
                            bullet.SpawnDelay = 5 + j;
                            bullet.Shoot(angle2 + j*4, 0);
                        }

                        for (int j = 0; j < 5; ++j)
                        {
                            var bullet = new Bullet(new StarBarrage(j*4), bulletOrigin, 51);
                            bullet.SpawnDelay = 5 + j;
                            bullet.Shoot(angle2 - j*4, 0);
                        }
                        angle2 += 36f; //360f / 10f;
                    }
                    yield return 30;
                    break;

                case 4:
                    if (frame2 == 120)
                    {
                        for (int i = 0; i < 8; ++i)
                        {
                            var bullet = new Bullet(bulletOrigin, 51);
                            bullet.Shoot(angle2, 3);
                            angle2 += 360f/12;
                            bullet = new Bullet(bulletOrigin, 172);
                            bullet.Shoot(angle, 1);
                            angle += 360f/6;
                            float radian = MathHelper.ToRadians(angle3);
                            bullet =
                                new Bullet(
                                    new Vector2(bulletOrigin.X + 100f*(float) Math.Cos(radian),
                                                bulletOrigin.Y + 100f*(float) Math.Sin(radian)), 172);
                            bullet.TurnSpeed = .57f;
                            bullet.KillTime = 120;
                            bullet.Shoot(angle3 + 90, 1);
                            angle3 += 45f; //360f / 8;
                            angle2 += 4f;
                        }
                        frame2 = 115;
                    }
                    if (frame3 == 200)
                    {
                        for (int i = 0; i < 15; ++i)
                        {
                            var bullet = new Bullet(bulletOrigin, 243);
                            bullet.Shoot(angle4, 2.5f);
                            angle4 += 24f; //360f / 15;
                        }
                        angle4 -= 17f;
                        frame3 = 50;
                    }
                    break;

                case 5:
                    yield return 20;
                    ButterflyBarrage2(1);
                    ButterflyBarrage2(-1);
                    yield return 120;
                    ButterflyBarrage();
                    yield return 120;
                    break;

                case 6:
                    StartScript(BulletFlower, 1);
                    yield return 220;
                    StartScript(Pentagram, -1);
                    yield return 400;
                    StartScript(BulletFlower, -1);
                    yield return 220;
                    StartScript(Pentagram, 1);
                    yield return 400;
                    break;

                case 7:
                    yield return 10;
                    StartScript(CircleBarrage, 1);
                    StartScript(CircleBarrage, 2);
                    yield return 50;
                    StartScript(LineBarrage);
                    yield return 70;
                    break;

                case 8:
                    break;
            }
        }

        private IEnumerator<int> CircleBarrage(params float[] p)
        {
            float mod = p[0];
            for (int j = 0; j < 5; ++j)
            {
                for (int i = 0; i < 50; ++i)
                {
                    var bullet = new Bullet(GetVectorAroundPoint(bulletOrigin, 20, angle), 83);
                    bullet.Shoot(angle, 3);
                    bullet.SetParameters(null, null, .05f*mod, 0, 0);
                    angle += 7.2f; //360f / 50;
                }
                angle += 4f;
                yield return 10;
            }
        }

        private IEnumerator<int> LineBarrage(params float[] p)
        {
            for (int i = 0; i < 15; ++i)
            {
                var bullet = new Bullet(bulletOrigin, 216);
                bullet.Shoot(bullet.DirectionAngleToPlayer, 5);
                bullet = new Bullet(bulletOrigin, 216);
                bullet.Shoot(bullet.DirectionAngleToPlayer + 20, 5);
                bullet = new Bullet(bulletOrigin, 216);
                bullet.Shoot(bullet.DirectionAngleToPlayer - 20, 5);
                yield return 5;
            }
        }

        private IEnumerator<int> BulletFlower(params float[] parameters)
        {
            float mod = parameters[0];
            Random rand = VariableProvider.RandomSeed;
            for (int j = 0; j < 10; ++j)
            {
                for (int i = 0; i < 80; ++i)
                {
                    var pq = new ParameterQueue();
                    var bullet = new Bullet(pq, bulletOrigin, 51);
                    bullet.Shoot(angle2, 8);
                    bullet.AutomaticCollision = false;
                    bullet.SetParameters(null, null, 0, -.1f, 1);
                    pq.AddTask(60, 1, angle2, 0, -.2f, -1);
                    pq.AddTask(120, -1, angle2, -2*mod, .2f, 2);
                    pq.AddTask(180, -1, angle2, rand.NextFloat(-.1f, .1f), -.1f, rand.NextFloat(-3, -1));
                    angle2 += 6f; //360f / 60;
                }
                yield return 6;
            }
        }

        private IEnumerator<int> Pentagram(params float[] parameters)
        {
            float mod = parameters[0];
            Random rand = VariableProvider.RandomSeed;
            for (int j = 0; j < 50; ++j)
            {
                for (int i = 0; i < 15; ++i)
                {
                    float radian = MathHelper.ToRadians(angle);
                    var pq = new ParameterQueue();
                    var bullet = new Bullet(pq,
                                            new Vector2(bulletOrigin.X - 120f*(float) Math.Cos(radian),
                                                        bulletOrigin.Y - 120f*(float) Math.Sin(radian)), 172);
                    bullet.Shoot(angle, 5);
                    bullet.SetParameters(null, null, mod, -.1f, 1);
                    pq.AddTask(150, rand.NextFloat(1f, 1.5f), angle, rand.NextFloat(-.2f, .2f), 0, 0);
                    angle += 72f; //360f / 5;
                }

                for (int i = 0; i < 18; ++i)
                {
                    float radian = MathHelper.ToRadians(angle3);
                    var pq = new ParameterQueue();
                    var bullet = new Bullet(pq,
                                            new Vector2(bulletOrigin.X + 120f*(float) Math.Cos(radian),
                                                        bulletOrigin.Y + 120f*(float) Math.Sin(radian)), 176);
                    bullet.Shoot(angle3 + 90, -1*mod);
                    bullet.SetParameters(null, null, -1*mod, 0, 0);
                    pq.AddTask(120, mod, angle3 + 90, 0, 0, 0);
                    angle3 += 20f; //360 / 18;
                }
                angle3 += 5f*mod;
                angle += 5f*mod;
                yield return 10;
            }
        }

        private void ButterflyBarrage()
        {
            for (float i = 1; i < 10; ++i)
            {
                for (int j = 0; j < 40; ++j)
                {
                    var bullet = new Bullet(bulletOrigin, 83);
                    bullet.Shoot(angle, 1 + i/3);
                    angle += 9f; //360f / 40;
                }
            }
        }

        private void ButterflyBarrage2(int modifier)
        {
            for (int i = 0; i < 20; ++i)
            {
                for (int j = 0; j < 30; ++j)
                {
                    var bullet = new Bullet(new ButterflyBarrage(modifier, i), bulletOrigin, 228);
                    bullet.TurnSpeed = -3*modifier;
                    bullet.Acceleration = -.2f;
                    bullet.SpeedLimit = 1;
                    bullet.Shoot(angle*modifier, 5);
                    angle += 12f; //360f / 30;
                }
            }
        }

        private IEnumerator<int> CurtainBarrage(params float[] parameters)
        {
            for (int i = 0; i < 60; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    var radian = MathHelper.ToRadians(angle);
                    var bullet =
                        new Bullet(
                            new Vector2(bulletOrigin.X + 60f*(float) Math.Cos(radian),
                                        bulletOrigin.Y + 100f*(float) Math.Sin(radian)), 20)
                            {AutomaticCollision = false};
                    bullet.Shoot(bullet.DirectionAngleToPlayer, 3.5f);
                    bullet =
                        new Bullet(
                            new Vector2(bulletOrigin.X + 60f*(float) Math.Cos(radian),
                                        bulletOrigin.Y + 100f*(float) Math.Sin(radian)), 20)
                            {AutomaticCollision = false};
                    bullet.Shoot(bullet.DirectionAngleToPlayer + 90f, -3.5f);
                    bullet =
                        new Bullet(
                            new Vector2(bulletOrigin.X + 60f*(float) Math.Cos(radian),
                                        bulletOrigin.Y + 100f*(float) Math.Sin(radian)), 20)
                            {AutomaticCollision = false};
                    bullet.Shoot(bullet.DirectionAngleToPlayer + 90f, 3.5f);
                    bullet =
                        new Bullet(
                            new Vector2(bulletOrigin.X + 60f*(float) Math.Cos(radian),
                                        bulletOrigin.Y + 100f*(float) Math.Sin(radian)), 20)
                            {AutomaticCollision = false};
                    bullet.Shoot(bullet.DirectionAngleToPlayer, -3.5f);
                    angle += 120f; //360f / 3f;
                    yield return 1;
                }
                angle += 12.5f;
            }
            yield return 300;
        }

        private IEnumerator<int> OngoingBarrage(params float[] parameters)
        {
            for (int i = 0; i < 20; ++i)
            {
                var bullet = new Bullet(bulletOrigin, 31);
                bullet.Shoot(angle, 2.8f);
                angle += 18f; //360 / 20;
            }
            angle += 4f;
            yield return 20;
        }

        private IEnumerator<int> AntiSafeSpotBarrage(params float[] parameters)
        {
            float modifier = parameters[0];
            for (int i = 0; i < 10; ++i)
            {
                var bullet = new Bullet(bulletOrigin, 69);
                bullet.Acceleration = -.1f;
                bullet.SpeedLimit = 3f;
                bullet.Shoot(bullet.DirectionAngleToPlayer + modifier, 6f);
                yield return 10;
            }
        }

        private void PlayerPrison()
        {
            float angle = 0;
            for (int i = 0; i < 120; ++i)
            {
                float radian = MathHelper.ToRadians(angle);
                var bullet = new Bullet(new PlayerTrap1(angle),
                                        new Vector2(Player.PlayerPosX + 150f*(float) Math.Cos(radian),
                                                    Player.PlayerPosY + 150f*(float) Math.Sin(radian)), 50);
                bullet.Acceleration = .1f;
                bullet.SpeedLimit = 0;
                bullet.Shoot(angle, -3);
                bullet.AutomaticCollision = false;
                var bullet2 = new Bullet(new PlayerTrap2(angle),
                                         new Vector2(Player.PlayerPosX + 90f*(float) Math.Cos(radian),
                                                     Player.PlayerPosY + 90f*(float) Math.Sin(radian)), 50);
                bullet2.Acceleration = .05f;
                bullet2.SpeedLimit = 0;
                bullet2.Shoot(angle, -2);
                bullet2.AutomaticCollision = false;
                angle += 3f; //360 / 120;
            }
        }

        private void ButterflyCircleShot(int modifier)
        {
            float angle = 0;
            for (float i = 0; i < 10; ++i)
            {
                for (int j = 0; j < 60; ++j)
                {
                    var bullet = new Bullet(new ButterflyMayhem(modifier, i), bulletOrigin, 228);
                    bullet.TurnSpeed = -3f*modifier;
                    bullet.Acceleration = -.5f;
                    bullet.SpeedLimit = 2f;
                    bullet.Shoot(angle*modifier, 5f);
                    angle += 6f; //360 / 60;
                }
            }
        }

        private void Shoot3Circles()
        {
            float angle = 0;
            for (int i = 1; i < 4; ++i)
            {
                for (int j = 0; j < 40; ++j)
                {
                    var bullet = new Bullet(bulletOrigin, 216);
                    bullet.Shoot(angle, 1 + i);
                    angle += 9f; //360 / 40;
                }
            }
        }

        protected Vector2 GetVectorAroundPoint(Vector2 center, float distance, float angle)
        {
            float radian = MathHelper.ToRadians(angle);
            return new Vector2(center.X + distance*(float) Math.Cos(radian),
                               center.Y + distance*(float) Math.Sin(radian));
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
            base.Receive(message, obj);
        }

        protected virtual void SwitchPhase()
        {
            angle = 0;
            angle2 = 0;
            angle3 = 0;
            angle4 = 0;
            switch (phase)
            {
                case 1:
                case 3:
                    phaseTimer = 20;
                    break;

                case 5:
                case 7:
                    phaseTimer = 15;
                    break;
                default:
                    phaseTimer = 30;
                    break;
            }
        }
    }
}