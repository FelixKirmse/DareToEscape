using System.Collections.Generic;

namespace BlackDragonEngine.TileEngine
{
    public class CoordList
    {
        private const int ComputeSize = 500;
        private readonly List<List<Coords>> _coordsList = new List<List<Coords>>(ComputeSize);

        public CoordList()
        {
            for (var x = 0; x <= ComputeSize; ++x)
            {
                _coordsList.Add(new List<Coords>(ComputeSize));
                for (var y = 0; y <= ComputeSize; ++y)
                {
                    _coordsList[x].Add(new Coords(x,y));
                }
            }
        }

        public Coords this[int x, int y]
        {
            get { return _coordsList[x][y]; }
        }
    }
}
