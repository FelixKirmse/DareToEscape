using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BlackDragonEngine.GameStates;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Managers;
using BlackDragonEngine.Providers;
using BlackDragonEngine.TileEngine;
using DareToEscape.Editor;
using DareToEscape.Helpers;
using DareToEscape.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace DareToEscape.GameStates
{
    internal sealed class EditorManager : IUpdateableGameState, IDrawableGameState
    {
        public Vector2 CellCoords = Vector2.Zero;
        public EditorItem CurrentItem;
        public int DrawLayer;
        public int DrawTile;
        public string FillMode = "TILEFILL";
        public bool GettingCode;
        public bool InsertTile;
        public bool MakePassable = true;
        public bool MakeUnpassable;
        public bool PlayLevel;
        public bool RemoveTile;
        public bool SettingCode;
        public bool SmartInsert = true;
        private Vector2 _startCell;
        private bool _waitingForSecondClick;

        #region Doing The Impossible

        private static EditorManager _instance;
        private readonly EventHandler _gameFormVisibleChanged;
        private readonly PresentationParameters _orgPParams;
        private readonly EventHandler _pictureBoxSizeChanged;
        private readonly EventHandler<PreparingDeviceSettingsEventArgs> _preparingDeviceSettingsHandler;
        private readonly EventHandler<PreparingDeviceSettingsEventArgs> _resetDeviceSettingsHandler;
        private IntPtr _drawSurface;
        private MapEditor _editorForm;
        private DareToEscape _game;
        private Control _gameForm;
        private Form _parentForm;
        private PictureBox _pictureBox;

        private EditorManager()
        {
            _gameFormVisibleChanged = GameFormVisibleChanged;
            _pictureBoxSizeChanged = PictureBoxSizeChanged;
            _preparingDeviceSettingsHandler = GraphicsPreparingDeviceSettings;
            _resetDeviceSettingsHandler = GraphicsResetDeviceSettings;
            _orgPParams = DareToEscape.Graphics.GraphicsDevice.PresentationParameters.Clone();
            DareToEscape.Graphics.PreparingDeviceSettings += _resetDeviceSettingsHandler;
        }

        #region IDrawableGameState Members

        public bool DrawCondition
        {
            get { return UpdateCondition; }
        }

        #endregion

        #region IUpdateableGameState Members

        public bool UpdateCondition
        {
            get { return GameStateManager.State == States.Editor; }
        }

        #endregion

        public static EditorManager GetInstance()
        {
            return _instance ?? (_instance = new EditorManager());
        }

        public void Activate(string levelname)
        {
            _editorForm = new MapEditor(levelname);
            Activate();
        }

        public void Activate()
        {
            GameStateManager.State = States.Editor;
            VariableProvider.CurrentPlayer = Factory.CreatePlayer();
            EntityManager.SetPlayer();
            if (_editorForm == null)
                _editorForm = new MapEditor();
            _editorForm.Show();
            _drawSurface = _editorForm.pctSurface.Handle;
            _parentForm = _editorForm;
            _pictureBox = _editorForm.pctSurface;
            _game = (DareToEscape) VariableProvider.Game;
            _editorForm.Game = _game;

            DareToEscape.Graphics.PreparingDeviceSettings -= _resetDeviceSettingsHandler;
            DareToEscape.Graphics.PreparingDeviceSettings += _preparingDeviceSettingsHandler;
            DareToEscape.Graphics.GraphicsDevice.Reset();
            DareToEscape.Graphics.ApplyChanges();

            _gameForm = Control.FromHandle(_game.Window.Handle);
            _gameForm.VisibleChanged += _gameFormVisibleChanged;
            _pictureBox.SizeChanged += _pictureBoxSizeChanged;
            _gameForm.Visible = false;

            Camera.ViewPortWidth = _pictureBox.Width;
            Camera.ViewPortHeight = _pictureBox.Height;
            Camera.UpdateWorldRectangle();
            TileMap.SpriteFont = FontProvider.GetFont("Mono8");
            PictureBoxSizeChanged(null, null);
        }

        private void GraphicsPreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            e.GraphicsDeviceInformation.PresentationParameters.DeviceWindowHandle = _drawSurface;
            Mouse.WindowHandle = _drawSurface;
        }

        private void GraphicsResetDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            e.GraphicsDeviceInformation.PresentationParameters = _orgPParams.Clone();
            Mouse.WindowHandle = _orgPParams.Clone().DeviceWindowHandle;
        }

        public void Deactivate()
        {
            _gameForm.VisibleChanged -= _gameFormVisibleChanged;
            _pictureBox.SizeChanged -= _pictureBoxSizeChanged;
            DareToEscape.Graphics.PreparingDeviceSettings -= _preparingDeviceSettingsHandler;
            DareToEscape.Graphics.PreparingDeviceSettings += _resetDeviceSettingsHandler;
            DareToEscape.Graphics.GraphicsDevice.Reset();
            DareToEscape.Graphics.PreferredBackBufferWidth = 800;
            DareToEscape.Graphics.PreferredBackBufferHeight = 600;
            Camera.ViewPortWidth = 800;
            Camera.ViewPortHeight = 600;
            DareToEscape.Graphics.ApplyChanges();
            GameStateManager.State = States.Menu;
            _editorForm.Hide();
            _gameForm.Visible = true;
            TileMap.EditorMode = false;
        }

        public void JumpToLevel(string levelName)
        {
            Deactivate();
            GameStateManager.State = States.Ingame;
            Ingame.GetInstance().Activate();
            LevelManager.LoadLevel(levelName);
        }

        private void GameFormVisibleChanged(object sender, EventArgs e)
        {
            if (_gameForm.Visible) _gameForm.Visible = false;
        }

        private void PictureBoxSizeChanged(object sender, EventArgs e)
        {
            if (_parentForm.WindowState != FormWindowState.Minimized)
            {
                DareToEscape.Graphics.PreferredBackBufferWidth = _pictureBox.Width;
                DareToEscape.Graphics.PreferredBackBufferHeight = _pictureBox.Height;
                Camera.ViewPortWidth = _pictureBox.Width;
                Camera.ViewPortHeight = _pictureBox.Height;
                DareToEscape.Graphics.ApplyChanges();
            }
        }

        #endregion

        #region Updating and Drawing

        #region IDrawableGameState Members

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_waitingForSecondClick)
            {
                TileMap.DrawRectangleIndicator(spriteBatch, InputProvider.MouseState, _startCell);
            }
            EntityManager.Draw(spriteBatch);
            LevelManager.Draw(spriteBatch);
        }

        #endregion

        #region IUpdateableGameState Members

        public bool Update()
        {
            if (Form.ActiveForm != _parentForm) return true;
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
                    int cellX = TileMap.GetCellByPixelX((int) mouseLoc.X);
                    int cellY = TileMap.GetCellByPixelY((int) mouseLoc.Y);

                    switch (FillMode)
                    {
                        case "TILEFILL":
                            if (ShortcutProvider.LeftButtonClicked())
                            {
                                if (SmartInsert)
                                    InsertEditorItem(cellX, cellY);
                                else if (!RemoveTile)
                                    TileMap.SetTileAtCell(cellX, cellY, DrawLayer, DrawTile);

                                if (RemoveTile)
                                {
                                    TileMap.RemoveEverythingAtCell(cellX, cellY);
                                }
                            }
                            if (ShortcutProvider.RightButtonClicked())
                            {
                                if (SettingCode)
                                {
                                    ((MapEditor) _parentForm).SetCodeList(cellX, cellY);
                                }
                                else if (MakePassable)
                                {
                                    TileMap.SetPassabilityAtCell(cellX, cellY, true);
                                }
                                else if (MakeUnpassable)
                                {
                                    TileMap.SetPassabilityAtCell(cellX, cellY, false);
                                }
                                else if (GettingCode)
                                {
                                    ((MapEditor) _parentForm).GetCodeList(TileMap.GetCellCodes(cellX, cellY));
                                }
                                if (InsertTile)
                                {
                                    TileMap.SetTileAtCell(cellX, cellY, DrawLayer, DrawTile);
                                }
                            }
                            break;
                        case "RECTANGLEFILL":
                            if (ShortcutProvider.LeftButtonClickedNowButNotLastFrame())
                            {
                                if (!_waitingForSecondClick)
                                {
                                    _startCell = new Vector2(cellX, cellY);
                                    _waitingForSecondClick = true;
                                }
                                else
                                {
                                    var endCell = new Vector2(cellX, cellY);
                                    _waitingForSecondClick = false;

                                    for (var cellx = (int) _startCell.X; cellx <= endCell.X; ++cellx)
                                    {
                                        for (var celly = (int) _startCell.Y; celly <= endCell.Y; ++celly)
                                        {
                                            if (SmartInsert)
                                                InsertEditorItem(cellx, celly);
                                            else if (!RemoveTile)
                                                TileMap.SetTileAtCell(cellx, celly, DrawLayer, DrawTile);

                                            if (RemoveTile)
                                            {
                                                TileMap.RemoveEverythingAtCell(cellx, celly);
                                            }
                                        }
                                    }
                                }
                            }
                            if (ShortcutProvider.RightButtonClickedButNotLastFrame())
                            {
                                if (!_waitingForSecondClick)
                                {
                                    _startCell = new Vector2(cellX, cellY);
                                    _waitingForSecondClick = true;
                                }
                                else
                                {
                                    var endCell = new Vector2(cellX, cellY);
                                    _waitingForSecondClick = false;

                                    for (var cellx = (int) _startCell.X; cellx <= endCell.X; ++cellx)
                                    {
                                        for (var celly = (int) _startCell.Y; celly <= endCell.Y; ++celly)
                                        {
                                            if (SettingCode)
                                            {
                                                ((MapEditor) _parentForm).SetCodeList(cellx, celly);
                                            }
                                            else if (MakePassable)
                                            {
                                                TileMap.SetPassabilityAtCell(cellx, celly, true);
                                            }
                                            else if (MakeUnpassable)
                                            {
                                                TileMap.SetPassabilityAtCell(cellx, celly, false);
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
                            break;
                    }
                }
                CodeManager.CheckCodes();
            }
            else
            {
                EntityManager.Update();
                CodeManager.CheckPlayerCodes();
            }
            return true;
        }

        #endregion

        #endregion

        private void InsertEditorItem(int cellX, int cellY)
        {
            if (CurrentItem == null)
                return;

            MapSquare mapSquare = TileMap.GetMapSquareAtCell(cellX, cellY);
            List<string> codes = TileMap.GetCellCodes(cellX, cellY);
            if (mapSquare.InValidSquare)
            {
                mapSquare = new MapSquare(0, CurrentItem.Passable == null || (bool) CurrentItem.Passable);
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
                mapSquare.LayerTiles[DrawLayer] = (int) CurrentItem.TileID;
            }
            if (CurrentItem.Type == ItemType.Tile)
                TileMap.SetMapSquareAtCell(cellX, cellY, mapSquare);
            TileMap.SetCellCodes(cellX, cellY, codes);
        }

        public EditorItem GetEditorItemByName(string name)
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