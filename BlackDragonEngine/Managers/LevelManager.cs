using System.IO;
using System.Windows.Forms;
using BlackDragonEngine.GameStates;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.TileEngine;
using Microsoft.Xna.Framework.Graphics;

namespace BlackDragonEngine.Managers
{
    public delegate void OnLevelLoadEventHandler();

    public static class LevelManager
    {
        public static string CurrentLevel;

        public static event OnLevelLoadEventHandler OnLevelLoad;

        public static void LoadLevel(string levelName)
        {
            CurrentLevel = levelName;
            TileMap.LoadMap(new FileStream(Application.StartupPath + @"\Content\maps\" + levelName + ".map",
                                           FileMode.Open));
            Camera.UpdateWorldRectangle();
            if (OnLevelLoad != null)
                OnLevelLoad();
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            TileMap.Draw(spriteBatch);
        }

        public static void ReloadLevel()
        {
            LoadLevel(CurrentLevel);
        }
    }
}