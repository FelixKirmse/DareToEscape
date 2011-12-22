using System;
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
    /// <summary>
    ///   The heart of the TileEngine, manages a Map object.
    ///   This class implements a Pseudo-Singleton pattern.
    ///   The first instance has to be created via the public constructor. 
    ///   However, all instances you need afterwards have to be fetched via the static GetInstance() method.
    /// </summary>
    /// <typeparam name = "TMap">Type of the Map Object</typeparam>
    /// <typeparam name = "TCodes">Type of the Codes in the Map Object</typeparam>
    public class TileMap<TMap, TCodes> where TMap : IMap<TCodes>, new()
    {
        #region Declarations

        private const int MapLayers = 1;
        private readonly SpriteFont _spriteFont;
        private readonly int _tileOffset;
        private readonly Texture2D _tileSheet;
        private readonly Rectangle[] _tileSourceRects;
        private readonly int _tilesPerRow;

        public TMap Map { get; internal set; }
        public int TileWidth { get; private set; }
        public int TileHeight { get; private set; }
        public bool EditorMode { get; set; }

        #endregion

        private static TileMap<TMap, TCodes> _instance;

        public static TileMap<TMap, TCodes> GetInstance()
        {
            if (_instance == null)
                throw new Exception("There's no instance available, make sure you call the public constructor once!");
            return _instance;
        }

        #region Constructor

        public TileMap(int tileWidth, int tileHeight, int tileOffset, SpriteFont spriteFont, Texture2D tileTexture)
        {
            if (_instance != null)
                throw new Exception(
                    "An Instance of this class was already created elsewhere, use GetInstance to get it!");

            _tileSheet = tileTexture;
            _spriteFont = spriteFont;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            _tileOffset = tileOffset;

            Map = new TMap();
            _tilesPerRow = _tileSheet.Width/(TileWidth + _tileOffset);
            int tileCount = (_tilesPerRow*_tileSheet.Height)/(TileHeight + _tileOffset);
            _tileSourceRects = new Rectangle[tileCount];
            for (int i = 0; i < tileCount; ++i)
            {
                _tileSourceRects[i] = new Rectangle((i%_tilesPerRow)*(TileWidth + _tileOffset),
                                                    (i/_tilesPerRow)*(TileHeight + _tileOffset), TileWidth, TileHeight);
            }
            VariableProvider.CoordList = new CoordList();
            _instance = this;
        }

        #endregion

        #region Tile and Tile Sheet Handling

        private Rectangle? TileSourceRectangle(int? tileIndex)
        {
            if (tileIndex == null)
                return null;
            return _tileSourceRects[(int) tileIndex];
        }

        #endregion

        #region Information about Map Cells

        public int GetCellByPixelX(int pixelX)
        {
            int cell = pixelX/TileWidth;
            return pixelX < 0 ? cell - 1 : cell;
        }

        public int GetCellByPixelY(int pixelY)
        {
            int cell = pixelY/TileWidth;
            return pixelY < 0 ? cell - 1 : cell;
        }

        public Vector2 GetCellByPixel(Vector2 pixelLocation)
        {
            return new Vector2(GetCellByPixelX((int) pixelLocation.X), GetCellByPixelY((int) pixelLocation.Y));
        }

        private Vector2 GetCellCenter(int cellX, int cellY)
        {
            return new Vector2((cellX*TileWidth) + (TileWidth/2), (cellY*TileHeight) + (TileHeight/2));
        }

        public Vector2 GetCellCenter(Vector2 cell)
        {
            return GetCellCenter((int) cell.X, (int) cell.Y);
        }

        private Rectangle CellWorldRectangle(int cellX, int cellY)
        {
            return new Rectangle(cellX*TileWidth, cellY*TileHeight, TileWidth, TileHeight);
        }

        public Rectangle CellWorldRectangle(Vector2 cell)
        {
            return CellWorldRectangle((int) cell.X, (int) cell.Y);
        }

        private Rectangle CellScreenRectangle(int cellX, int cellY)
        {
            return Camera.WorldToScreen(CellWorldRectangle(cellX, cellY));
        }

        public Rectangle CellScreenRectangle(Vector2 cell)
        {
            return CellScreenRectangle((int) cell.X, (int) cell.Y);
        }

        public bool CellIsPassable(int cellX, int cellY)
        {
            MapSquare square = GetMapSquareAtCell(cellX, cellY);
            return square.InValidSquare || square.Passable;
        }

        public bool CellIsPassable(Vector2 cell)
        {
            return CellIsPassable((int) cell.X, (int) cell.Y);
        }

        public bool CellIsPassable(Coords cell)
        {
            return CellIsPassable(cell.X, cell.Y);
        }

        public bool CellIsPassableByPixel(Vector2 pixelLocation)
        {
            return CellIsPassable(GetCellByPixelX((int) pixelLocation.X), GetCellByPixelY((int) pixelLocation.Y));
        }

        public List<TCodes> GetCellCodes(int cellX, int cellY)
        {
            Coords coords = VariableProvider.CoordList[cellX, cellY];
            return !Map.Codes.ContainsKey(coords) ? new List<TCodes>() : Map.Codes[coords];
        }

        public void SetCellCodes(int cellX, int cellY, List<TCodes> codes)
        {
            Coords coords = VariableProvider.CoordList[cellX, cellY];
            if (codes == null || codes.Count == 0)
            {
                Map.Codes.Remove(coords);
                return;
            }
            if (Map.Codes.ContainsKey(coords))
                Map.Codes[coords] = codes;
            else
                Map.Codes.Add(coords, codes);
        }

        public List<TCodes> GetCellCodes(Vector2 cell)
        {
            return GetCellCodes((int) cell.X, (int) cell.Y);
        }

        public List<TCodes> GetCellCodes(Coords cell)
        {
            return GetCellCodes(cell.X, cell.Y);
        }

        public void AddCodeToCell(int cellX, int cellY, TCodes code)
        {
            AddCodeToCell(VariableProvider.CoordList[cellX, cellY], code);
        }

        public void AddUniqueCodeToCell(Coords cell, TCodes code)
        {
            if (!Map.Codes.ContainsKey(cell))
            {
                var codeList = new List<TCodes> {code};
                Map.Codes.Add(cell, codeList);
            }
            else
            {
                if (!Map.Codes[cell].Contains(code))
                    Map.Codes[cell].Add(code);
            }
        }

        public void AddCodeToCell(Coords cell, TCodes code)
        {
            if (!Map.Codes.ContainsKey(cell))
            {
                var codeList = new List<TCodes> {code};
                Map.Codes.Add(cell, codeList);
            }
            else
            {
                Map.Codes[cell].Add(code);
            }
        }

        public void RemoveCodeFromCell(int cellX, int cellY, TCodes code)
        {
            Coords coords = VariableProvider.CoordList[cellX, cellY];
            if (!Map.Codes.ContainsKey(coords)) return;
            Map.Codes[coords].Remove(code);
            if (Map.Codes[coords].Count != 0) return;
            Map.Codes.Remove(coords);
        }

        #endregion

        #region Information about MapSquare objects

        public void RemoveEverythingAtCell(int tileX, int tileY)
        {
            RemoveEverythingAtCell(VariableProvider.CoordList[tileX, tileY]);
        }

        public void RemoveEverythingAtCell(Coords coords)
        {
            Map.MapData.Remove(coords);
            Map.Codes.Remove(coords);
        }

        public void RemoveMapSquareAtCell(Coords coords)
        {
            Map.MapData.Remove(coords);
        }

        public void SetSolidTileAtCell(Coords coords)
        {
            Map[coords] = new MapSquare(0, false);
        }

        public MapSquare GetMapSquareAtCell(int cellX, int cellY)
        {
            return Map[cellX, cellY];
        }

        public MapSquare GetMapSquareAtCell(Vector2 cell)
        {
            return Map[(int) cell.X, (int) cell.Y];
        }

        public void SetMapSquareAtCell(Vector2 cell, MapSquare tile)
        {
            SetMapSquareAtCell((int) cell.X, (int) cell.Y, tile);
        }

        public void SetMapSquareAtCell(int tileX, int tileY, MapSquare tile)
        {
            Map[tileX, tileY] = tile;
        }

        public void SetPassabilityAtCell(Vector2 cell, bool passable)
        {
            MapSquare square = GetMapSquareAtCell(cell);
            square.Passable = passable;
            SetMapSquareAtCell(cell, square);
        }

        public void SetPassabilityAtCell(int x, int y, bool passable)
        {
            SetPassabilityAtCell(new Vector2(x, y), passable);
        }

        public void SetPassabilityAtCell(Coords coords, bool passable)
        {
            SetPassabilityAtCell(new Vector2(coords.X, coords.Y), passable);
        }

        public void SetTileAtCell(int tileX, int tileY, int layer, int tileIndex)
        {
            MapSquare square = GetMapSquareAtCell(tileX, tileY);
            if (square.InValidSquare)
            {
                var newSquare = new MapSquare(layer, tileIndex);
                Map[tileX, tileY] = newSquare;
                return;
            }
            Map[tileX, tileY].LayerTiles[layer] = tileIndex;
        }

        public void SetTileAtCell(Vector2 cell, int tileIndex)
        {
            SetTileAtCell((int)cell.X, (int)cell.Y, 0, tileIndex);
        }

        public void SetSolidTileAtCoords(Coords coords, int tileIndex)
        {
            SetTileAtCell(coords.X, coords.Y, 0, tileIndex);
            MapSquare square = Map[coords];
            square.Passable = false;
            Map[coords] = square;
        }

        private MapSquare GetMapSquareAtPixel(int pixelX, int pixelY)
        {
            return GetMapSquareAtCell(GetCellByPixelX(pixelX), GetCellByPixelY(pixelY));
        }

        public MapSquare GetMapSquareAtPixel(Vector2 pixelLocation)
        {
            return GetMapSquareAtPixel((int) pixelLocation.X, (int) pixelLocation.Y);
        }

        #endregion

        #region Information about the Map

        public int MapWidth
        {
            get { return Map.MapWidth; }
        }

        public int MapHeight
        {
            get { return Map.MapHeight; }
        }

        #endregion

        #region Drawing

        public void Draw(SpriteBatch spriteBatch)
        {
            int startX = GetCellByPixelX((int) Camera.Position.X) - 1;
            int endX = GetCellByPixelX((int) Camera.Position.X + Camera.ViewPortWidth);

            int startY = GetCellByPixelY((int) Camera.Position.Y) - 1;
            int endY = GetCellByPixelY((int) Camera.Position.Y + Camera.ViewPortHeight);

            foreach (var coords in Map.MapData.Keys)
            {
                if (coords.X < startX || coords.X > endX || coords.Y < startY || coords.Y > endY) continue;
                for (int z = 0; z < MapLayers; ++z)
                {
                    spriteBatch.Draw(_tileSheet, CellScreenRectangle(coords.X, coords.Y),
                                     TileSourceRectangle(Map[coords].LayerTiles[z]), Color.White, 0.0f,
                                     Vector2.Zero, SpriteEffects.None, 1f - (z*0.1f));
                }
                if (EditorMode)
                {
                    DrawEditModeItems(spriteBatch, coords.X, coords.Y);
                }
            }
            if (!EditorMode) return;

            foreach (var cell in Map.Codes)
            {
                Coords coords = cell.Key;
                spriteBatch.Draw(VariableProvider.WhiteTexture, CellScreenRectangle(coords.X, coords.Y),
                                 new Rectangle(0, 0, TileWidth, TileHeight), new Color(0, 0, 255, 80), 0f,
                                 Vector2.Zero, SpriteEffects.None, 0.1f);
                spriteBatch.DrawString(_spriteFont, Map.Codes[coords].Count.ToString(),
                                       Camera.WorldToScreen(new Vector2(coords.X*TileWidth, coords.Y*TileHeight)),
                                       Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, .09f);
            }
        }

        private void DrawEditModeItems(SpriteBatch spriteBatch, int x, int y)
        {
            if (CellIsPassable(x, y)) return;
            spriteBatch.Draw(VariableProvider.WhiteTexture, CellScreenRectangle(x, y),
                             new Rectangle(0, 0, TileWidth, TileHeight), new Color(255, 0, 0, 80), 0f, Vector2.Zero,
                             SpriteEffects.None, 0.2f);
        }


        public void DrawRectangleIndicator(SpriteBatch spriteBatch, MouseState ms, Vector2 startCell)
        {
            if ((ms.X <= 0) || (ms.Y <= 0) || (ms.X >= Camera.ViewPortWidth) || (ms.Y >= Camera.ViewPortHeight)) return;
            Vector2 mouseLoc = Camera.ScreenToWorld(new Vector2(ms.X, ms.Y));
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

        public void SaveMap(FileStream fileStream)
        {
            var gzs = new GZipStream(fileStream, CompressionMode.Compress);
            if (SerializeToXml)
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

        public void LoadMap(FileStream fileStream)
        {
            try
            {
                var gzs = new GZipStream(fileStream, CompressionMode.Decompress);
                if (SerializeToXml)
                {
                    var xmlSer = new XmlSerializer(Map.GetType());
                    Map = (TMap) xmlSer.Deserialize(gzs);
                }
                else
                {
                    var bFormatter = new BinaryFormatter();
                    Map = (TMap) bFormatter.Deserialize(gzs);
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

        public void ClearMap()
        {
            Map = new TMap();
        }

        #endregion
    }
}