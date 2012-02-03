using System;

namespace BlackDragonEngine.TileEngine
{
    [Serializable]
    public struct MapSquare
    {
        public static uint Layers { get; set; }

        #region Declarations

        public bool InValidSquare;
        public int?[] LayerTiles;
        public bool Passable;

        #endregion

        #region Constructor

        public MapSquare(uint layer, int? tile)
        {
            LayerTiles = new int?[Layers];
            LayerTiles[layer] = tile;
            Passable = true;
            InValidSquare = false;
        }

        public MapSquare(int? tile, bool? passable, uint layer = 0)
            : this(layer, tile)
        {
            Passable = passable ?? true;
        }

        #endregion
    }
}