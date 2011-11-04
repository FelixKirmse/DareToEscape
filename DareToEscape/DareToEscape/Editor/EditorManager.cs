using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Managers;
using BlackDragonEngine.Providers;
using BlackDragonEngine.TileEngine;
using DareToEscape.Helpers;
using DareToEscape.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace DareToEscape.Editor
{
    internal static class EditorManager
    {
        public static int DrawLayer;
        public static int DrawTile;
        public static bool SettingCode;
        public static bool MakeUnpassable;
        public static bool MakePassable = true;
        public static bool GettingCode;
        public static bool InsertTile;
        public static Vector2 CellCoords = Vector2.Zero;
        public static string FillMode = "TILEFILL";
        public static bool WaitingForSecondClick;
        private static Vector2 startCell;
        public static bool RemoveTile;
        public static EditorItem CurrentItem;
        public static bool SmartInsert = true;
        public static bool PlayLevel;

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
            gameFormVisibleChanged = gameForm_VisibleChanged;
            pictureBoxSizeChanged = pictureBox_SizeChanged;
            preparingDeviceSettingsHandler = graphics_PreparingDeviceSettings;
            resetDeviceSettingsHandler = graphics_resetDeviceSettings;
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
            game = (DareToEscape) VariableProvider.Game;
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
                    int mod = 1;
                    if (ShortcutProvider.IsKeyDown(Keys.LeftShift))
                    {
                        mod = 2;
                    }
                    if (InputMapper.Up)
                    {
                        Camera.ForcePosition -= new Vector2(0, 5)*mod;
                    }
                    if (InputMapper.Down)
                    {
                        Camera.ForcePosition += new Vector2(0, 5)*mod;
                    }
                    if (InputMapper.Left)
                    {
                        Camera.ForcePosition -= new Vector2(5, 0)*mod;
                    }
                    if (InputMapper.Right)
                    {
                        Camera.ForcePosition += new Vector2(5, 0)*mod;
                    }

                    if ((ms.X > 0) && (ms.Y > 0) && (ms.X < Camera.ViewPortWidth) && (ms.Y < Camera.ViewPortHeight))
                    {
                        Vector2 mouseLoc = Camera.ScreenToWorld(new Vector2(ms.X, ms.Y));
                        var cellX = (int) MathHelper.Max(TileMap.GetCellByPixelX((int) mouseLoc.X), 0);
                        var cellY = (int) MathHelper.Max(TileMap.GetCellByPixelY((int) mouseLoc.Y), 0);

                        if (FillMode == "TILEFILL")
                        {
                            if (ShortcutProvider.LeftButtonClicked())
                            {
                                if (SmartInsert)
                                    InsertEditorItem(cellX, cellY);
                                else if (!RemoveTile)
                                    TileMap.SetTileAtCell(cellX, cellY, DrawLayer, DrawTile);

                                if (RemoveTile)
                                {
                                    TileMap.RemoveMapSquareAtCell(cellX, cellY);
                                }
                            }

                            if (ShortcutProvider.RightButtonClicked())
                            {
                                if (SettingCode)
                                {
                                    ((MapEditor) parentForm).SetCodeList(cellX, cellY);
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
                                    ((MapEditor) parentForm).GetCodeList(TileMap.GetCellCodes(cellX, cellY));
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
                                    var endCell = new Vector2(cellX, cellY);
                                    WaitingForSecondClick = false;

                                    for (var cellx = (int) startCell.X; cellx <= endCell.X; ++cellx)
                                    {
                                        for (var celly = (int) startCell.Y; celly <= endCell.Y; ++celly)
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
                                    var endCell = new Vector2(cellX, cellY);
                                    WaitingForSecondClick = false;

                                    for (var cellx = (int) startCell.X; cellx <= endCell.X; ++cellx)
                                    {
                                        for (var celly = (int) startCell.Y; celly <= endCell.Y; ++celly)
                                        {
                                            if (SettingCode)
                                            {
                                                ((MapEditor) parentForm).SetCodeList(cellx, celly);
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
            List<string> codes = TileMap.GetCellCodes(cellX, cellY);
            if (mapSquare == null)
            {
                mapSquare = new MapSquare(null, null, null,
                                          CurrentItem.Passable == null || (bool) CurrentItem.Passable);
            }

            if (CurrentItem.Unique)
            {
                List<Coords> codesToRemove = (from item in TileMap.Map.Codes
                                              where
                                                  TileMap.GetCellCodes(item.Key.X, item.Key.Y).Contains(CurrentItem.Code)
                                              select item.Key).ToList();
                codesToRemove.ForEach(coords => TileMap.RemoveCodeFromCell(coords.X, coords.Y, CurrentItem.Code));
            }

            if (CurrentItem.Code != null)
            {
                if (!codes.Contains(CurrentItem.Code))
                    codes.Add(CurrentItem.Code);
            }
            if (CurrentItem.CodeAbove != null)
            {
                if (RemoveTile)
                    TileMap.RemoveCodeFromCell(cellX, cellY - 1, CurrentItem.CodeAbove);
                else
                {
                    List<string> otherCodes = TileMap.GetCellCodes(cellX, cellY - 1);
                    if (!otherCodes.Contains(CurrentItem.CodeAbove))
                    {
                        TileMap.AddCodeToCell(cellX, cellY - 1, CurrentItem.CodeAbove);
                    }
                }
            }
            if (CurrentItem.CodeBelow != null)
            {
                if (RemoveTile)
                    TileMap.RemoveCodeFromCell(cellX, cellY + 1, CurrentItem.CodeBelow);
                else
                {
                    List<string> otherCodes = TileMap.GetCellCodes(cellX, cellY + 1);
                    if (!otherCodes.Contains(CurrentItem.CodeBelow))
                    {
                        TileMap.AddCodeToCell(cellX, cellY + 1, CurrentItem.CodeBelow);
                    }
                }
            }
            if (CurrentItem.CodeLeft != null)
            {
                if (RemoveTile)
                    TileMap.RemoveCodeFromCell(cellX - 1, cellY, CurrentItem.CodeLeft);
                else
                {
                    List<string> otherCodes = TileMap.GetCellCodes(cellX - 1, cellY);
                    if (!otherCodes.Contains(CurrentItem.CodeLeft))
                    {
                        TileMap.AddCodeToCell(cellX - 1, cellY, CurrentItem.CodeLeft);
                    }
                }
            }
            if (CurrentItem.CodeRight != null)
            {
                if (RemoveTile)
                    TileMap.RemoveCodeFromCell(cellX + 1, cellY, CurrentItem.CodeRight);
                else
                {
                    List<string> otherCodes = TileMap.GetCellCodes(cellX + 1, cellY);
                    if (!otherCodes.Contains(CurrentItem.CodeRight))
                    {
                        TileMap.AddCodeToCell(cellX + 1, cellY, CurrentItem.CodeRight);
                    }
                }
            }
            if (CurrentItem.TileID != null)
            {
                mapSquare.LayerTiles[DrawLayer] = CurrentItem.TileID;
            }
            TileMap.SetMapSquareAtCell(cellX, cellY, mapSquare);
            TileMap.SetCellCodes(cellX, cellY, codes);
        }

        public static EditorItem GetEditorItemByName(string name)
        {
            var item = new EditorItem();
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