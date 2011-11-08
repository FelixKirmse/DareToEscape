﻿using System;
using System.Collections.Generic;
using System.Linq;
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

                case GenerationState.SingleRemoving:
                    drawString = "Removing single blocks... " + Math.Round(((float) _cellCounter/_mapSize)*100f) + "%";
                    break;

                case GenerationState.Inverting:
                    drawString = "Inverting Map (2 Pass)... " + Math.Round(((float)_mapGen.ProgressCounter/_mapGen.ProgressMax) * 100f) + "%";
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
                                                  var previousState = StateManager.GameState;
                                                  StateManager.GameState = GameStates.GeneratingMap;
                                                  _task.Wait(32);
                                                  _state = GenerationState.Digging;
                                                  _mapGen.GenerateNewMap(200);
                                                  _state = GenerationState.Inverting;
                                                  _mapGen.InvertMap();  
                                                  _state = GenerationState.PlacingPlatforms;
                                                  _mapSize = TileMap.MapWidth * TileMap.MapHeight;
                                                  PlacePlatforms();
                                                  /*_mapGen.ManipulateCellsByCondition(PlacePlatform,
                                                                                     CellHasNeighborToLeftOrRight);*/
                                                  _state = GenerationState.SingleRemoving;
                                                  _mapSize = TileMap.MapWidth*TileMap.MapHeight*2;
                                                  _cellCounter = 0;
                                                  _mapGen.RemoveCellsByCondition(CellSurroundedByAir);
                                                  _mapGen.RemoveCellsByCondition(CellOnlyHasOneNeighbor);
                                                  RemoveMapgenCodes();
                                                  OnGenerationFinished();
                                                  StateManager.GameState = previousState;
                                              });
        }

        private static void RemoveMapgenCodes()
        {
            foreach(var cell in TileMap.Map.MapData.Keys)
            {
                while (TileMap.Map.Codes[cell].Remove("AddedByInvert")) continue;
            }
            foreach(var cell in TileMap.Map.MapData.Keys)
            {
                if (TileMap.Map.Codes[cell].Count == 0)
                    TileMap.Map.Codes.Remove(cell);
            }
        }

        private static void PlacePlatforms()
        {
            var cellsToChange = (from cell in TileMap.Map.Codes where cell.Value.Contains("AddedByInvert") && cell.Value.Count > 5 && cell.Value.Count < 8 && CellHasNeighborToLeftOrRight(cell.Key) select cell.Key).ToList();
            cellsToChange.ForEach(PlacePlatform);
        }

        private static void PlacePlatform(Coords cell)
        {
            TileMap.AddCodeToCell(cell, "JUMPTHROUGH");
            TileMap.AddCodeToCell(new Coords(cell.X, cell.Y - 1), "JUMPTHROUGHTOP");
            TileMap.SetTileAtCell(cell.X, cell.Y, 0, 1);
            TileMap.Map[cell].Passable = true;
        }


        private static bool CellHasNeighborToLeftOrRight(Coords cell)
        {
            ++_cellCounter;
            var neighborCount = 0;
            for (var x = -1; x <= 1; ++x)
            {
                for (var y = -1; y <= 1; ++y)
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
            var neighborCount = 0;
            neighborCount += !TileMap.CellIsPassable(cell.UpLeft) ? 1 : 0;
            neighborCount += !TileMap.CellIsPassable(cell.UpRight) ? 1 : 0;
            neighborCount += !TileMap.CellIsPassable(cell.DownLeft) ? 1 : 0;
            neighborCount += !TileMap.CellIsPassable(cell.DownRight) ? 1 : 0;
            neighborCount += !TileMap.CellIsPassable(cell.Up) ? -4 : 0;
            neighborCount += !TileMap.CellIsPassable(cell.Down) ? -4 : 0;
            neighborCount += !TileMap.CellIsPassable(cell.Left) ? -4 : 0;
            neighborCount += !TileMap.CellIsPassable(cell.Right) ? -4 : 0;
            return neighborCount == 1;
        }

        private static bool CellSurroundedByAir(Coords cell)
        {
            return TileMap.Map.Codes[cell].Count == 8 && !TileMap.Map.Codes[cell].Contains("JUMPTHROUGH");
        }

        #region Nested type: GenerationState

        private enum GenerationState
        {
            Digging,
            Hollowing,
            SingleRemoving,
            PlacingPlatforms,
            Inverting
        }

        #endregion
    }
}