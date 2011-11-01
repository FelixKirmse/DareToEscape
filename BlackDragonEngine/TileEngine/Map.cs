using System;
using System.Collections.Generic;
using BlackDragonEngine.Helpers;

namespace BlackDragonEngine.TileEngine
{
    [Serializable]
    public class Map
    {
        public SerializableDictionary<Coords, MapSquare> MapData = new SerializableDictionary<Coords, MapSquare>();
        public SerializableDictionary<Coords, List<string>> Codes = new SerializableDictionary<Coords, List<string>>();
        public SerializableDictionary<string, string> Properties = new SerializableDictionary<string, string>();               

        public int MapWidth
        {
            get 
            {
                int maxX = 0;
                foreach (var item in MapData)
                {
                    if (item.Key.X > maxX)
                        maxX = item.Key.X;
                }
                return maxX;            
            }
        }

        public int MapHeight
        {
            get
            {
                int maxY = 0;
                foreach (var item in MapData)
                {
                    if (item.Key.Y > maxY)
                        maxY = item.Key.Y;
                }
                return maxY;
            }
        }

        public MapSquare this[int x, int y]
        {
            get 
            {
                return this[new Coords(x, y)];
            }

            set
            {
                this[new Coords(x, y)] = value;
            }
        }

        public MapSquare this[Coords coords]
        {
            get
            {
                if (MapData.ContainsKey(coords))
                    return MapData[coords];
                else
                    return null;                
            }

            set
            {
                if (MapData.ContainsKey(coords))
                {
                    MapData[coords] = value;
                }
                else
                    MapData.Add(coords, value);                                
            }
        }
    }
}
