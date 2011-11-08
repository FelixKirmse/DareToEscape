using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Providers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BlackDragonEngine.TileEngine
{
    public static class TileMap
    {
        #region Declarations

        public const int TileWidth = 16;
        public const int TileHeight = 16;
        private const int MapLayers = 1;
        public const int TileOffset = 0;
        public static bool EditorMode;
        public static SpriteFont SpriteFont;
        private static Texture2D _tileSheet;
        private static int _tilesPerRow;
        private static Rectangle[] _tileSourceRects;

        public static Map Map;

        #endregion

        #region Initialization

        public static void Initialize(Texture2D tileTexture)
        {
            _tileSheet = tileTexture;
            Map = new Map();
            _tilesPerRow = _tileSheet.Width / (TileWidth + TileOffset);
            var tileCount = (_tilesPerRow*_tileSheet.Height) / (TileHeight+TileOffset);
            _tileSourceRects = new Rectangle[tileCount];
            for(var i = 0; i < tileCount; ++i)
            {
                _tileSourceRects[i] = new Rectangle((i % _tilesPerRow) * (TileWidth + TileOffset),
                                 (i / _tilesPerRow) * (TileHeight + TileOffset), TileWidth, TileHeight);
            }
        }

        #endregion

        #region Tile and Tile Sheet Handling

        private static Rectangle? TileSourceRectangle(int? tileIndex)
        {
            if (tileIndex == null)
                return null;
            return _tileSourceRects[(int)tileIndex];
        }

        #endregion

        #region Information about Map Cells

        public static int GetCellByPixelX(int pixelX)
        {
            return pixelX/TileWidth;
        }

        public static int GetCellByPixelY(int pixelY)
        {
            return pixelY/TileHeight;
        }

        public static Vector2 GetCellByPixel(Vector2 pixelLocation)
        {
            return new Vector2(GetCellByPixelX((int) pixelLocation.X), GetCellByPixelY((int) pixelLocation.Y));
        }

        private static Vector2 GetCellCenter(int cellX, int cellY)
        {
            return new Vector2((cellX*TileWidth) + (TileWidth/2), (cellY*TileHeight) + (TileHeight/2));
        }

        public static Vector2 GetCellCenter(Vector2 cell)
        {
            return GetCellCenter((int) cell.X, (int) cell.Y);
        }

        private static Rectangle CellWorldRectangle(int cellX, int cellY)
        {
            return new Rectangle(cellX*TileWidth, cellY*TileHeight, TileWidth, TileHeight);
        }

        public static Rectangle CellWorldRectangle(Vector2 cell)
        {
            return CellWorldRectangle((int) cell.X, (int) cell.Y);
        }

        private static Rectangle CellScreenRectangle(int cellX, int cellY)
        {
            return Camera.WorldToScreen(CellWorldRectangle(cellX, cellY));
        }

        public static Rectangle CellScreenRectangle(Vector2 cell)
        {
            return CellScreenRectangle((int) cell.X, (int) cell.Y);
        }

        public static bool CellIsPassable(int cellX, int cellY)
        {
            var square = GetMapSquareAtCell(cellX, cellY);
            return square == null || square.Passable;
        }

        public static bool CellIsPassable(Vector2 cell)
        {
            return CellIsPassable((int) cell.X, (int) cell.Y);
        }

        public static bool CellIsPassable(Coords cell)
        {
            return CellIsPassable(cell.X, cell.Y);
        }

        public static bool CellIsPassableByPixel(Vector2 pixelLocation)
        {
            return CellIsPassable(GetCellByPixelX((int) pixelLocation.X), GetCellByPixelY((int) pixelLocation.Y));
        }

        public static List<string> GetCellCodes(int cellX, int cellY)
        {
            var coords = VariableProvider.CoordList[cellX, cellY];
            return !Map.Codes.ContainsKey(coords) ? new List<string>() : Map.Codes[coords];
        }

        public static void SetCellCodes(int cellX, int cellY, List<string> codes)
        {
            var coords = VariableProvider.CoordList[cellX, cellY];
            if (codes.Count == 0)
            {
                Map.Codes.Remove(coords);
                return;
            }
            if (Map.Codes.ContainsKey(coords))
                Map.Codes[coords] = codes;
            else
                Map.Codes.Add(coords, codes);
        }

        public static List<string> GetCellCodes(Vector2 cell)
        {
            return GetCellCodes((int) cell.X, (int) cell.Y);
        }

        public static List<string> GetCellCodes(Coords cell)
        {
            return GetCellCodes(cell.X, cell.Y);
        }

        public static void AddCodeToCell(int cellX, int cellY, string code)
        {
            AddCodeToCell(VariableProvider.CoordList[cellX, cellY], code);
        }

        public static void AddUniqueCodeToCell(Coords cell, string code)
        {
            if (!Map.Codes.ContainsKey(cell))
            {
                var codeList = new List<string> { code };
                Map.Codes.Add(cell, codeList);
            }
            else
            {
                if(!Map.Codes[cell].Contains(code))
                    Map.Codes[cell].Add(code);
            }
        }

        public static void AddCodeToCell(Coords cell, string code)
        {
            if (!Map.Codes.ContainsKey(cell))
            {
                var codeList = new List<string> {code};
                Map.Codes.Add(cell, codeList);
            }
            else
            {
                Map.Codes[cell].Add(code);
            }
        }

        public static void RemoveCodeFromCell(int cellX, int cellY, string code)
        {
            var coords = VariableProvider.CoordList[cellX, cellY];
            if (!Map.Codes.ContainsKey(coords)) return;
            Map.Codes[coords].Remove(code);
            if (Map.Codes[coords].Count != 0) return;
            Map.Codes.Remove(coords);
        }

        #endregion

        #region Information about MapSquare objects

        public static void RemoveEverythingAtCell(int tileX, int tileY)
        {
            RemoveEverythingAtCell(VariableProvider.CoordList[tileX, tileY]);
        }

        public static void RemoveEverythingAtCell(Coords coords)
        {
            Map.MapData.Remove(coords);
            Map.Codes.Remove(coords);
        }

        public static void RemoveMapSquareAtCell(Coords coords)
        {
            Map.MapData.Remove(coords);
        }

        public static void SetSolidTileAtCell(Coords coords)
        {
            Map[coords] = new MapSquare(0, null, null, false);
        }

        public static MapSquare GetMapSquareAtCell(int cellX, int cellY)
        {
            return Map[cellX, cellY];
        }

        public static void SetMapSquareAtCell(int tileX, int tileY, MapSquare tile)
        {
            Map[tileX, tileY] = tile;
        }

        public static void SetTileAtCell(int tileX, int tileY, int layer, int? tileIndex)
        {
            var square = GetMapSquareAtCell(tileX, tileY);
            if (square == null)
            {
                square = new MapSquare(layer, tileIndex);
                Map[tileX, tileY] = square;
                return;
            }
            Map[tileX, tileY].LayerTiles[layer] = tileIndex;
        }

        public static void SetSolidTileAtCoords(Coords coords, int? tileIndex)
        {
            SetTileAtCell(coords.X, coords.Y, 0, tileIndex);
            Map[coords].Passable = false;
        }

        private static MapSquare GetMapSquareAtPixel(int pixelX, int pixelY)
        {
            return GetMapSquareAtCell(GetCellByPixelX(pixelX), GetCellByPixelY(pixelY));
        }

        public static MapSquare GetMapSquareAtPixel(Vector2 pixelLocation)
        {
            return GetMapSquareAtPixel((int) pixelLocation.X, (int) pixelLocation.Y);
        }

        #endregion

        #region Information about the Map

        public static int MapWidth
        {
            get { return Map.MapWidth; }
        }

        public static int MapHeight
        {
            get { return Map.MapHeight; }
        }

        public static string GetMapProperty(string name)
        {
            return Map.Properties.ContainsKey(name) ? Map.Properties[name] : null;
        }

        public static void AddMapProperty(string name, string value)
        {
            Map.Properties.Add(name, value);
        }

        public static void RemoveMapProperty(string name)
        {
            Map.Properties.Remove(name);
        }

        #endregion

        #region Drawing

        public static void Draw(SpriteBatch spriteBatch)
        {
            var startX = GetCellByPixelX((int) Camera.Position.X) - 1;
            var endX = GetCellByPixelX((int) Camera.Position.X + Camera.ViewPortWidth);

            var startY = GetCellByPixelY((int) Camera.Position.Y) - 1;
            var endY = GetCellByPixelY((int) Camera.Position.Y + Camera.ViewPortHeight);

            foreach(var coords in Map.MapData.Keys)
            {
                if (coords.X < startX || coords.X > endX || coords.Y < startY || coords.Y > endY) continue;
                for (var z = 0; z < MapLayers; ++z)
                {
                    spriteBatch.Draw(_tileSheet, CellScreenRectangle(coords.X, coords.Y),
                                        TileSourceRectangle(Map[coords].LayerTiles[z]), Color.White, 0.0f,
                                        Vector2.Zero, SpriteEffects.None, 1f - (z * 0.1f));
                }
                if (EditorMode)
                {
                    DrawEditModeItems(spriteBatch, coords.X, coords.Y);
                }
            }
            if (!EditorMode) return;
            
            foreach (var cell in Map.Codes)
            {
                var coords = cell.Key;
                spriteBatch.Draw(VariableProvider.WhiteTexture, CellScreenRectangle(coords.X, coords.Y),
                                    new Rectangle(0, 0, TileWidth, TileHeight), new Color(0, 0, 255, 80), 0f,
                                    Vector2.Zero, SpriteEffects.None, 0.1f);
                spriteBatch.DrawString(SpriteFont, Map.Codes[coords].Count.ToString(),
                                        Camera.WorldToScreen(new Vector2(coords.X * TileWidth, coords.Y * TileHeight)),
                                        Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, .09f);
            }
            
        }

        private static void DrawEditModeItems(SpriteBatch spriteBatch, int x, int y)
        {
            if (CellIsPassable(x, y)) return;
            spriteBatch.Draw(VariableProvider.WhiteTexture, CellScreenRectangle(x, y),
                             new Rectangle(0, 0, TileWidth, TileHeight), new Color(255, 0, 0, 80), 0f, Vector2.Zero,
                             SpriteEffects.None, 0.2f);
        }


        public static void DrawRectangleIndicator(SpriteBatch spriteBatch, MouseState ms, Vector2 startCell)
        {
            if ((ms.X <= 0) || (ms.Y <= 0) || (ms.X >= Camera.ViewPortWidth) || (ms.Y >= Camera.ViewPortHeight)) return;
            var mouseLoc = Camera.ScreenToWorld(new Vector2(ms.X, ms.Y));
            var cellX = (int) MathHelper.Clamp(GetCellByPixelX((int) mouseLoc.X), 0, MapWidth - 1);
            var cellY = (int) MathHelper.Clamp(GetCellByPixelY((int) mouseLoc.Y), 0, MapHeight - 1);

            for (var cellx = (int) startCell.X; cellx <= cellX; ++cellx)
            {
                for (var celly = (int) startCell.Y; celly <= cellY; ++celly)
                {
                    spriteBatch.Draw(VariableProvider.WhiteTexture, CellScreenRectangle(cellx, celly),
                                     new Rectangle(0, 0, TileWidth, TileHeight), new Color(1, 1, 1, 80), 0f,
                                     Vector2.Zero, SpriteEffects.None, 0f);
                }
            }
        }

        #endregion

        #region Loading and Saving

        private const bool SerializeToXml = false;

        public static void SaveMap(FileStream fileStream)
        {
            var gzs = new GZipStream(fileStream, CompressionMode.Compress);
            if(SerializeToXml)
            {
                var xmlser = new XmlSerializer(Map.GetType());
                xmlser.Serialize(gzs, Map);
            }
            else
            {
                var bFormatter = new BinaryFormatter();
                bFormatter.Serialize(gzs, Map);
            }
            gzs.Close();
            fileStream.Close();
        }

        public static void LoadMap(FileStream fileStream)
        {
            try
            {
                var gzs = new GZipStream(fileStream, CompressionMode.Decompress);
                if(SerializeToXml)
                {
                    var xmlSer = new XmlSerializer(Map.GetType());
                    Map = (Map)xmlSer.Deserialize(gzs);
                }
                else
                {
                    var bFormatter = new BinaryFormatter();
                    Map = (Map)bFormatter.Deserialize(gzs);
                }
                gzs.Close();
                fileStream.Close();
            }
            catch
            {
                ClearMap();
                fileStream.Close();
            }
        }

        public static void ClearMap()
        {
            Map = new Map();
        }

        #endregion
    }
}