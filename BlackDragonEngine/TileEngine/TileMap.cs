using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Xml.Serialization;
using System.IO.Compression;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Providers;

namespace BlackDragonEngine.TileEngine
{   
    public static class TileMap
    {
        #region Declarations
        public const int TileWidth = 16;
        public const int TileHeight = 16;        
        public const int MapLayers = 3;        
        public const int TileOffset = 0;                  
        public static bool EditorMode = false;
        public static SpriteFont spriteFont;
        private static Texture2D tileSheet;

        public static Map Map;
        #endregion       

        #region Initialization
        public static void Initialize(Texture2D tileTexture) 
        {
            tileSheet = tileTexture;
            Map = new Map();
        }
        #endregion

        #region Tile and Tile Sheet Handling
        public static int TilesPerRow 
        {
            get { return tileSheet.Width / (TileWidth + TileOffset); }
        }

        public static Rectangle? TileSourceRectangle(int? tileIndex) 
        {
            if (tileIndex == null)
                return null;
            return new Rectangle(((int)tileIndex % TilesPerRow) * (TileWidth + TileOffset), ((int)tileIndex / TilesPerRow) * (TileHeight + TileOffset), TileWidth, TileHeight);
        }
        #endregion

        #region Information about Map Cells
        public static int GetCellByPixelX(int pixelX) 
        {
            return pixelX / TileWidth;
        }

        public static int GetCellByPixelY(int pixelY) 
        {
            return pixelY / TileHeight;
        }

        public static Vector2 GetCellByPixel(Vector2 pixelLocation)
        {
            return new Vector2(GetCellByPixelX((int)pixelLocation.X), GetCellByPixelY((int)pixelLocation.Y));
        }

        public static Vector2 GetCellCenter(int cellX, int cellY) 
        {
            return new Vector2((cellX * TileWidth) + (TileWidth / 2), (cellY * TileHeight) + (TileHeight / 2));
        }

        public static Vector2 GetCellCenter(Vector2 cell) 
        {
            return GetCellCenter((int)cell.X, (int)cell.Y);
        }

        public static Rectangle CellWorldRectangle(int cellX, int cellY)
        {
            return new Rectangle(cellX * TileWidth, cellY * TileHeight, TileWidth, TileHeight);
        }

        public static Rectangle CellWorldRectangle(Vector2 cell)
        {
            return CellWorldRectangle((int)cell.X, (int)cell.Y);
        }

        public static Rectangle CellScreenRectangle(int cellX, int cellY) 
        {
            return Camera.WorldToScreen(CellWorldRectangle(cellX, cellY));
        }

        public static Rectangle CellScreenRectangle(Vector2 cell)
        {
            return CellScreenRectangle((int)cell.X, (int)cell.Y);
        }

        public static bool CellIsPassable(int cellX, int cellY)
        {
            MapSquare square = GetMapSquareAtCell(cellX, cellY);
            if (square == null) return true;
            else return square.Passable;
        }

        public static bool CellIsPassable(Vector2 cell)
        {
            return CellIsPassable((int)cell.X, (int)cell.Y);
        }

        public static bool CellIsPassableByPixel(Vector2 pixelLocation) 
        { 
            return CellIsPassable(GetCellByPixelX((int)pixelLocation.X), GetCellByPixelY((int)pixelLocation.Y));
        }        

        public static List<string> GetCellCodes(int cellX, int cellY)
        {
            MapSquare square = GetMapSquareAtCell(cellX, cellY);
            if (square == null) return null;
            return square.Codes;
        }

        public static List<string> GetCellCodes(Vector2 cell)
        {
            return GetCellCodes((int)cell.X, (int)cell.Y);
        }

        public static void AddCodeToCell(int cellX, int cellY, string code)
        {
            MapSquare square = GetMapSquareAtCell(cellX, cellY);
            if (square != null)
            {
                if (!square.Codes.Contains(code))
                    square.Codes.Add(code);
            }
            else
            {
                square = new MapSquare(null, null, null, true);
                square.Codes.Add(code);
                SetMapSquareAtCell(cellX, cellY, square);
            }
        }

        public static void RemoveCodeFromCell(int cellX, int cellY, string code)
        {
            MapSquare square = GetMapSquareAtCell(cellX, cellY);
            if (square != null)
            {
                if (square.Codes.Contains(code))
                    square.Codes.Remove(code);
            }  
        }
        #endregion

        #region Information about MapSquare objects
        public static void RemoveMapSquareAtCell(int tileX, int tileY)
        {
            Map.MapData.Remove(new Coords(tileX, tileY));
        }

        public static MapSquare GetMapSquareAtCell(int tileX, int tileY)
        {
            if ((tileX >= 0) && (tileY >= 0)) 
            {
                return Map[tileX, tileY];                
            }
            else return null;
        }

        public static void SetMapSquareAtCell(int tileX, int tileY, MapSquare tile)
        {            
            if ((tileX >= 0) && (tileY >= 0))
            {
                Map[tileX, tileY] = tile;               
            }
        }

