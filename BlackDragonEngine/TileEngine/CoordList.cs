using System.Collections.Generic;

namespace BlackDragonEngine.TileEngine
{
    public sealed class CoordList
    {
        private readonly Dictionary<int, Dictionary<int, Coords>> _coords =
            new Dictionary<int, Dictionary<int, Coords>>();

        public Coords this[int x, int y]
        {
            get
            {
                lock (_coords)
                {
                    if (!_coords.ContainsKey(x)) _coords.Add(x, new Dictionary<int, Coords>());
                    if (!_coords[x].ContainsKey(y)) _coords[x].Add(y, new Coords(x, y));
                    return _coords[x][y];
                }
            }
        }
    }
}