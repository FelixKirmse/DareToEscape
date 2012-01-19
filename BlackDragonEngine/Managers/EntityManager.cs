using System.Collections.Generic;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Providers;
using Microsoft.Xna.Framework.Graphics;

namespace BlackDragonEngine.Managers
{
    public static class EntityManager
    {
        private static IGameObject _player;
        private static readonly List<IGameObject> Entities = new List<IGameObject>();

        public static void Update()
        {
            Entities.ForEach(e => e.Update());
            _player.Update();
        }

        public static void Draw()
        {
            SpriteBatch spriteBatch = VariableProvider.SpriteBatch;
            Entities.ForEach((e => e.Draw()));
            _player.Draw();
        }

        public static void SetPlayer()
        {
            _player = VariableProvider.CurrentPlayer;
        }

        public static void AddEntity(GameObject entity)
        {
            if (!Entities.Contains(entity))
                Entities.Add(entity);
        }

        public static void ClearEntities()
        {
            Entities.Clear();
        }
    }
}