using System;
using Microsoft.Xna.Framework.Graphics;
using BlackDragonEngine.TileEngine;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Providers;
using DareToEscape.Managers;
using BlackDragonEngine.Managers;
using Microsoft.Xna.Framework.Input;
using DareToEscape.Helpers;

namespace DareToEscape.Editor
{
    static class EditorManager
    {
        public static int DrawLayer = 0;
        public static int DrawTile = 0;
        public static bool SettingCode = false;
        public static bool MakeUnpassable = false;
        public static bool MakePassable = true;
        public static bool GettingCode = false;
        public static bool InsertTile = false;
        public static Vector2 CellCoords = Vector2.Zero;
        public static string FillMode = "TILEFILL";
        public static bool WaitingForSecondClick = false;
        private static Vector2 startCell;
        public static bool RemoveTile = false;
        public static EditorItem CurrentItem;
        public static bool SmartInsert = true;
        public static bool PlayLevel = false;

        #region Doing The Impossible
        private static IntPtr drawSurface;
        private static Form parentForm;
        private static PictureBox pictureBox;
        private static Control gameForm;        
        private static MapEditor editorForm;
        private static PresentationParameters orgPParams;
        private static DareToEscape game;
        private static EventHandler gameFormVisibleChanged;
        private static EventHandler pictureBoxSizeChanged;
        private static EventHandler<PreparingDeviceSettingsEventArgs> preparingDeviceSettingsHandler;
        private static EventHandler<PreparingDeviceSettingsEventArgs> resetDeviceSettingsHandler;

        public static void Initialize()
        {
            gameFormVisibleChanged = new EventHandler(gameForm_VisibleChanged);
            pictureBoxSizeChanged = new EventHandler(pictureBox_SizeChanged);
            preparingDeviceSettingsHandler = new EventHandler<PreparingDeviceSettingsEventArgs>(graphics_PreparingDeviceSettings);
            resetDeviceSettingsHandler = new EventHandler<PreparingDeviceSettingsEventArgs>(graphics_resetDeviceSettings);
            orgPParams = DareToEscape.Graphics.GraphicsDevice.PresentationParameters.Clone();
            DareToEscape.Graphics.PreparingDeviceSettings += resetDeviceSettingsHandler;            
        }        

        public static void Activate(string levelname)
        {
            editorForm = new MapEditor(levelname);
            Activate();
        }

        public static void Activate()
        {            
            StateManager.GameState = GameStates.Editor;
            VariableProvider.CurrentPlayer = Factory.CreatePlayer();
            EntityManager.SetPlayer();
            if (editorForm == null)
                editorForm = new MapEditor();
            editorForm.Show();
            drawSurface = editorForm.pctSurface.Handle;
            parentForm = editorForm;
            pictureBox = editorForm.pctSurface;
            game = (DareToEscape)VariableProvider.Game;
            editorForm.Game = game;
            
            DareToEscape.Graphics.PreparingDeviceSettings -= resetDeviceSettingsHandler;
            DareToEscape.Graphics.PreparingDeviceSettings += preparingDeviceSettingsHandler;
            DareToEscape.Graphics.GraphicsDevice.Reset();
            DareToEscape.Graphics.ApplyChanges();

            gameForm = Control.FromHandle(game.Window.Handle);
            gameForm.VisibleChanged += gameFormVisibleChanged;
            pictureBox.SizeChanged += pictureBoxSizeChanged;
            gameForm.Visible = false;

            Camera.ViewPortWidth = pictureBox.Width;
            Camera.ViewPortHeight = pictureBox.Height;
            Camera.UpdateWorldRectangle();
            TileMap.spriteFont = FontProvider.GetFont("Mono8");
            pictureBox_SizeChanged(null, null);
        }

        private static void graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            e.GraphicsDeviceInformation.PresentationParameters.DeviceWindowHandle = drawSurface;
            Mouse.WindowHandle = drawSurface;
        }

