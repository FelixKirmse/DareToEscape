using System;
using System.Collections.Generic;
using System.Linq;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Providers;

namespace BlackDragonEngine.TileEngine
{
    [Serializable]
    public class Map
    {
        public Dictionary<Coords, List<string>> Codes;
        public Dictionary<Coords, MapSquare> MapData;
        public Dictionary<string, string> Properties;

        public Map()
        {
            var comparer = new CoordComparer();
            Codes = new Dictionary<Coords, List<string>>(comparer);
            MapData = new Dictionary<Coords, MapSquare>(comparer);
            Properties = new Dictionary<string, string>();
        }

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

        public MapSquare this[int x, int y]
        {
            get { return this[VariableProvider.CoordList[x, y]]; }

            set { this[VariableProvider.CoordList[x, y]] = value; }
        }

        public MapSquare this[Coords coords]
        {
            get { return MapData.ContainsKey(coords) ? MapData[coords] : new MapSquare(true); }

            set
            {
                if (MapData.ContainsKey(coords))
                {
                    MapData[coords] = value;
                }
                else
                {
                    MapData.Add(coords, value);
                }
            }
        }
    }
}