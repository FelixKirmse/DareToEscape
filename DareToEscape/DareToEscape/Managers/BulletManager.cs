using System.Collections.Generic;
using System.Linq;
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
        public  static float CurrentDrawDepth = .82f;
        private readonly List<Bullet> _bullets = new List<Bullet>();
        private readonly int _processorCount;
        private int _counter;
        private readonly Task[] _tasks; 

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
            var bulletsToProcess = bulletCount / _processorCount;
            
            //Split up the bullets to update among all available cores using Tasks and a lambda expression
            for (var i = 0; i < _processorCount; ++i )
            {
                var x = i;
                _tasks[i] = Task.Factory.StartNew( () =>
                                           {
                                               for(var j = bulletsToProcess * x; j < bulletsToProcess * x + bulletsToProcess; ++j)
                                               {
                                                   if (_bullets[j].Active)
                                                        _bullets[j].Update();
                                               }
                                           });
            }
            
            //Update the remaining bullets (if any)
            for (var i = bulletsToProcess * _processorCount; i < bulletCount; ++i)
            {
                if (_bullets[i].Active)
                    _bullets[i].Update();
            }
            //Wait for all tasks to finish
            Task.WaitAll(_tasks);

            //This is an attempt to reduce the load per frame, originally _bullets.RemoveAll(s => !s.Active) ran every frame.
            ++_counter;
            if (_counter != 300) return;
            _counter = 0;
            _bullets.RemoveAll(s => !s.Active);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (StateManager.GameState != GameStates.Ingame && StateManager.GameState != GameStates.Editor) return;

            spriteBatch.DrawString(FontProvider.GetFont("Mono14"), _bullets.Count.ToString(), new Vector2(100, 20),
                                   Color.White);

            //Using some LINQ to only draw bullets in the viewport
            foreach (var bullet in _bullets.Where(bullet => Camera.ViewPort.Contains(bullet.CircleCollisionCenter.ToPoint())))
            {
                bullet.Draw(spriteBatch);
                CurrentDrawDepth -= .82e-5f;
            }
            CurrentDrawDepth = .82f;
        }
    }
}