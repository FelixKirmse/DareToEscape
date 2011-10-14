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
using DareToEscape.Providers;
using BlackDragonEngine.Helpers;

namespace DareToEscape.Components.Entities
{
    class Boss1Component : TurretComponent
    {
        private bool shoot = false;
        private bool active = true;
       
        private int phaseTimer { get { return (int)timeTracker; } set {timeTracker = value; } }
        private int phase;
        private double timeTracker;        

        public Boss1Component()
        {
            texture = VariableProvider.Game.Content.Load<Texture2D>(@"textures/entities/boss1");
            phase = 7;
            SwitchPhase();
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
                            VariableProvider.ScriptEngine.ExecuteSript(OngoingBarrage);
                        break;

                    case 3:
                        if (!VariableProvider.ScriptEngine.IsScriptRunning(CurtainBarrage))
                            VariableProvider.ScriptEngine.ExecuteSript(CurtainBarrage);
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
            spriteBatch.DrawString(FontProvider.GetFont("Mono14"), phaseTimer.ToString(), new Vector2(1000, 100), Color.White);
            if (active || !SaveManager<SaveState>.CurrentSaveState.BossDead)
                base.Draw(obj, spriteBatch);
        }

        float angle = 0;
        float angle2 = 0;
        float angle3 = 0;
        float angle4 = 90;
        int frame = 0;
        int frame2 = 0;
        int frame3 = 0;
        /*protected override IEnumerator<int> ShootBehavior()
        {
            float frequency = .6f;
            
                for (int i = 0; i < 20; ++i)
                {
                    int red = (int)(Math.Sin(frequency * i + 0) * 127 + 128);
                    int green = (int)(Math.Sin(frequency * i + (2 * Math.PI / 3)) * 127 + 128);
                    int blue = (int)(Math.Sin(frequency * i + (4 * Math.PI / 3)) * 127 + 128);
                    Bullet bullet = new Bullet(ReusableBehaviors.StandardBehavior, bulletOrigin, new Color(red, green, blue, 255));                    
                    bullet.Shoot(angle, 2.8f);
                    angle += 360 / 20;
                }
                angle += 4;
                for (int i = 0; i < 20; ++i)
                {
                    int red = (int)(Math.Sin(frequency * i + 0) * 127 + 128);
                    int green = (int)(Math.Sin(frequency * i + (2 * Math.PI / 3)) * 127 + 128);
                    int blue = (int)(Math.Sin(frequency * i + (4 * Math.PI / 3)) * 127 + 128);
                    Bullet bullet = new Bullet(ReusableBehaviors.StandardBehavior, bulletOrigin, new Color(red, green, blue, 255));                    
                    bullet.Shoot(angle2, 2.8f);
                    angle2 -= 360 / 20;
                }
                angle2 -= 4;
                yield return 0;
                        
            //yield return 2000f;
        }*/

        protected override IEnumerator<int> ShootBehavior()
        {
            bulletOrigin = ShortcutProvider.ScreenCenter;
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
                    for(int i = 0; i < 10; ++i)
                    {
                        AntiSafeSpotBarrage(0);                    
	                    AntiSafeSpotBarrage(5);
	                    AntiSafeSpotBarrage(-5);
	                    AntiSafeSpotBarrage(-10);
	                    AntiSafeSpotBarrage(10);
	                    AntiSafeSpotBarrage(-15);
	                    AntiSafeSpotBarrage(15);
	                    AntiSafeSpotBarrage(-20);
	                    AntiSafeSpotBarrage(20);
                        yield return 10;
                    }
                    yield return 400;
                    break;

                case 3:
                    for (int i = 0; i < 10; ++i)
                    {
                        for (int j = 0; j < 5; ++j)
                        {
                            Bullet bullet = new Bullet(new StarBarrage(j * 4), bulletOrigin);
                            bullet.SpawnDelay = 5 + j;                            
                            bullet.Shoot(angle2 + j * 4, 0);
                        }

                        for (int j = 0; j < 5; ++j)
                        {
                            Bullet bullet = new Bullet(new StarBarrage(j * 4), bulletOrigin);
                            bullet.SpawnDelay = 5 + j;                            
                            bullet.Shoot(angle2 - j * 4, 0);
                        }
                        angle2 += 360f / 10f;
                    }
                    yield return 30;
                    break;

                case 4:
                    if (frame2 == 120)
                    {
                        for (int i = 0; i < 8; ++i)
                        {
                            Bullet bullet = new Bullet(bulletOrigin);
                            bullet.AutomaticCollision = false;                         
                            bullet.Shoot(angle2, 3);
                            angle2 += 360f / 12;
                            bullet = new Bullet(bulletOrigin);
                            bullet.AutomaticCollision = false; 
                            bullet.Shoot(angle, 1);
                            angle += 360f / 6;
                            float radian = MathHelper.ToRadians(angle3);
                            bullet = new Bullet(new Vector2(bulletOrigin.X + 100f * (float)Math.Cos(radian), bulletOrigin.Y + 100f * (float)Math.Sin(radian)));
                            bullet.TurnSpeed = .57f;
                            bullet.AutomaticCollision = false;
                            bullet.KillTime = 120;
                            bullet.Shoot(angle3 + 90, 1);                         
                            angle3 += 360f / 8;
                            angle2 += 4;
                        }
                        frame2 = 115;
                    }
                    if (frame3 == 200)
                    {
                        for (int i = 0; i < 15; ++i)
                        {
                            Bullet bullet = new Bullet(bulletOrigin);
                            bullet.Shoot(angle4, 2.5f);
                            angle4 += 360f / 15;
                        }
                        angle4 -= 17;
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
                    for (int i = 0; i < 10; i++)
                    {
                        BulletFlower(1);
                        yield return 6;
                    }
                    yield return 220;
                    for (int i = 0; i < 30; ++i)
                    {
                        Pentagram(-1);
                        yield return 10;
                    }
                    yield return 400;
                    for (int i = 0; i < 10; i++)
                    {
                        BulletFlower(-1);
                        yield return 6;
                    }
                    yield return 220;
                    for (int i = 0; i < 30; ++i)
                    {
                        Pentagram(1);
                        yield return 10;
                    }
                    yield return 400;
                    break;

                case 7:
                    yield return 10;
                    for (int i = 0; i < 5; ++i)
                    {
                        CircleBarrage(1);                        
                        yield return 10;
                    }
                    for (int i = 0; i < 5; ++i)
                    {
                        CircleBarrage(2);
                        yield return 10;
                    }  
                    yield return 50;
                    for (int i = 0; i < 15; ++i)
                    {
                        LineBarrage();
                        yield return 5;
                    }                    
                    yield return 70;
                    break;
            }                        
        }

