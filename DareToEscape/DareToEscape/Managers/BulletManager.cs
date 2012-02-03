using System.Collections.Generic;
using System.Threading.Tasks;
using BlackDragonEngine;
using BlackDragonEngine.GameStates;
using BlackDragonEngine.Helpers;
using DareToEscape.Bullets;

namespace DareToEscape.Managers
{
    public sealed class BulletManager : IUpdateableGameState, IDrawableGameState
    {
        private static BulletManager _instance;
        private readonly List<Bullet> _bullets = new List<Bullet>(50000);
        private readonly List<int> _bulletsToDelete = new List<int>(1000);

        private BulletManager()
        {
        }

        #region IDrawableGameState Members

        public bool DrawCondition
        {
            get { return GameStateManager.State == States.Ingame || GameStateManager.State == States.Editor; }
        }

        public void Draw()
        {
            for (int i = 0; i < _bullets.Count; ++i)
            {
                if (!Camera.ViewPort.Contains(_bullets[i].CircleCollisionCenter.ToPoint())) continue;
                _bullets[i].Draw();
            }
        }

        #endregion

        #region IUpdateableGameState Members

        public bool UpdateCondition
        {
            get
            {
                return GameStateManager.State == States.Ingame ||
                       (GameStateManager.State == States.Editor && EngineState.GameState == EngineStates.Running);
            }
        }

        public bool Update()
        {
            int bulletCount = _bullets.Count;
            if (bulletCount == 0) return true;

            Parallel.For(0, bulletCount, j => _bullets[j] = _bullets[j].Update(j, _bulletsToDelete));

            _bulletsToDelete.Sort((x, y) =>
                                      {
                                          if (x < y) return 1;
                                          if (x > y) return -1;
                                          return 0;
                                      });
            foreach (var id in _bulletsToDelete)
            {
                _bullets[id] = _bullets[_bullets.Count - 1];
                _bullets.RemoveAt(_bullets.Count - 1);
            }
            _bulletsToDelete.Clear();
            return true;
        }

        #endregion

        public static BulletManager GetInstance()
        {
            return _instance ?? (_instance = new BulletManager());
        }

        public void ClearAllBullets()
        {
            _bullets.Clear();
        }

        public void AddBullet(Bullet bullet)
        {
            _bullets.Add(bullet);
        }
    }
}