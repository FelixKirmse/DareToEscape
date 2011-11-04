using System.Collections.Generic;
using System.Text;
using BlackDragonEngine;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Providers;
using DareToEscape.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading.Tasks;

namespace DareToEscape.Managers
{
    internal class BulletManager : GameComponent
    {
        private readonly List<Bullet> _bullets = new List<Bullet>();
        private readonly int _processorCount;
        private int _counter;
        private readonly Task[] _tasks;
        readonly StringBuilder _sb = new StringBuilder();

        public BulletManager(Game game)
            : base(game)
        {
            _processorCount = VariableProvider.ProcessorCount;
            _tasks = new Task[_processorCount];
        }

        public void ClearAllBullets()
        {
            _bullets.Clear();
        }

        public void AddBullet(Bullet bullet)
        {
            _bullets.Add(bullet);
        }

        public override void Update(GameTime gameTime)
        {
            if (StateManager.GameState != GameStates.Ingame &&
                (StateManager.GameState != GameStates.Editor || EngineStates.GameStates != EEngineStates.Running))
                return;
            
            var bulletCount = _bullets.Count;
            if (bulletCount == 0) return;
            var bulletsToProcess = bulletCount / _processorCount;
            
            for (var i = 0; i < _processorCount; ++i )
            {
                var x = i;
                _tasks[i] = Task.Factory.StartNew( () =>
                                           {
                                               for(var j = bulletsToProcess * x; j < bulletsToProcess * x + bulletsToProcess; ++j)
                                               {
                                                   if (_bullets[j].Active)
                                                        _bullets[j] = _bullets[j].Update();
                                               }
                                           });
            }
            
            for (var i = bulletsToProcess * _processorCount; i < bulletCount; ++i)
            {
                if (_bullets[i].Active)
                    _bullets[i] = _bullets[i].Update();
            }
            
            Task.WaitAll(_tasks);
            
            ++_counter;
            if (_counter != 300) return;
            _counter = 0;
            _bullets.RemoveAll(s => !s.Active);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (StateManager.GameState != GameStates.Ingame && StateManager.GameState != GameStates.Editor) return;
            _sb.Clear();
            spriteBatch.DrawString(FontProvider.GetFont("Mono14"), _sb.Append(_bullets.Count).ToString(), new Vector2(100, 20),
                                   Color.White);
            
            foreach (var bullet in _bullets)
            {
                if (!Camera.ViewPort.Contains(bullet.CircleCollisionCenter.ToPoint())) continue;
                bullet.Draw();
            }
        }
    }
}