using System.Collections.Generic;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Providers;
using Microsoft.Xna.Framework.Graphics;

namespace BlackDragonEngine.Managers
{
    public static class EntityManager
    {
        private static GameObject player;        
        public static List<GameObject> Entities = new List<GameObject>();

        public static void Update()
        {
            foreach (GameObject entity in Entities)
            {
                entity.Update();
            }
            player.Update();              
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject entity in Entities)
            {
                entity.Draw(spriteBatch);
            }
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
