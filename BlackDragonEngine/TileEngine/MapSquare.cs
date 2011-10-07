using System;
using System.Collections.Generic;

namespace BlackDragonEngine.TileEngine
{
    [Serializable]
    public class MapSquare
    {
        #region Declarations
        public int?[] LayerTiles = new int?[3];
        public List<string> Codes = new List<string>();
        public bool Passable = true;
        #endregion

        #region Constructor
        private MapSquare()
        { 
        }

        public MapSquare(int layer, int? tile)
        {
            LayerTiles[layer] = tile;
            Passable = true;
        }

        public MapSquare(int? background, int? interactive, int? foreground, bool passable)
        {
            LayerTiles[0] = background;
            LayerTiles[1] = interactive;
            LayerTiles[2] = foreground;            
            Passable = passable;
        }
        #endregion

        #region Public Methods
        public void TogglePassable() {
            Passable = !Passable;
        }
        #endregion
    }
}
