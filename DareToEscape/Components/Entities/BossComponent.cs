﻿using System.Collections.Generic;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Providers;
using BlackDragonEngine.Scripting;
using DareToEscape.Bullets;
using DareToEscape.Managers;
using DareToEscape.Providers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DareToEscape.Components.Entities
{
    internal abstract class BossComponent : TurretComponent
    {
        protected readonly Patterns Patterns;
        private bool _active = true;
        private double _timeTracker;
        protected int Phase;
        protected bool Shoot;

        protected BossComponent()
        {
            Texture = VariableProvider.Content.Load<Texture2D>(@"textures/entities/boss1");
            Patterns = new Patterns(this);
        }

        protected int PhaseTimer
        {
            get => (int) _timeTracker;
            set => _timeTracker = value;
        }

        public override void Update(GameObject obj)
        {
            if (Shoot)
            {
                --_timeTracker;
                if (_timeTracker <= 0)
                {
                    ++Phase;
                    BulletManager.GetInstance().ClearAllBullets();
                    VariableProvider.ScriptEngine.StopAllScripts();
                    SwitchPhase();
                }
            }

            if (_active || !GameVariableProvider.SaveManager.CurrentSaveState.BossDead) base.Update(obj);
        }

        public override void Draw(GameObject obj)
        {
            if (_active || !GameVariableProvider.SaveManager.CurrentSaveState.BossDead)
                base.Draw(obj);
        }

        protected void StartScript(Script script, params float[] parameters)
        {
            VariableProvider.ScriptEngine.ExecuteScript(script, parameters);
        }

        protected abstract override IEnumerator<int> ShootBehavior(params float[] parameters);


        protected override bool ShootCondition(Vector2 playerPosition, GameObject turret)
        {
            return Shoot;
        }

        public override void Receive<T>(string message, T obj)
        {
            if (message == "SHOOT")
                Shoot = true;
            if (message == "INACTIVE")
            {
                GameVariableProvider.SaveManager.CurrentSaveState.BossDead = true;
                GameVariableProvider.SaveManager.CurrentSaveState.Keys.Add("BOSS");
                _active = false;
            }

            base.Receive(message, obj);
        }

        protected abstract void SwitchPhase();
    }
}