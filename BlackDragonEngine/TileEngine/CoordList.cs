using System.Collections.Generic;

namespace BlackDragonEngine.TileEngine
{
    public class CoordList
    {

        private readonly List<Coords> _coords = new List<Coords>();

        public Coords this[int x, int y]
        {
            get
            {
                for(var i = 0; i < _coords.Count; ++i)
                {
                    if (_coords[i].X == x && _coords[i].Y == y)
                        return _coords[i];
                }
                var newCoords = new Coords(x, y);
                _coords.Add(newCoords);
                return newCoords;
            }
        }
    }
}
