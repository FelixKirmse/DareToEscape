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
        public const int MaxDiggers = 100;
        private readonly List<Digger> _diggers = new List<Digger>();
        private readonly Random _rand = VariableProvider.RandomSeed;
        public int AddedDiggers { get; private set; }
    
        private int _height;
        private int _width;


        public void GenerateNewMap(int minWidth, int minHeight, int maxWidth, int maxHeight)
        {
            TileMap.Map = new Map();
            _width = _rand.Next(minWidth, maxWidth + 1);
            _height = _rand.Next(minHeight, maxHeight + 1);

            for (var x = 0; x < _width; ++x)
            {
                for (var y = 0; y < _height; ++y)
                {
                    var coords = new Coords(x, y);
                    var square = new MapSquare(0, null, null, false);
                    TileMap.Map.MapData.Add(coords, square);
                }
            }

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

        public void RemoveCellsByCondition(CellCondition condition)
        {
            ManipulateCellsByCondition(TileMap.RemoveMapSquareAtCell, condition);
        }

        public void ManipulateCellsByCondition(CustomAction action, CellCondition condition)
        {
            var cellsToManipulate =
                (from cell in TileMap.Map.MapData where condition(cell.Key) select cell.Key).ToList();
            cellsToManipulate.ForEach(cell => action(cell));
        }

        public void RemoveOuterWall()
        {
            for (var x = 0; x < _width; ++x)
            {
                TileMap.RemoveMapSquareAtCell(new Coords(x, 0));
                
            }
            for (var x = 0; x < _height; ++x)
            {
                TileMap.RemoveMapSquareAtCell(new Coords(0, x));
            }

            for (var x = 0; x < _width; ++x)
            {
                TileMap.RemoveMapSquareAtCell(new Coords(x, _height - 1));
            }
            for (var x = 0; x < _height; ++x)
            {
                TileMap.RemoveMapSquareAtCell(new Coords(_width - 1, x));
            }
        }

        #region Nested type: Digger

        private class Digger
        {
            private readonly RandomMapGenerator mapGen;
            private readonly Coords[] positions = new Coords[4];

            private readonly Random rand;

            public Digger(Coords position, RandomMapGenerator mapGen)
            {
                this.mapGen = mapGen;
                Position = position;
                rand = VariableProvider.RandomSeed;
                foreach (Coords cell in positions)
                {
                    TileMap.RemoveMapSquareAtCell(cell);
                }
            }

            public Digger(RandomMapGenerator mapGen)
                : this(new Coords(TileMap.MapWidth/2, TileMap.MapHeight/2), mapGen)
            {
            }

            public Coords Position
            {
                get { return positions[0]; }
                set
                {
                    positions[0] = value;
                    positions[1] = new Coords(value.X + 1, value.Y);
                    positions[2] = new Coords(value.X, value.Y + 1);
                    positions[3] = new Coords(value.X + 1, value.Y + 1);
                }
            }

            public bool LastManStanding { get; set; }

            public bool Dig()
            {
                List<Coords> diggableBlocks = getBlocksToDig();

                if (diggableBlocks.Count > 0)
                {
                    Position = diggableBlocks[rand.Next(0, diggableBlocks.Count)];
                    foreach (Coords cell in positions)
                    {
                        TileMap.RemoveMapSquareAtCell(cell);
                    }
                    if (rand.Next(1, 101) < 4)
                    {
                        LastManStanding = false;
                        mapGen.AddDigger(Position);
                    }
                    return true;
                }
                return false;
            }

            private List<Coords> getBlocksToDig()
            {
                var possibleBlocks = new List<Coords>();
                if (blockIsMineAble(positions[0] + Coords.Up))
                {
                    possibleBlocks.Add(positions[0] + Coords.Up);
                }
                if (blockIsMineAble(positions[1] + Coords.Up))
                {
                    possibleBlocks.Add(positions[0] + Coords.Up);
                }
                if (blockIsMineAble(positions[2] + Coords.Down))
                {
                    possibleBlocks.Add(positions[0] + Coords.Down);
                }
                if (blockIsMineAble(positions[3] + Coords.Down))
                {
                    possibleBlocks.Add(positions[0] + Coords.Down);
                }
                if (blockIsMineAble(positions[0] + Coords.Left))
                {
                    possibleBlocks.Add(positions[0] + Coords.Left);
                }
                if (blockIsMineAble(positions[2] + Coords.Left))
                {
                    possibleBlocks.Add(positions[0] + Coords.Left);
                }
                if (blockIsMineAble(positions[1] + Coords.Right))
                {
                    possibleBlocks.Add(positions[0] + Coords.Right);
                }
                if (blockIsMineAble(positions[3] + Coords.Right))
                {
                    possibleBlocks.Add(positions[0] + Coords.Right);
                }
                return possibleBlocks;
            }

            private bool blockIsMineAble(Coords coords)
            {
                if (LastManStanding)
                    return true;
                return !TileMap.CellIsPassable(coords);
            }
        }

        #endregion
    }
}