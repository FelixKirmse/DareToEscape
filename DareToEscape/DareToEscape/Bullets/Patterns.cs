using System;
using System.Collections.Generic;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Providers;
using DareToEscape.Bullets.Behaviors;
using DareToEscape.Components.Entities;
using DareToEscape.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DareToEscape.Bullets
{
    internal sealed class Patterns
    {
        private readonly BossComponent _turret;
        private float _angle;
        private float _angle2;
        private float _angle3;
        private float _angle4 = 90;

        public Patterns(BossComponent turret)
        {
            _turret = turret;
        }

        private Vector2 BulletOrigin
        {
            get { return _turret.BulletOrigin; }
        }

        public void Phase4Frame3200Shot()
        {
            for (int i = 0; i < 15; ++i)
            {
                var bullet = new Bullet(BulletOrigin, 243, BlendState.Additive);
                bullet.Shoot(_angle4, 2.5f);
                _angle4 += 24f; //360f / 15;
            }
            _angle4 -= 17f;
        }

        public void Phase4Frame2120Shot(float angle)
        {
            _angle = angle;
            for (int i = 0; i < 8; ++i)
            {
                var bullet = new Bullet(BulletOrigin, 51);
                bullet.Shoot(_angle2, 3);
                _angle2 += 360f/12;
                bullet = new Bullet(BulletOrigin, 172, BlendState.Additive);
                bullet.Shoot(_angle, 1);
                _angle += 360f/6;
                float radian = MathHelper.ToRadians(_angle3);
                bullet =
                    new Bullet(
                        new Vector2(BulletOrigin.X + 100f*(float) Math.Cos(radian),
                                    BulletOrigin.Y + 100f*(float) Math.Sin(radian)), 172,
                        BlendState.Additive) {TurnSpeed = .57f, KillTime = 120};
                bullet.Shoot(_angle3 + 90, 1);
                _angle3 += 45f; //360f / 8;
                _angle2 += 4f;
            }
        }

        public void Phase3Shot()
        {
            for (int i = 0; i < 10; ++i)
            {
                for (int j = 0; j < 5; ++j)
                {
                    ParameterQueue pq = ParameterQueue.GetInstance();
                    var bullet = new Bullet(pq, BulletOrigin, 51) {SpawnDelay = 5 + j};
                    bullet.Shoot(_angle2 + j*4, 0);
                    pq.AddTask(j*4, 1f, null, 0f, .1f, 3f);
                }

                for (int j = 0; j < 5; ++j)
                {
                    ParameterQueue pq = ParameterQueue.GetInstance();
                    var bullet = new Bullet(pq, BulletOrigin, 51) {SpawnDelay = 5 + j};
                    bullet.Shoot(_angle2 - j*4, 0);
                    pq.AddTask(j*4, 1f, null, 0f, .1f, 3f);
                }
                _angle2 += 36f; //360f / 10f;
            }
        }

        public IEnumerator<int> CircleBarrage(params float[] p)
        {
            float mod = p[0];
            for (int j = 0; j < 5; ++j)
            {
                for (int i = 0; i < 50; ++i)
                {
                    var bullet = new Bullet(GetVectorAroundPoint(BulletOrigin, 20, _angle), 83);
                    bullet.SetParameters(null, null, .05f*mod, 0, 0);
                    bullet.Shoot(_angle, 3);
                    _angle += 7.2f; //360f / 50;
                }
                _angle += 4f;
                yield return 10;
            }
        }

        public IEnumerator<int> LineBarrage(params float[] p)
        {
            for (int i = 0; i < 15; ++i)
            {
                var bullet = new Bullet(BulletOrigin, 216);
                bullet.Shoot(bullet.DirectionAngleToPlayer, 5);
                bullet = new Bullet(BulletOrigin, 216);
                bullet.Shoot(bullet.DirectionAngleToPlayer + 20, 5);
                bullet = new Bullet(BulletOrigin, 216);
                bullet.Shoot(bullet.DirectionAngleToPlayer - 20, 5);
                yield return 5;
            }
        }

        public IEnumerator<int> BulletFlower(params float[] parameters)
        {
            float mod = parameters[0];
            Random rand = VariableProvider.RandomSeed;
            for (int j = 0; j < 10; ++j)
            {
                for (int i = 0; i < 80; ++i)
                {
                    ParameterQueue pq = ParameterQueue.GetInstance();
                    var bullet = new Bullet(pq, BulletOrigin, 51);
                    bullet.SetParameters(null, null, 0, -.1f, 1);
                    bullet.AutomaticCollision = false;
                    bullet.Shoot(_angle2, 5);
                    pq.AddTask(60, 1, _angle2, 0, -.2f, -1);
                    pq.AddTask(120, -1, _angle2, -2*mod, .2f, 2);
                    pq.AddTask(180, -1, _angle2, rand.NextFloat(-.1f, .1f), -.1f, rand.NextFloat(-3, -1));
                    _angle2 += 6f; //360f / 60;
                }
                yield return 6;
            }
        }

        public IEnumerator<int> Pentagram(params float[] parameters)
        {
            float mod = parameters[0];
            Random rand = VariableProvider.RandomSeed;
            for (int j = 0; j < 50; ++j)
            {
                for (int i = 0; i < 15; ++i)
                {
                    float radian = MathHelper.ToRadians(_angle);
                    ParameterQueue pq = ParameterQueue.GetInstance();
                    var bullet = new Bullet(pq,
                                            new Vector2(BulletOrigin.X - 120f*(float) Math.Cos(radian),
                                                        BulletOrigin.Y - 120f*(float) Math.Sin(radian)), 172,
                                            BlendState.Additive);
                    bullet.SetParameters(null, null, mod, -.1f, 1);
                    bullet.AutomaticCollision = false;
                    bullet.Shoot(_angle, 5);
                    pq.AddTask(150, rand.NextFloat(1f, 1.5f), _angle, rand.NextFloat(-.2f, .2f), 0, 0);
                    _angle += 72f; //360f / 5;
                }

                for (int i = 0; i < 18; ++i)
                {
                    float radian = MathHelper.ToRadians(_angle3);
                    ParameterQueue pq = ParameterQueue.GetInstance();
                    var bullet = new Bullet(pq,
                                            new Vector2(BulletOrigin.X + 120f*(float) Math.Cos(radian),
                                                        BulletOrigin.Y + 120f*(float) Math.Sin(radian)), 176,
                                            BlendState.Additive) {AutomaticCollision = false};
                    bullet.SetParameters(null, null, -1*mod, 0, 0);
                    bullet.Shoot(_angle3 + 90, -1*mod);
                    pq.AddTask(120, mod, _angle3 + 90, 0, 0, 0);
                    _angle3 += 20f; //360 / 18;
                }
                _angle3 += 5f*mod;
                _angle += 5f*mod;
                yield return 10;
            }
        }

        public void ButterflyBarrage()
        {
            for (float i = 1; i < 10; ++i)
            {
                for (int j = 0; j < 40; ++j)
                {
                    var bullet = new Bullet(BulletOrigin, 83);
                    bullet.Shoot(_angle, 1 + i/3);
                    _angle += 9f; //360f / 40;
                }
            }
        }

        public void ButterflyBarrage2(int modifier)
        {
            for (float i = 0; i < 20; ++i)
            {
                for (int j = 0; j < 30; ++j)
                {
                    ParameterQueue pq = ParameterQueue.GetInstance();
                    var bullet = new Bullet(pq, BulletOrigin, 228)
                                     {TurnSpeed = -3*modifier, Acceleration = -.2f, SpeedLimit = 1};
                    bullet.Shoot(_angle*modifier, 5);
                    pq.AddTask(60, 2, null, .5f*modifier, .1f, 1 + (i/4f));
                    pq.AddTask(120, null, null, 0f, 0f, 0f);
                    _angle += 12f; //360f / 30;
                }
            }
        }

        public IEnumerator<int> CurtainBarrage(params float[] parameters)
        {
            for (int i = 0; i < 60; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    float radian = MathHelper.ToRadians(_angle);
                    var bullet =
                        new Bullet(
                            new Vector2(BulletOrigin.X + 60f*(float) Math.Cos(radian),
                                        BulletOrigin.Y + 100f*(float) Math.Sin(radian)), 20)
                            {AutomaticCollision = false};
                    bullet.Shoot(bullet.DirectionAngleToPlayer, 3.5f);
                    bullet =
                        new Bullet(
                            new Vector2(BulletOrigin.X + 60f*(float) Math.Cos(radian),
                                        BulletOrigin.Y + 100f*(float) Math.Sin(radian)), 20)
                            {AutomaticCollision = false};
                    bullet.Shoot(bullet.DirectionAngleToPlayer + 90f, -3.5f);
                    bullet =
                        new Bullet(
                            new Vector2(BulletOrigin.X + 60f*(float) Math.Cos(radian),
                                        BulletOrigin.Y + 100f*(float) Math.Sin(radian)), 20)
                            {AutomaticCollision = false};
                    bullet.Shoot(bullet.DirectionAngleToPlayer + 90f, 3.5f);
                    bullet =
                        new Bullet(
                            new Vector2(BulletOrigin.X + 60f*(float) Math.Cos(radian),
                                        BulletOrigin.Y + 100f*(float) Math.Sin(radian)), 20)
                            {AutomaticCollision = false};
                    bullet.Shoot(bullet.DirectionAngleToPlayer, -3.5f);
                    _angle += 120f; //360f / 3f;
                    yield return 1;
                }
                _angle += 12.5f;
            }
            yield return 300;
        }

        public IEnumerator<int> OngoingBarrage(params float[] parameters)
        {
            for (int i = 0; i < 20; ++i)
            {
                var bullet = new Bullet(BulletOrigin, 31);
                bullet.Shoot(_angle, 2.8f);
                _angle += 18f; //360 / 20;
            }
            _angle += 4f;
            yield return 20;
        }

        public IEnumerator<int> AntiSafeSpotBarrage(params float[] parameters)
        {
            float modifier = parameters[0];
            for (int i = 0; i < 10; ++i)
            {
                var bullet = new Bullet(BulletOrigin, 69) {Acceleration = -.1f, SpeedLimit = 3f};
                bullet.Shoot(bullet.DirectionAngleToPlayer + modifier, 6f);
                yield return 10;
            }
        }

        public void PlayerPrison()
        {
            float angle = 0;
            for (int i = 0; i < 120; ++i)
            {
                float radian = MathHelper.ToRadians(angle);
                ParameterQueue pq = ParameterQueue.GetInstance();
                var bullet = new Bullet(pq,
                                        new Vector2(Player.PlayerPosX + 150f*(float) Math.Cos(radian),
                                                    Player.PlayerPosY + 150f*(float) Math.Sin(radian)), 50,
                                        BlendState.Additive)
                                 {Acceleration = .1f, SpeedLimit = 0, AutomaticCollision = false};
                bullet.Shoot(angle, -3);
                pq.AddTask(110, 0f, angle, 0f, 0f, 0f);
                pq.AddTask(380, -1f, angle, .2f, 0f, 0f);
                pq.AddTask(700, null, null, 0f, -.5f, -3);
                pq = ParameterQueue.GetInstance();
                var bullet2 = new Bullet(pq,
                                         new Vector2(Player.PlayerPosX + 90f*(float) Math.Cos(radian),
                                                     Player.PlayerPosY + 90f*(float) Math.Sin(radian)), 50,
                                         BlendState.Additive)
                                  {Acceleration = .05f, SpeedLimit = 0, AutomaticCollision = false};
                bullet2.Shoot(angle, -2);
                pq.AddTask(110, 0f, angle, 0f, 0f, 0f);
                pq.AddTask(380, 1, angle, .05f, 0f, 0f);
                pq.AddTask(700, null, null, 0f, .5f, 3f);

                angle += 3f; //360 / 120;
            }
        }

        public void ButterflyCircleShot(int modifier)
        {
            float angle = 0;
            for (float i = 0; i < 10; ++i)
            {
                for (int j = 0; j < 60; ++j)
                {
                    ParameterQueue pq = ParameterQueue.GetInstance();
                    var bullet = new Bullet(pq, BulletOrigin, 228);
                    bullet.SetParameters(null, null, -3f*modifier, -.5f, 2f);
                    bullet.Shoot(angle*modifier, 5f);
                    pq.AddTask(60, 2f, null, 1f*modifier, .2f, 2f + (i/3f));
                    pq.AddTask(120, null, null, 0f, 0f, 0f);
                    angle += 6f; //360 / 60;
                }
            }
        }

        public void Shoot3Circles()
        {
            float angle = 0;
            for (int i = 1; i < 4; ++i)
            {
                for (int j = 0; j < 40; ++j)
                {
                    var bullet = new Bullet(BulletOrigin, 216);
                    bullet.Shoot(angle, 1 + i);
                    angle += 9f; //360 / 40;
                }
            }
        }

        private static Vector2 GetVectorAroundPoint(Vector2 center, float distance, float angle)
        {
            float radian = MathHelper.ToRadians(angle);
            return new Vector2(center.X + distance*(float) Math.Cos(radian),
                               center.Y + distance*(float) Math.Sin(radian));
        }
    }
}