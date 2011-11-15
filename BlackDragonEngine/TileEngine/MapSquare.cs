using System;

namespace BlackDragonEngine.TileEngine
{
    [Serializable]
    public struct MapSquare
    {
        #region Declarations

        public int[] LayerTiles;
        public bool Passable;
        public bool InValidSquare;

        #endregion

        #region Constructor
        public MapSquare(int layer, int tile)
        {
            LayerTiles = new int[TileMap.MapLayers];
            LayerTiles[layer] = tile;
            Passable = true;
            InValidSquare = false;
        }

        public MapSquare(int tile, bool passable)
            : this(0,tile)
        {
            Passable = passable;
        }

        public MapSquare(bool invalid)
            :this()
        {
            InValidSquare = invalid;
        }
        #endregion
    }
}