        public static void SetTileAtCell(int tileX, int tileY, int layer, int? tileIndex)
        {
            if ((tileX >= 0) && (tileY >= 0)) 
            {
                MapSquare square = GetMapSquareAtCell(tileX, tileY);
                if (square == null)
                {
                    square = new MapSquare(layer, tileIndex);
                    Map[tileX, tileY] = square;
                    return;
                }
                Map[tileX, tileY].LayerTiles[layer] = tileIndex;                
            }
        }

        public static MapSquare GetMapSquareAtPixel(int pixelX, int pixelY)
        {
            return GetMapSquareAtCell(GetCellByPixelX(pixelX), GetCellByPixelY(pixelY));
        }

        public static MapSquare GetMapSquareAtPixel(Vector2 pixelLocation) 
        {
            return GetMapSquareAtPixel((int)pixelLocation.X, (int)pixelLocation.Y);
        }
        #endregion

        #region Information about the Map

        public static int MapWidth { get { return Map.MapWidth; } }
        public static int MapHeight { get { return Map.MapHeight; } }   

        public static string GetMapProperty(string name)
        {
            if (Map.Properties.ContainsKey(name))
                return Map.Properties[name];
            else
                return null;
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
            int startX = GetCellByPixelX((int)Camera.Position.X);
            int endX = GetCellByPixelX((int)Camera.Position.X + Camera.ViewPortWidth);

            int startY = GetCellByPixelY((int)Camera.Position.Y);
            int endY = GetCellByPixelY((int)Camera.Position.Y + Camera.ViewPortHeight);

            foreach (var item in Map.MapData)
            {
                if (item.Key.X >= startX && item.Key.X <= endX && item.Key.Y >= startY && item.Key.Y <= endY)
                {
                    for (int z = 0; z < MapLayers; ++z)
                    {
                        if (TileSourceRectangle(Map[item.Key].LayerTiles[z]) != null)
                        {
                            spriteBatch.Draw(tileSheet, CellScreenRectangle(item.Key.X, item.Key.Y), TileSourceRectangle(Map[item.Key].LayerTiles[z]), Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 1f - ((float)z * 0.1f));
                        }
                    }
                    if (EditorMode)
                    {
                        DrawEditModeItems(spriteBatch, item.Key.X, item.Key.Y);
                    }
                }
            }  
        }

        public static void DrawEditModeItems(SpriteBatch spriteBatch, int x, int y)
        {            
            if (!CellIsPassable(x, y)) 
            {
                spriteBatch.Draw(VariableProvider.WhiteTexture, CellScreenRectangle(x, y), new Rectangle(0,0, TileWidth, TileHeight), new Color(255, 0, 0, 80), 0f, Vector2.Zero, SpriteEffects.None, 0.2f);
            }
            if (Map[x, y].Codes.Count != 0)
            {
                spriteBatch.Draw(VariableProvider.WhiteTexture, CellScreenRectangle(x, y), new Rectangle(0, 0, TileWidth, TileHeight), new Color(0, 0, 255, 80), 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                spriteBatch.DrawString(spriteFont, Map[x, y].Codes.Count.ToString(), Camera.WorldToScreen(new Vector2(x * TileWidth, y * TileHeight)), Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, .09f);                 
            }
        }

        

        public static void DrawRectangleIndicator(SpriteBatch spriteBatch, MouseState ms, Vector2 startCell)
        {
            if ((ms.X > 0) && (ms.Y > 0) && (ms.X < Camera.ViewPortWidth) && (ms.Y < Camera.ViewPortHeight))
            {
                Vector2 mouseLoc = Camera.ScreenToWorld(new Vector2(ms.X, ms.Y));
                int cellX = (int)MathHelper.Clamp(TileMap.GetCellByPixelX((int)mouseLoc.X), 0, TileMap.MapWidth - 1);
                int cellY = (int)MathHelper.Clamp(TileMap.GetCellByPixelY((int)mouseLoc.Y), 0, TileMap.MapHeight - 1);

                for (int cellx = (int)startCell.X; cellx <= cellX; ++cellx)
                {
                    for (int celly = (int)startCell.Y; celly <= cellY; ++celly)
                    {
                        spriteBatch.Draw(VariableProvider.WhiteTexture, CellScreenRectangle(cellx, celly), new Rectangle(0, 0, TileWidth, TileHeight), new Color(1, 1, 1, 80), 0f, Vector2.Zero, SpriteEffects.None, 0f);
                    }
                }
            }
        }
        #endregion

        #region Loading and Saving
        public static void SaveMap(FileStream fileStream) 
        {
            GZipStream gzs = new GZipStream(fileStream, CompressionMode.Compress);
            XmlSerializer xmlSer = new XmlSerializer(Map.GetType());  
            xmlSer.Serialize(gzs, Map);
            gzs.Close();
            fileStream.Close();
        }

        public static void LoadMap(FileStream fileStream)
        {
            try 
            {               
                GZipStream gzs = new GZipStream(fileStream, CompressionMode.Decompress);
                XmlSerializer xmlSer = new XmlSerializer(Map.GetType());                
                Map = (Map)xmlSer.Deserialize(gzs);
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