        private void CircleBarrage(int mod)
        {
            for (int i = 0; i < 50; ++i)
            {
                Bullet bullet = new Bullet(GetVectorAroundPoint(bulletOrigin, 20, angle));
                bullet.Shoot(angle, 3);
                bullet.SetParameters(null, null, .05f * mod, 0, 0);
                angle += 360f / 50;
            }
            angle += 4;
        }

        private void LineBarrage()
        {
            Bullet bullet = new Bullet(bulletOrigin);
            bullet.Shoot(bullet.DirectionAngleToPlayer,5 );
            bullet = new Bullet(bulletOrigin);
            bullet.Shoot(bullet.DirectionAngleToPlayer + 20, 5);
            bullet = new Bullet(bulletOrigin);
            bullet.Shoot(bullet.DirectionAngleToPlayer - 20, 5);
        }

        private void BulletFlower(int mod)   
        {
            Random rand = VariableProvider.RandomSeed;
            for (int i = 0; i < 80; ++i)
            {
                ParameterQueue pq = new ParameterQueue();
                Bullet bullet = new Bullet(pq, bulletOrigin);                
                bullet.Shoot(angle2, 5);
                bullet.SetParameters(null, null, 0, -.1f, 1);
                pq.AddTask(60, 1, angle2, 0, -.2f, -1);
                pq.AddTask(120, -1, angle2, -2 * mod, .2f, 2);
                pq.AddTask(180, -1, angle2, rand.NextFloat(-.1f, .1f), -.1f, rand.NextFloat(-3, -1));
                angle2 += 360f / 60;
            }
        }

        private void Pentagram(int mod)
        {
            Random rand = VariableProvider.RandomSeed;
            for (int i = 0; i < 15; ++i)
            {
                float radian = MathHelper.ToRadians(angle);
                ParameterQueue pq = new ParameterQueue();
                Bullet bullet = new Bullet(pq, new Vector2(bulletOrigin.X - 120f * (float)Math.Cos(radian), bulletOrigin.Y - 120f * (float)Math.Sin(radian)));
                bullet.Shoot(angle, 5);
                bullet.SetParameters(null, null, mod, -.1f, 1);
                pq.AddTask(150, rand.NextFloat(1f, 1.5f), angle, rand.NextFloat(-.2f, .2f), 0, 0);
                angle += 360f / 5;
            }

            for (int i = 0; i < 18; ++i)
            {
                float radian = MathHelper.ToRadians(angle3);
                ParameterQueue pq = new ParameterQueue();
                Bullet bullet = new Bullet(pq, new Vector2(bulletOrigin.X + 120f * (float)Math.Cos(radian), bulletOrigin.Y + 120f * (float)Math.Sin(radian)));
                bullet.Shoot(angle3 + 90, -1 * mod);
                bullet.SetParameters(null, null, -1 * mod, 0, 0);
                pq.AddTask(120, mod, angle3 + 90, 0, 0, 0);
                angle3 += 360 / 18;
            }
            angle3 += 5 * mod;
            angle += 5 * mod;
        }

        private void ButterflyBarrage()
        {
            for (float i = 1; i < 10; ++i)
            {
                for (int j = 0; j < 40; ++j)
                {
                    Bullet bullet = new Bullet(bulletOrigin);
                    bullet.Shoot(angle, 1 + i / 3);
                    angle += 360f / 40;
                }
            }
        }

