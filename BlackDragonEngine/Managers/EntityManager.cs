using System.Collections.Generic;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Providers;
using Microsoft.Xna.Framework.Graphics;

namespace BlackDragonEngine.Managers
{
    public static class EntityManager
    {
        private static IGameObject player;
        private static readonly List<IGameObject> Entities = new List<IGameObject>();

        public static void Update()
        {
            Entities.ForEach(e => e.Update());
            player.Update();
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            Entities.ForEach((e => e.Draw(spriteBatch)));
            player.Draw(spriteBatch);
        }

        public static void SetPlayer()
        {
            player = VariableProvider.CurrentPlayer;
        }

        public static void AddEntity(GameObject entity)
        {
            Entities.Add(entity);
        }

        public static void ClearEntities()
        {
            Entities.Clear();
        }
    }
}