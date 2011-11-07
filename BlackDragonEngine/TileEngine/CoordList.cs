using System.Collections.Generic;

namespace BlackDragonEngine.TileEngine
{
    public class CoordList
    {
        private readonly List<List<Coords>> _coordsList = new List<List<Coords>>(250000);

        public CoordList()
        {
            for(var x = 0; x <= 500; ++x)
            {
                _coordsList.Add(new List<Coords>());
                for(var y = 0; y <= 500; ++y)
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
