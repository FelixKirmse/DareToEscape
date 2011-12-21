using System;

namespace BlackDragonEngine.TileEngine
{
    [Serializable]
    public struct MapSquare
    {
        #region Declarations

        public bool InValidSquare;
        public int[] LayerTiles;
        public bool Passable;

        #endregion

        #region Constructor

        public MapSquare(int layer, int tile)
        {
            LayerTiles = new int[1];
            LayerTiles[layer] = tile;
            Passable = true;
            InValidSquare = false;
        }

        public MapSquare(int tile, bool passable)
            : this(0, tile)
        {
            Passable = passable;
        }

        public MapSquare(bool invalid)
            : this()
        {
            InValidSquare = invalid;
        }

        #endregion
    }
}