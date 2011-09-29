using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackDragonEngine.TileEngine
{
    [Serializable]
    public class MapRow
    {
        public List<MapSquare> MapCellRow = new List<MapSquare>();

        public void AddRow(int? background, int? interactive, int? foreground, bool passable)
        {
            MapCellRow.Add(new MapSquare(background, interactive, foreground, passable));
        }
    }

    [Serializable]
    public class Map
    {
        public List<MapRow> MapCellColumns = new List<MapRow>();
        public Dictionary<string, string> Properties = new Dictionary<string, string>(); 
    }
}
