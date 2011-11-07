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
        public SerializableDictionary<Coords, List<string>> Codes = new SerializableDictionary<Coords, List<string>>();
        public SerializableDictionary<Coords, MapSquare> MapData = new SerializableDictionary<Coords, MapSquare>();
        public SerializableDictionary<string, string> Properties = new SerializableDictionary<string, string>();
        public List<Coords> ValidCoords = new List<Coords>(); 

        public int MapWidth
        {
            get { return MapData.Select(item => item.Key.X).Concat(new[] {0}).Max() + 1; }
        }

        public int MapHeight
        {
            get { return MapData.Select(item => item.Key.Y).Concat(new[] {0}).Max() + 1; }
        }

        public MapSquare this[int x, int y]
        {
            get { return this[VariableProvider.CoordList[x, y]]; }

            set { this[VariableProvider.CoordList[x, y]] = value; }
        }

        public MapSquare this[Coords coords]
        {
            get { return MapData.ContainsKey(coords) ? MapData[coords] : null; }

            set
            {
                if (MapData.ContainsKey(coords))
                {
                    MapData[coords] = value;
                }
                else
                {
                    MapData.Add(coords, value);
                    ValidCoords.Add(coords);
                }
            }
        }
    }
}