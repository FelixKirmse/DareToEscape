using System;
using System.IO;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.TileEngine;

namespace BlackDragonEngine.Managers
{
    public delegate void OnLevelLoadEventHandler();

    public static class LevelManager
    {
        public static string CurrentLevel;

        public static event OnLevelLoadEventHandler OnLevelLoad;

        public static void LoadLevel<TMap, TCodes>(string levelName) where TMap : IMap<TCodes>, new()
        {
            var tileMap = TileMap<TMap, TCodes>.GetInstance();
            CurrentLevel = levelName;
            tileMap.LoadMap(new FileStream($"{AppDomain.CurrentDomain.BaseDirectory}/Content/maps/{levelName}.map",
                FileMode.Open));
            Camera.UpdateWorldRectangle(tileMap);
            OnLevelLoad?.Invoke();
        }

        public static void ReloadLevel<TMap, TCodes>() where TMap : IMap<TCodes>, new()
        {
            LoadLevel<TMap, TCodes>(CurrentLevel);
        }
    }
}