using System.Collections.Generic;
using System.Threading.Tasks;
using BlackDragonEngine;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Providers;
using DareToEscape.Bullets;
using Microsoft.Xna.Framework;

namespace DareToEscape.Managers
{
    internal class BulletManager
    {
        private readonly List<Bullet> _bullets = new List<Bullet>(50000);
        private readonly List<int> _bulletsToDelete = new List<int>(1000);
        private readonly int _processorCount;
        private readonly Task[] _tasks;

        public BulletManager()
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

        public void Update(GameTime gameTime)
        {
            if (StateManager.GameState != GameStates.Ingame &&
                (StateManager.GameState != GameStates.Editor || EngineStates.GameStates != EEngineStates.Running))
                return;

            int bulletCount = _bullets.Count;
            if (bulletCount == 0) return;
            int bulletsToProcess = bulletCount/_processorCount;

            for (int i = 0; i < _processorCount; ++i)
            {
                int x = i;
                _tasks[i] = Task.Factory.StartNew(() =>
                                                      {
                                                          for (int j = bulletsToProcess*x;
                                                               j < bulletsToProcess*x + bulletsToProcess;
                                                               ++j)
                                                          {
                                                              _bullets[j] = _bullets[j].Update(j, _bulletsToDelete);
                                                          }
                                                      });
            }

            for (int i = bulletsToProcess*_processorCount; i < bulletCount; ++i)
            {
                _bullets[i] = _bullets[i].Update(i, _bulletsToDelete);
            }


            Task.WaitAll(_tasks);

            _bulletsToDelete.Sort((x, y) =>
                                      {
                                          if (x < y) return 1;
                                          if (x > y) return -1;
                                          return 0;
                                      });
            foreach (int id in _bulletsToDelete)
            {
                _bullets[id] = _bullets[_bullets.Count - 1];
                _bullets.RemoveAt(_bullets.Count - 1);
            }
            _bulletsToDelete.Clear();
        }

        public void Draw()
        {
            if (StateManager.GameState != GameStates.Ingame && StateManager.GameState != GameStates.Editor) return;

            for (int i = 0; i < _bullets.Count; ++i)
            {
                if (!Camera.ViewPort.Contains(_bullets[i].CircleCollisionCenter.ToPoint())) continue;
                _bullets[i].Draw();
            }
        }
    }
}