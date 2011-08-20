using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackDragonEngine.HelpMaps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Windows.Forms;
using xTile;
using BlackDragonEngine.Providers;
using BlackDragonEngine.Helpers;

namespace BlackDragonEngine.Managers
{
    public delegate void OnLevelLoadEventHandler();

    public static class LevelManager
    {
        public static string CurrentLevel;
        public static Map CurrentMap;

        public static event OnLevelLoadEventHandler OnLevelLoad;

        public static void LoadLevel(string levelName)
        {
            CurrentLevel = levelName;                        
            
            CurrentMap = MapProvider.GetMap(levelName);  

            int editorLayerInt = CurrentMap.Properties["PlayerLayer"];
            xTile.Layers.Layer editorLayer = CurrentMap.Layers[editorLayerInt];
            TileMap.TileHeight = CurrentMap.Properties["TileSize"];
            TileMap.TileWidth = CurrentMap.Properties["TileSize"];
            TileMap.MapHeight = editorLayer.TileHeight / TileMap.TileHeight * editorLayer.LayerHeight;
            TileMap.MapWidth = editorLayer.TileWidth / TileMap.TileWidth * editorLayer.LayerWidth;

            TileMap.LoadMap(new FileStream(Application.StartupPath + @"\Content\maps\" + levelName + ".map", FileMode.Open), true);
            Camera.UpdateWorldRectangle();
            if (OnLevelLoad != null)
                OnLevelLoad();
        }

        public static void Draw()
        {
            CurrentMap.Update(VariableProvider.GameTime.ElapsedGameTime.Milliseconds);
            CurrentMap.Draw(VariableProvider.DisplayDevice, VariableProvider.Viewport);
        }

        public static void ReloadLevel()
        {
            LoadLevel(CurrentLevel);
        }
    }
}
