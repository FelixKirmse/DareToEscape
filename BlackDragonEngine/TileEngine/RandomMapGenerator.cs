using System;
using System.Collections.Generic;
using System.Linq;
using BlackDragonEngine.Providers;

namespace BlackDragonEngine.TileEngine
{
    public delegate bool CellCondition(Coords cell);

    public delegate void CustomAction(Coords cell);

    public class RandomMapGenerator
    {
        public static int MaxDiggers;
        private readonly List<Digger> _diggers = new List<Digger>();
        public int AddedDiggers { get; private set; }

        public void GenerateNewMap(int maxDiggers)
        {
            MaxDiggers = maxDiggers;
            TileMap.Map = new Map();
            _diggers.Clear();

            AddDigger(new Coords(0, 0));
            TileMap.AddCodeToCell(0, 0, "START");
            do
            {
                for (var i = 0; i < _diggers.Count; ++i)
                {
                    if (_diggers[i].Dig()) continue;
                    if (_diggers.Count > 1)
                        _diggers.Remove(_diggers[i]);
                    else
                        _diggers[i].LastManStanding = true;
                }
            } while (AddedDiggers < MaxDiggers);
        }

        private void AddDigger(Coords position)
        {
            _diggers.Add(new Digger(position, this));
            ++AddedDiggers;
        }


        public int ProgressMax;
        public int ProgressCounter;

        public void InvertMap()
        {
            var map = TileMap.Map;
            ProgressMax = TileMap.Map.Codes.Count;
            var cells = TileMap.Map.Codes.Keys.ToList();
            foreach(var cell in cells)
            {
                ++ProgressCounter;
                TileMap.AddCodeToCell(cell.Up, "AddedByInvert");
                TileMap.AddCodeToCell(cell.Down, "AddedByInvert");
                TileMap.AddCodeToCell(cell.Left, "AddedByInvert");
                TileMap.AddCodeToCell(cell.Right, "AddedByInvert");
                map[cell.Up] = new MapSquare(0, null, null, false);
                TileMap.AddCodeToCell(cell.UpLeft, "AddedByInvert");
                TileMap.AddCodeToCell(cell.UpRight, "AddedByInvert");
                map[cell.Down] = new MapSquare(0, null, null, false);
                map[cell.Left] = new MapSquare(0, null, null, false);
                map[cell.Right] = new MapSquare(0, null, null, false);
                TileMap.AddCodeToCell(cell.DownLeft, "AddedByInvert");
                TileMap.AddCodeToCell(cell.DownRight, "AddedByInvert");
                map[cell.UpLeft] = new MapSquare(0, null, null, false);
                map[cell.UpRight] = new MapSquare(0, null, null, false);
                map[cell.DownLeft] = new MapSquare(0, null, null, false);
                map[cell.DownRight] = new MapSquare(0, null, null, false);
            }
            ProgressMax = map.MapData.Count;
            ProgressCounter = 0;
            RemoveCellsByCondition(cell =>
                                       {
                                           ++ProgressCounter;
                                           return TileMap.GetCellCodes(cell).Contains("OriginalPlaced");
                                       });
            var codesToDelete =
                (from cell in TileMap.Map.Codes where TileMap.Map.Codes[cell.Key].Contains("OriginalPlaced") select cell.Key).
                    ToList();
            codesToDelete.ForEach(cell => map.Codes[cell].RemoveAll(s => s == "OriginalPlaced"));
        }

        public void RemoveCellsByCondition(CellCondition condition)
        {
            ManipulateCellsByCondition(TileMap.RemoveEverythingAtCell, condition);
        }

        public void ManipulateCellsByCondition(CustomAction action, CellCondition condition)
        {
            var cellsToManipulate =
                (from cell in TileMap.Map.MapData where condition(cell.Key) select cell.Key).ToList();
            cellsToManipulate.ForEach(cell => action(cell));
        }

        #region Nested type: Digger

        private class Digger
        {
            private readonly RandomMapGenerator _mapGen;
            private readonly Coords[] _positions = new Coords[4];

            private readonly Random _rand;

            public Digger(Coords position, RandomMapGenerator mapGen)
            {
                _mapGen = mapGen;
                Position = position;
                _rand = VariableProvider.RandomSeed;
                foreach (var cell in _positions)
                {
                    TileMap.SetSolidTileAtCell(cell);
                    TileMap.AddUniqueCodeToCell(cell, "OriginalPlaced");
                }
            }

            private Coords Position
            {
                get { return _positions[0]; }
                set
                {
                    _positions[0] = value;
                    _positions[1] = new Coords(value.X + 1, value.Y);
                    _positions[2] = new Coords(value.X, value.Y + 1);
                    _positions[3] = new Coords(value.X + 1, value.Y + 1);
                }
            }

            public bool LastManStanding { private get; set; }

            public bool Dig()
            {
                var diggableBlocks = GetBlocksToDig();

                if (diggableBlocks.Count > 0)
                {
                    Position = diggableBlocks[_rand.Next(0, diggableBlocks.Count)];
                    foreach (var cell in _positions)
                    {
                        TileMap.SetSolidTileAtCell(cell);
                        TileMap.AddUniqueCodeToCell(cell, "OriginalPlaced");
                    }
                    if (_rand.Next(1, 101) < 4)
                    {
                        LastManStanding = false;
                        _mapGen.AddDigger(Position);
                    }
                    return true;
                }
                return false;
            }

            private List<Coords> GetBlocksToDig()
            {
                var possibleBlocks = new List<Coords>();
                if (BlockIsMineAble(_positions[0].Up))
                {
                    possibleBlocks.Add(_positions[0].Up);
                }
                if (BlockIsMineAble(_positions[1].Up))
                {
                    possibleBlocks.Add(_positions[0].Up);
                }
                if (BlockIsMineAble(_positions[2].Down))
                {
                    possibleBlocks.Add(_positions[0].Down);
                }
                if (BlockIsMineAble(_positions[3].Down))
                {
                    possibleBlocks.Add(_positions[0].Down);
                }
                if (BlockIsMineAble(_positions[0].Left))
                {
                    possibleBlocks.Add(_positions[0].Left);
                }
                if (BlockIsMineAble(_positions[2].Left))
                {
                    possibleBlocks.Add(_positions[0].Left);
                }
                if (BlockIsMineAble(_positions[1].Right))
                {
                    possibleBlocks.Add(_positions[0].Right);
                }
                if (BlockIsMineAble(_positions[3].Right))
                {
                    possibleBlocks.Add(_positions[0].Right);
                }
                return possibleBlocks;
            }

            private bool BlockIsMineAble(Coords coords)
            {
                return LastManStanding || TileMap.CellIsPassable(coords);
            }
        }

        #endregion
    }
}