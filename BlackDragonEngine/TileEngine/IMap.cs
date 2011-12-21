using System.Collections.Generic;

namespace BlackDragonEngine.TileEngine
{
    public interface IMap<TCodes>
    {
        Dictionary<Coords, List<TCodes>> Codes { get; }
        Dictionary<Coords, MapSquare> MapData { get; }
        int MapWidth { get; }
        int MapHeight { get; }
        int LowestX { get; }
        int HighestX { get; }
        int LowestY { get; }
        int HighestY { get; }

        MapSquare this[int x, int y] { get; set; }

        MapSquare this[Coords coords] { get; set; }
    }
}