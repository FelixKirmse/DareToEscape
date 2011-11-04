using System;
using System.Threading.Tasks;
using BlackDragonEngine.Providers;
using BlackDragonEngine.TileEngine;
using DareToEscape.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DareToEscape.MapTools
{
    public delegate void MapGenerated();

    public static class MapGenerator
    {
        private static RandomMapGenerator _mapGen;
        private static int _mapSize;
        private static int _cellCounter;
        private static Task _task;

        private static GenerationState _state;
        public static event MapGenerated OnGenerationFinished;


        public static void Draw(SpriteBatch spriteBatch)
        {
            string drawString = null;
            switch (_state)
            {
                case GenerationState.Digging:
                    drawString = "Digging... " +
                                 Math.Round(((float) _mapGen.AddedDiggers/RandomMapGenerator.MaxDiggers)*100f) + "%";
                    break;

                case GenerationState.PlacingPlatforms:
                    drawString = "Placing platforms... " + Math.Round(((float) _cellCounter/_mapSize)*100f) + "%";
                    break;

                case GenerationState.Hollowing:
                    drawString = "Making walls hollow... " + Math.Round(((float) _cellCounter/_mapSize)*100f) + "%";
                    break;

                case GenerationState.WallRemoving:
                    drawString = "Removing outer walls...";
                    break;

                case GenerationState.SingleRemoving:
                    drawString = "Removing single blocks... " + Math.Round(((float) _cellCounter/_mapSize)*100f) + "%";
                    ;
                    break;
            }
            spriteBatch.DrawString(FontProvider.GetFont("Mono14"), drawString,
                                   ShortcutProvider.ScreenCenter - ShortcutProvider.GetFontCenter("Mono14", drawString),
                                   new Color(0, 255, 0));
        }

        public static void GenerateNewMap()
        {
            _mapGen = new RandomMapGenerator();
            _task = Task.Factory.StartNew(() =>
                                              {
                                                  _cellCounter = 0;
                                                  GameStates previousState = StateManager.GameState;
                                                  StateManager.GameState = GameStates.GeneratingMap;
                                                  _task.Wait(32);
                                                  _state = GenerationState.Digging;
                                                  _mapGen.GenerateNewMap(50, 50, 500, 500);
                                                  _state = GenerationState.PlacingPlatforms;
                                                  _mapSize = TileMap.MapWidth*TileMap.MapHeight;
                                                  _mapGen.ManipulateCellsByCondition(PlacePlatform,
                                                                                     CellHasNeighborToLeftOrRight);
                                                  _state = GenerationState.Hollowing;
                                                  _cellCounter = 0;
                                                  _mapSize = TileMap.MapWidth*TileMap.MapHeight;
                                                  _mapGen.RemoveCellsByCondition(CellSurroundedByBlocks);
                                                  _state = GenerationState.WallRemoving;
                                                  _mapGen.RemoveOuterWall();
                                                  _state = GenerationState.SingleRemoving;
                                                  _mapSize = TileMap.MapWidth*TileMap.MapHeight*2;
                                                  _cellCounter = 0;
                                                  _mapGen.RemoveCellsByCondition(CellOnlyHasOneNeighbor);
                                                  _mapGen.RemoveCellsByCondition(CellSurroundedByAir);
                                                  OnGenerationFinished();
                                                  StateManager.GameState = previousState;
                                              });
        }

        private static void PlacePlatform(Coords cell)
        {
            TileMap.RemoveMapSquareAtCell(cell);
            TileMap.AddCodeToCell(cell, "JUMPTHROUGH");
            TileMap.AddCodeToCell(new Coords(cell.X, cell.Y - 1), "JUMPTHROUGHTOP");
            TileMap.SetTileAtCell(cell.X, cell.Y, 0, 1);
        }


        private static bool CellHasNeighborToLeftOrRight(Coords cell)
        {
            ++_cellCounter;
            int neighborCount = 0;
            for (int x = -1; x <= 1; ++x)
            {
                for (int y = -1; y <= 1; ++y)
                {
                    if (x == 0 && y == 0)
                        continue;
                    if (y == 0 && !TileMap.CellIsPassable(cell + new Coords(x, y)))
                        ++neighborCount;
                    if (y != 0 && x == 0 && !TileMap.CellIsPassable(cell + new Coords(x, y)))
                        return false;
                }
            }
            return neighborCount > 0;
        }

        private static bool CellOnlyHasOneNeighbor(Coords cell)
        {
            ++_cellCounter;
            if (TileMap.CellIsPassable(cell))
                return false;
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
            return neighborCount == 1;
        }

        private static bool CellSurroundedByBlocks(Coords cell)
        {
            ++_cellCounter;
            if (TileMap.CellIsPassable(cell))
                return false;
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

        private static bool CellSurroundedByAir(Coords cell)
        {
            ++_cellCounter;
            if (TileMap.CellIsPassable(cell))
                return false;
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

        #region Nested type: GenerationState

        private enum GenerationState
        {
            Digging,
            Hollowing,
            WallRemoving,
            SingleRemoving,
            PlacingPlatforms
        }

        #endregion
    }
}