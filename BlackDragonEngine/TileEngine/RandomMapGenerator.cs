using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackDragonEngine.Providers;

namespace BlackDragonEngine.TileEngine
{
    public class RandomMapGenerator
    {
        private List<Digger> diggers = new List<Digger>();
        private Random rand = VariableProvider.RandomSeed;
        private int addedDiggers;
        public int Width;
        public int Height;

        public void GenerateNewMap(int minWidth, int minHeight, int maxWidth, int maxHeight)
        {
            TileMap.Map = new Map();
            Width = rand.Next(minWidth, maxWidth + 1);
            Height = rand.Next(minHeight, maxHeight + 1);

            for (int x = 0; x < Width; ++x)
            {
                for (int y = 0; y < Height; ++y)
                {
                    Coords coords = new Coords(x, y);
                    var square = new MapSquare(0, null, null, false);
                    TileMap.Map.MapData.Add(coords, square);
                }
            }      
                  
            AddDigger(new Coords(2, 2));
            do
            {
                for (int i = 0; i < diggers.Count; ++i)
                {
                    if (!diggers[i].Dig())
                    {
                        if (diggers.Count > 1)
                            diggers.Remove(diggers[i]);
                        else
                            diggers[i].LastManStanding = true;
                    }
                }                
            } while (addedDiggers < 100);
        }

        public void AddDigger(Coords position)
        {
            diggers.Add(new Digger(position, this));
            ++addedDiggers;
        }

        public void RemoveIslands()
        {
            var tilesToRemove = new List<Coords>();
            foreach (var cell in TileMap.Map.MapData)
            {
                if (CellOnlyHasTwoNeighbors(cell.Key))
                    tilesToRemove.Add(cell.Key);
            }
            foreach (var cell in tilesToRemove)
            {
                TileMap.RemoveMapSquareAtCell(cell);
            }
        }

        private bool CellOnlyHasTwoNeighbors(Coords cell)
        {
            int neighborCount = 0;
            for (int x = -1; x <= 1; ++x)
            {
                for (int y = -1; y <= 1; ++y)
                {
                    if (x == 0 && y == 0)
                        continue;
                    if (!TileMap.CellIsPassable(cell + new Coords(x, y)))
                        ++neighborCount;
                }
            }
            return neighborCount < 3;
        }

        public void FillNarrowPassages()
        {            
            var width = TileMap.MapWidth;
            var height = TileMap.MapHeight;
            var modified = false;
            do
            {
                modified = false;
                for (int x = 0; x < width; ++x)
                {
                    for (int y = 0; y < height; ++y)
                    {
                        var coords = new Coords(x, y);
                        if (!TileMap.CellIsPassable(coords))
                            continue;

                        if (CellIsNarrowedIn(coords))
                        {
                            TileMap.Map.MapData[coords] = new MapSquare(0, null, null, false);
                            modified = true;
                        }
                    }
                }
            } while (modified);
        }

        private bool CellIsNarrowedIn(Coords cell)
        { 
            for(int x = -1; x <= 1; ++x)
            {
                for (int y = 0; y <= 1; ++y)
                {
                    if (x == 0 && y == 0)
                        continue;
                    if (!TileMap.CellIsPassable(cell + new Coords(x, y)) && !TileMap.CellIsPassable(cell + new Coords(x * -1, y * -1)))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void MakeWallsHollow()
        {
            List<Coords> wallsToDelete = new List<Coords>();
            foreach (var cell in TileMap.Map.MapData)
            {
                if (CellSurroundedByBlocks(cell.Key))
                    wallsToDelete.Add(cell.Key);
            }

            foreach (var cell in wallsToDelete)
            {
                TileMap.Map.MapData.Remove(cell);
            }
        }

        public void CleanUpSingleBlocks()
        {
            List<Coords> wallsToDelete = new List<Coords>();
            foreach (var cell in TileMap.Map.MapData)
            {
                if (CellSurroundedByAir(cell.Key))
                    wallsToDelete.Add(cell.Key);
            }

            foreach (var cell in wallsToDelete)
            {
                TileMap.Map.MapData.Remove(cell);
            }
        }

        public void RemoveOuterWall()
        {
            for (int x = 0; x < Width; ++x)
            {
                TileMap.Map.MapData.Remove(new Coords(x, 0));
            }
            for (int x = 0; x < Height; ++x)
            {
                TileMap.Map.MapData.Remove(new Coords(0, x));
            }

            for (int x = 0; x < Width; ++x)
            {
                TileMap.Map.MapData.Remove(new Coords(x, Height - 1));
            }
            for (int x = 0; x < Height; ++x)
            {
                TileMap.Map.MapData.Remove(new Coords(Width - 1, x));
            }
        }

        private bool CellSurroundedByBlocks(Coords cell)
        {
            for (int i = -1; i <= 1; ++i)
            {
                for (int j = -1; j <= 1; ++j)
                {                   
                    if (TileMap.CellIsPassable(cell + new Coords(i, j)))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool CellSurroundedByAir(Coords cell)
        {
            for (int i = -1; i <= 1; ++i)
            {
                for (int j = -1; j <= 1; ++j)
                {
                    if (j == 0 && i == 0)
                        continue;
                    if (Math.Abs(j) == 1 && Math.Abs(i) == 1)
                        continue;
                    if (!TileMap.CellIsPassable(cell + new Coords(i, j)))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private class Digger
        {
            private Coords[] positions = new Coords[4];
            public Coords Position
            {
                get
                {
                    return positions[0];
                }
                set
                {
                    positions[0] = value;
                    positions[1] = new Coords(value.X + 1, value.Y);
                    positions[2] = new Coords(value.X, value.Y + 1);
                    positions[3] = new Coords(value.X + 1, value.Y + 1);
                    
                }
            }
            private Random rand;
            private RandomMapGenerator mapGen;
            public bool LastManStanding { get; set; }

            public Digger(Coords position, RandomMapGenerator mapGen)
            {
                this.mapGen = mapGen;
                Position = position;
                rand = VariableProvider.RandomSeed;
                foreach (var cell in positions)
                {
                    TileMap.RemoveMapSquareAtCell(cell);
                }
            }

            public Digger(RandomMapGenerator mapGen)
                : this(new Coords(TileMap.MapWidth / 2, TileMap.MapHeight / 2), mapGen)
            {
            }

            public bool Dig()
            {
                var diggableBlocks = getBlocksToDig();
               
                if(diggableBlocks.Count > 0)
                {
                    Position = diggableBlocks[rand.Next(0, diggableBlocks.Count)];
                    foreach (var cell in positions)
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
                if(blockIsMineAble(positions[0] + Coords.Up))
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
                if (coords.X == 1 || coords.Y == 1 || coords.X == mapGen.Width - 2 || coords.Y == mapGen.Height - 2)
                    return false;
                if (LastManStanding)
                    return true;
                return !TileMap.CellIsPassable(coords);
            }
        }
    }
}