        private static void graphics_resetDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            e.GraphicsDeviceInformation.PresentationParameters = orgPParams.Clone();
            Mouse.WindowHandle = orgPParams.Clone().DeviceWindowHandle;
        }

        public static void Deactivate()
        {
            gameForm.VisibleChanged -= gameFormVisibleChanged;
            pictureBox.SizeChanged -= pictureBoxSizeChanged;
            DareToEscape.Graphics.PreparingDeviceSettings -= preparingDeviceSettingsHandler;
            DareToEscape.Graphics.PreparingDeviceSettings += resetDeviceSettingsHandler;
            DareToEscape.Graphics.GraphicsDevice.Reset();
            DareToEscape.Graphics.PreferredBackBufferWidth = 800;
            DareToEscape.Graphics.PreferredBackBufferHeight = 600;
            Camera.ViewPortWidth = 800;
            Camera.ViewPortHeight = 600;
            DareToEscape.Graphics.ApplyChanges();
            StateManager.GameState = GameStates.Menu;            
            editorForm.Hide();
            gameForm.Visible = true;
            TileMap.EditorMode = false;
        }

        public static void JumpToLevel(string levelName)
        {
            Deactivate();
            StateManager.GameState = GameStates.Ingame;
            IngameManager.Activate();
            LevelManager.LoadLevel(levelName);
        }

        private static void gameForm_VisibleChanged(object sender, EventArgs e)
        {
            if (gameForm.Visible) gameForm.Visible = false;
        }

        private static void pictureBox_SizeChanged(object sender, EventArgs e)
        {
            if (parentForm.WindowState != FormWindowState.Minimized)
            {
                DareToEscape.Graphics.PreferredBackBufferWidth = pictureBox.Width;
                DareToEscape.Graphics.PreferredBackBufferHeight = pictureBox.Height;
                Camera.ViewPortWidth = pictureBox.Width;
                Camera.ViewPortHeight = pictureBox.Height;
                DareToEscape.Graphics.ApplyChanges();
            }
        }

        #endregion

        #region Updating and Drawing
        public static void Update()
        {
            if (Form.ActiveForm == parentForm)
            {     
                MouseState ms = InputProvider.MouseState;
                if (!PlayLevel)
                {
                    int mod = 1; ;
                    if(ShortcutProvider.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftShift))
                    {
                        mod = 2;
                    }
                    if (InputMapper.Up)
                    {
                        Camera.ForcePosition -= new Vector2(0,5) * mod;
                    }
                    if (InputMapper.Down)
                    {
                        Camera.ForcePosition += new Vector2(0, 5) * mod;
                    }
                    if (InputMapper.Left)
                    {
                        Camera.ForcePosition -= new Vector2(5, 0) * mod;
                    }
                    if (InputMapper.Right)
                    {
                        Camera.ForcePosition += new Vector2(5, 0) * mod;
                    }                                        

                    if ((ms.X > 0) && (ms.Y > 0) && (ms.X < Camera.ViewPortWidth) && (ms.Y < Camera.ViewPortHeight))
                    {
                        Vector2 mouseLoc = Camera.ScreenToWorld(new Vector2(ms.X, ms.Y));
                        int cellX = (int)MathHelper.Max(TileMap.GetCellByPixelX((int)mouseLoc.X), 0);
                        int cellY = (int)MathHelper.Max(TileMap.GetCellByPixelY((int)mouseLoc.Y), 0);
                        
                        if (FillMode == "TILEFILL")
                        {
                            if (ShortcutProvider.LeftButtonClicked())
                            {
                                if (SmartInsert)
                                    InsertEditorItem(cellX, cellY);
                                else if (!RemoveTile)
                                    TileMap.SetTileAtCell(cellX, cellY, DrawLayer, DrawTile);

                                if(RemoveTile)
                                {
                                    TileMap.RemoveMapSquareAtCell(cellX, cellY);
                                }                                        
                            }

                            if (ShortcutProvider.RightButtonClicked())
                            {
                                if (SettingCode)
                                {
                                    ((MapEditor)parentForm).SetCodeList(cellX, cellY);
                                }
                                else if (MakePassable)
                                {
                                    TileMap.GetMapSquareAtCell(cellX, cellY).Passable = true;
                                }
                                else if (MakeUnpassable)
                                {
                                    TileMap.GetMapSquareAtCell(cellX, cellY).Passable = false;
                                }
                                else if (GettingCode)
                                {
                                    ((MapEditor)parentForm).GetCodeList(TileMap.GetCellCodes(cellX, cellY));
                                }
                                if (InsertTile)
                                {
                                    TileMap.SetTileAtCell(cellX, cellY, DrawLayer, DrawTile);
                                }
                            }
                        }
                        else if (FillMode == "RECTANGLEFILL")
                        {
                            if (ShortcutProvider.LeftButtonClickedNowButNotLastFrame())
                            {
                                if (!WaitingForSecondClick)
                                {
                                    startCell = new Vector2(cellX, cellY);
                                    WaitingForSecondClick = true;
                                }
                                else
                                {
                                    Vector2 endCell = new Vector2(cellX, cellY);
                                    WaitingForSecondClick = false;

                                    for (int cellx = (int)startCell.X; cellx <= endCell.X; ++cellx)
                                    {
                                        for (int celly = (int)startCell.Y; celly <= endCell.Y; ++celly)
                                        {
                                            if (SmartInsert)
                                                InsertEditorItem(cellx, celly);
                                            else if (!RemoveTile)
                                                TileMap.SetTileAtCell(cellx, celly, DrawLayer, DrawTile);

                                            if (RemoveTile)
                                            {
                                                TileMap.RemoveMapSquareAtCell(cellx, celly);
                                            }
                                        }
                                    }
                                }
                            }
                            if (ShortcutProvider.RightButtonClickedButNotLastFrame())
                            {
                                if (!WaitingForSecondClick)
                                {
                                    startCell = new Vector2(cellX, cellY);
                                    WaitingForSecondClick = true;
                                }
                                else
                                {
                                    Vector2 endCell = new Vector2(cellX, cellY);
                                    WaitingForSecondClick = false;

                                    for (int cellx = (int)startCell.X; cellx <= endCell.X; ++cellx)
                                    {
                                        for (int celly = (int)startCell.Y; celly <= endCell.Y; ++celly)
                                        {
                                            if (SettingCode)
                                            {
                                                ((MapEditor)parentForm).SetCodeList(cellx, celly);
                                            }
                                            else if (MakePassable)
                                            {
                                                TileMap.GetMapSquareAtCell(cellx, celly).Passable = true;
                                            }
                                            else if (MakeUnpassable)
                                            {
                                                TileMap.GetMapSquareAtCell(cellx, celly).Passable = false;
                                            }
                                            if (InsertTile)
                                            {
                                                TileMap.SetTileAtCell(cellx, celly, DrawLayer, DrawTile);
                                            }
                                        }
                                    }
                                }                                
                            }
                            CellCoords = new Vector2(cellX, cellY);
                        }
                    }
                    CodeManager.CheckCodes();
                }
                else
                {
                    EntityManager.Update();
                    CodeManager.CheckPlayerCodes();
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if (WaitingForSecondClick)
            {
                TileMap.DrawRectangleIndicator(spriteBatch, InputProvider.MouseState, startCell);
            }
            EntityManager.Draw(spriteBatch);
        }
        #endregion

        public static void InsertEditorItem(int cellX, int cellY)
        {
            if (CurrentItem == null)
                return;

            MapSquare mapSquare = TileMap.GetMapSquareAtCell(cellX, cellY);
            if (mapSquare == null)
            {
                mapSquare = new MapSquare(null, null, null, CurrentItem.Passable == null ? true :  (bool)CurrentItem.Passable);
            }

            if (CurrentItem.Unique)
            {
                foreach (var item in TileMap.Map.MapData)
                {
                    if (TileMap.GetCellCodes(item.Key.X, item.Key.Y).Contains(CurrentItem.Code))
                        TileMap.GetCellCodes(item.Key.X, item.Key.Y).Remove(CurrentItem.Code);  
                }                
            }

            if (CurrentItem.Code != null)
             {                
                if(!mapSquare.Codes.Contains(CurrentItem.Code))
                    mapSquare.Codes.Add(CurrentItem.Code);                                   
            }
            if (CurrentItem.CodeAbove != null)
            {
                if (RemoveTile)
                    TileMap.RemoveCodeFromCell(cellX, cellY - 1, CurrentItem.CodeAbove);
                else
                    TileMap.AddCodeToCell(cellX, cellY - 1, CurrentItem.CodeAbove);
            }
            if (CurrentItem.CodeBelow != null)
            {
                if (RemoveTile)
                    TileMap.RemoveCodeFromCell(cellX, cellY + 1, CurrentItem.CodeBelow);
                else
                    TileMap.AddCodeToCell(cellX, cellY + 1, CurrentItem.CodeBelow);
            }
            if (CurrentItem.CodeLeft != null)
            {
                if (RemoveTile)
                    TileMap.RemoveCodeFromCell(cellX - 1, cellY, CurrentItem.CodeLeft);
                else
                    TileMap.AddCodeToCell(cellX - 1, cellY, CurrentItem.CodeLeft);
            }
            if (CurrentItem.CodeRight != null)
            {
                if (RemoveTile)
                    TileMap.RemoveCodeFromCell(cellX + 1, cellY, CurrentItem.CodeRight);
                else
                    TileMap.AddCodeToCell(cellX + 1, cellY, CurrentItem.CodeRight);
            }       
            if (CurrentItem.TileID != null)
            {
                mapSquare.LayerTiles[DrawLayer] = CurrentItem.TileID;
            }
            TileMap.SetMapSquareAtCell(cellX, cellY, mapSquare);
        }

        public static EditorItem GetEditorItemByName(string name)
        {
            EditorItem item = new EditorItem();
            switch (name)
            { 
                case "0":
                    item.Passable = false;
                    item.TileID = 0;
                    item.Type = ItemType.Tile;
                    break;
                
                case "1":
                    item.Passable = true;
                    item.Code = "JUMPTHROUGH";
                    item.CodeAbove = "JUMPTHROUGHTOP";
                    item.TileID = 1;
                    item.Type = ItemType.Tile;
                    break;

                case "2":
                    item.Passable = true;
                    item.Code = "WALKRIGHT";
                    item.TileID = 2;
                    item.Type = ItemType.Tile;
                    break;

                case "3":
                    item.Passable = true;
                    item.Code = "WALKLEFT";
                    item.TileID = 3;
                    item.Type = ItemType.Tile;
                    break;

                case "start":
                    item.Type = ItemType.Entity;
                    item.Code = "START";
                    item.Unique = true;
                    break;
                
                default:
                    item.Code = name.ToUpper();
                    item.Type = ItemType.Entity;
                    break;
            }
            return item;
        }
    }
}
