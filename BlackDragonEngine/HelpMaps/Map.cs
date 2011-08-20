using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackDragonEngine.HelpMaps
{
    [Serializable]
    public class MapRow
    {
        public List<MapSquare> MapCellRow = new List<MapSquare>();

        public void AddRow(bool passable)
        {
            MapCellRow.Add(new MapSquare(passable));
        }
    }

    [Serializable]
    public class HelpMap
    {
        public List<MapRow> MapCellColumns = new List<MapRow>();
    }
}