        private void ButterflyBarrage2(int modifier)
        {
            for (int i = 0; i < 20; ++i)
            {
                for (int j = 0; j < 30; ++j)
                {
                    Bullet bullet = new Bullet(new ButterflyBarrage(modifier, i), bulletOrigin);
                    bullet.TurnSpeed = -3 * modifier;
                    bullet.Acceleration = -.2f;
                    bullet.SpeedLimit = 1;
                    bullet.Shoot(angle * modifier, 5);
                    angle += 360f / 30;
                }
            }
        }

        private IEnumerator<int> CurtainBarrage()
        {
            for (int i = 0; i < 60; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    float radian = MathHelper.ToRadians(angle);
                    Bullet bullet = new Bullet(new Vector2(bulletOrigin.X + 60f * (float)Math.Cos(radian), bulletOrigin.Y + 100f * (float)Math.Sin(radian)));
                    bullet.AutomaticCollision = false;
                    bullet.Shoot(bullet.DirectionAngleToPlayer, 3.5f);                    
                    bullet = new Bullet(new Vector2(bulletOrigin.X + 60f * (float)Math.Cos(radian), bulletOrigin.Y + 100f * (float)Math.Sin(radian)));
                    bullet.AutomaticCollision = false;
                    bullet.Shoot(bullet.DirectionAngleToPlayer + 90f, -3.5f);                    
                    bullet = new Bullet(new Vector2(bulletOrigin.X + 60f * (float)Math.Cos(radian), bulletOrigin.Y + 100f * (float)Math.Sin(radian)));
                    bullet.AutomaticCollision = false;
                    bullet.Shoot(bullet.DirectionAngleToPlayer + 90f, 3.5f);
                    bullet = new Bullet(new Vector2(bulletOrigin.X + 60f * (float)Math.Cos(radian), bulletOrigin.Y + 100f * (float)Math.Sin(radian)));
                    bullet.AutomaticCollision = false;
                    bullet.Shoot(bullet.DirectionAngleToPlayer, -3.5f);
                    angle += 360f / 3f;
                    yield return 1;
                }
                angle += 12.5f;
            }
            yield return 300;
        }

        private IEnumerator<int> OngoingBarrage()
        {
            for (int i = 0; i < 20; ++i)
            {
                Bullet bullet = new Bullet(bulletOrigin);
                bullet.Shoot(angle, 2.8f);
                angle += 360 / 20;
            }
            angle += 4;
            yield return 20;
        }

        private void AntiSafeSpotBarrage(int modifier)
        {            
            Bullet bullet = new Bullet(bulletOrigin);
            bullet.Acceleration = -.1f;
            bullet.SpeedLimit = 3f;
            bullet.Shoot(bullet.DirectionAngleToPlayer + modifier, 6f); 
        }

        private void PlayerPrison()
        {
            float angle = 0;
            for (int i = 0; i < 120; ++i)
            {
                float radian = MathHelper.ToRadians(angle);
                Bullet bullet = new Bullet(new PlayerTrap1(angle), new Vector2(Player.PlayerPosX + 150f * (float)Math.Cos(radian), Player.PlayerPosY + 150f * (float)Math.Sin(radian)));
                bullet.Acceleration = .1f;
                bullet.SpeedLimit = 0;
                bullet.Shoot(angle, -3);
                bullet.AutomaticCollision = false;
                Bullet bullet2 = new Bullet(new PlayerTrap2(angle), new Vector2(Player.PlayerPosX + 90f * (float)Math.Cos(radian), Player.PlayerPosY  + 90f * (float)Math.Sin(radian)));
                bullet2.Acceleration = .05f;
                bullet2.SpeedLimit = 0;
                bullet2.Shoot(angle, -2);
                bullet2.AutomaticCollision = false;
                angle += 360 / 120;
            }
        }

        private void ButterflyCircleShot(int modifier)
        {
            float angle = 0;
            for (float i = 0; i < 10; ++i)
            {
                for (int j = 0; j < 60; ++j)
                {
                    Bullet bullet = new Bullet(new ButterflyMayhem(modifier, i), bulletOrigin);                    
                    bullet.TurnSpeed = -3f * modifier;
                    bullet.Acceleration = -.5f;
                    bullet.SpeedLimit = 2f;
                    bullet.Shoot(angle * modifier, 5f);
                    angle += 360 / 60;
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
                    Bullet bullet = new Bullet(bulletOrigin);                    
                    bullet.Shoot(angle, 1 + i);
                    angle += 360 / 40;
                }
            }
        }

        protected Vector2 GetVectorAroundPoint(Vector2 center, float distance, float angle)
        { 
            float radian = MathHelper.ToRadians(angle);
            return new Vector2(center.X + distance * (float)Math.Cos(radian), center.Y + distance * (float)Math.Sin(angle));
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

        protected virtual void SwitchPhase()
        {
            switch (phase)
            {
                case 1: 
                case 3:
                    phaseTimer = 20;
                    break;

                default:
                    phaseTimer = 30;
                    break;
                
            }
        }
    }
}
