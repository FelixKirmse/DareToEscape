using System;
using System.Collections.Generic;
using System.Linq;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Providers;

namespace BlackDragonEngine.TileEngine
{
    [Serializable]
    public sealed class Map<TCodes> : IMap<TCodes>
    {
        public Map()
        {
            var comparer = new CoordComparer();
            Codes = new Dictionary<Coords, List<TCodes>>(comparer);
            MapData = new Dictionary<Coords, MapSquare>(comparer);
        }

        #region IMap<TCodes> Members

        public Dictionary<Coords, List<TCodes>> Codes { get; private set; }
        public Dictionary<Coords, MapSquare> MapData { get; private set; }

        public int MapWidth
        {
            get { return HighestX - LowestX; }
        }

        public int MapHeight
        {
            get { return HighestY - LowestY; }
        }

        public int LowestX
        {
            get { return MapData.Keys.Select(coords => coords.X).Concat(new[] {0}).Min(); }
        }

        public int HighestX
        {
            get { return MapData.Keys.Select(coords => coords.X).Concat(new[] {0}).Max(); }
        }

        public int LowestY
        {
            get { return MapData.Keys.Select(coords => coords.Y).Concat(new[] {0}).Min(); }
        }

        public int HighestY
        {
            get { return MapData.Keys.Select(coords => coords.Y).Concat(new[] {0}).Max(); }
        }

        public MapSquare? this[int x, int y]
        {
            get { return this[VariableProvider.CoordList[x, y]]; }

            set { this[VariableProvider.CoordList[x, y]] = value; }
        }

        public MapSquare? this[Coords coords]
        {
            get { return MapData.ContainsKey(coords) ? MapData[coords] : (MapSquare?) null; }

            set
            {
                if (MapData.ContainsKey(coords))
                {
                    if (value == null)
                    {
                        MapData.Remove(coords);
                        return;
                    }
                    MapData[coords] = value.Value;
                }
                else
                {
                    if (value == null) return;
                    MapData.Add(coords, value.Value);
                }
            }
        }

        #endregion
    }
}