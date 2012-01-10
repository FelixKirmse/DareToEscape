using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Managers;
using BlackDragonEngine.Providers;
using BlackDragonEngine.TileEngine;
using DareToEscape;
using DareToEscape.Helpers;
using DareToEscape.Providers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace MapEditor
{
    public sealed class MapEditor : Game
    {
        private readonly IntPtr _drawSurface;
        private readonly Editor _editorForm;
        private readonly Control _gameControl;
        private readonly GraphicsDeviceManager _graphics;
        private readonly Form _parentForm;
        private readonly PictureBox _pictureBox;
        private readonly Viewport _renderViewport = new Viewport(0, 0, 640, 480);
        private readonly Viewport _standardViewport = new Viewport(0, 0, 320, 240);
        public TileMap<Map<TileCode>, TileCode> TileMap;
        private GameObject _player;
        private RenderTarget2D _renderTarget;
        private SpriteBatch _spriteBatch;
        private Coords _lastCell;
        private CoordList _coordList;
        private bool _draggingItemBool;
        private Item _draggingItem;

        public MapEditor()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _editorForm = new Editor();
            _editorForm.Show();

            _drawSurface = _editorForm.PctSurface.Handle;
            _pictureBox = _editorForm.PctSurface;
            _parentForm = _editorForm;
            _editorForm.Game = this;
            _graphics.PreparingDeviceSettings += GraphicsPreparingDeviceSettings;
            _gameControl = Control.FromHandle(Window.Handle);
            _gameControl.VisibleChanged += GameControlVisibleChanged;
            _gameControl.SizeChanged += PictureBoxSizeChanged;
            _gameControl.Visible = false;
            PictureBoxSizeChanged(null, null);
            CurrentItem = Item.GetItemByTileId(0);
            VariableProvider.Game = this;
        }

        internal Item CurrentItem { private get; set; }

        private void GraphicsPreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            e.GraphicsDeviceInformation.PresentationParameters.DeviceWindowHandle = _drawSurface;
            Mouse.WindowHandle = _drawSurface;
        }

        private void GameControlVisibleChanged(object sender, EventArgs e)
        {
            if (_gameControl.Visible)
                _gameControl.Visible = false;
        }

        private void PictureBoxSizeChanged(object sender, EventArgs e)
        {
            if (_parentForm.WindowState == FormWindowState.Minimized) return;
            _graphics.PreferredBackBufferWidth = _pictureBox.Width;
            _graphics.PreferredBackBufferHeight = _pictureBox.Height;
            Camera.ViewPortWidth = _pictureBox.Width;
            Camera.ViewPortHeight = _pictureBox.Height;
            _graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            TileMap = new TileMap<Map<TileCode>, TileCode>(8, 8, 0, Content.Load<SpriteFont>(@"fonts/mono8"),
                                                           Content.Load<Texture2D>(@"textures/tilesheets/tilesheet"));
            AnimationDictionaryProvider.Content = Content;
            _player = Factory.CreatePlayer();
            VariableProvider.CurrentPlayer = _player;
            _renderTarget = new RenderTarget2D(GraphicsDevice, 320, 240, false,
                                               GraphicsDevice.DisplayMode.Format, DepthFormat.Depth24);
            GameInitializer.Initialize();
            _coordList = VariableProvider.CoordList;
            _lastCell = _coordList[int.MaxValue, int.MaxValue];
        }

        protected override void Update(GameTime gameTime)
        {
            if (Form.ActiveForm != _parentForm) return;
            InputProvider.Update();
            VariableProvider.GameTime = gameTime;
            MouseState ms = InputProvider.MouseState;
            CodeManager<TileCode>.CheckCodes<Map<TileCode>>();
            if ((ms.X > 0) && (ms.Y > 0) && (ms.X < Camera.ViewPortWidth) && (ms.Y < Camera.ViewPortHeight))
            {
                if ((ShortCuts.AreAnyKeysDown(new[] {Keys.LeftAlt, Keys.Space}) && InputMapper.LeftClick) ||
                    ms.MiddleButton == ButtonState.Pressed)
                    HandleCameraMovement(ms);
                else
                    HandleMouseActions(ms);
            }
            base.Update(gameTime);
        }

        private void HandleMouseActions(MouseState ms)
        {
            Coords cell = TileMap.GetCellByPixel(Camera.ScreenToWorld(new Vector2(ms.X/2, ms.Y/2)));
            if(!InputMapper.LeftClick)
            {
                _lastCell = _coordList[int.MaxValue, int.MaxValue];
                _draggingItemBool = false;
            }
            if(ShortCuts.IsKeyDown(Keys.LeftControl) && InputMapper.LeftClick && cell != _lastCell)
            {
                if(!_draggingItemBool)
                {
                    MapSquare? square = TileMap.GetMapSquareAtCell(cell);
                    List<TileCode> codes = TileMap.GetCellCodes(cell);
                    _draggingItem = new Item
                                        {
                                            Codes = codes,
                                            Passable = square.HasValue ? square.Value.Passable : (bool?) null,
                                            TileID = square.HasValue ? square.Value.LayerTiles[0] : null
                                        };
                    _draggingItemBool = true;
                }
               
                TileMap.RemoveEverythingAtCell(_lastCell);
                TileMap.RemoveEverythingAtCell(cell);
                InsertItem(cell, _draggingItem);
                _lastCell = cell;
                return;
            }
            if (InputMapper.StrictLeftClick || ShortCuts.IsKeyDown(Keys.LeftShift) && InputMapper.LeftClick)
            {
                InsertItem(cell);
            }
            if (InputMapper.StrictRightClick || ShortCuts.IsKeyDown(Keys.LeftShift) && InputMapper.RightClick)
            {
                TileMap.RemoveEverythingAtCell(cell);
            }
        }

        private void HandleCameraMovement(MouseState ms)
        {
            var currPos = new Vector2(ms.X, ms.Y);
            var lastPos = new Vector2(InputProvider.LastMouseState.X, InputProvider.LastMouseState.Y);
            Vector2 diff = currPos - lastPos;
            Camera.ForcePosition -= diff;
        }

        private void InsertItem(Coords cell, Item item)
        {
            Item i = item;
            MapSquare? square = i.TileID == null || i.AddToExisting ? TileMap.GetMapSquareAtCell(cell) : new MapSquare(i.TileID, i.Passable);
            
            if(i.IsTurret)
            {
                string add = !TileMap.CellIsPassable(cell.Up)
                                 ? "UP"
                                 : !TileMap.CellIsPassable(cell.Down)
                                       ? "DOWN"
                                       : !TileMap.CellIsPassable(cell.Left)
                                             ? "LEFT"
                                             : !TileMap.CellIsPassable(cell.Right) ? "RIGHT" : "DOWN";
                var tmp = i.Codes[0];
                tmp.Message += add;
                i.Codes[0] = tmp;
            }

            List<TileCode> codes = i.AddToExisting
                                       ? TileMap.GetCellCodes(cell).Union(i.Codes).ToList()
                                       : i.Codes.ToList();

            if (i.Unique)
            {
                Coords cellToRemoveFrom = null;
                TileCode? codeToRemove = null;
                foreach (var cellCodePair in TileMap.Map.Codes)
                {
                    foreach (var mapCode in cellCodePair.Value)
                    {
                        foreach (var thisCode in codes)
                        {
                            if (mapCode == thisCode)
                            {
                                cellToRemoveFrom = cellCodePair.Key;
                                codeToRemove = mapCode;
                            }
                        }
                    }
                }
                if(codeToRemove.HasValue)
                {
                    codes = i.Codes.ToList();
                    TileMap.RemoveCodeFromCell(cellToRemoveFrom, codeToRemove.Value);
                }
            }
            TileMap.SetEverythingAtCell(square, codes, cell);

            if (i.ExtraCells == null) return;
            for(int j = 0; j < i.ExtraCells.Count; ++j)
            {
                InsertItem(cell + i.ExtraCells[j], i.ExtraItems[j]);
            }
        }

        private void InsertItem(Coords cell)
        {
            InsertItem(cell, CurrentItem);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(_renderTarget);
            GraphicsDevice.Viewport = _standardViewport;
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);
            TileMap.Draw(_spriteBatch);
            _player.Draw(_spriteBatch);
            EntityManager.Draw(_spriteBatch);
            _spriteBatch.End();
            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Viewport = _renderViewport;
            GraphicsDevice.Clear(Color.LightBlue);
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);
            _spriteBatch.Draw(_renderTarget, new Rectangle(0, 0, _renderViewport.Width, _renderViewport.Height),
                              Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public void LoadMap(string fileName)
        {
            using (var fs = new FileStream(fileName, FileMode.Open))
            {
                TileMap.LoadMap(fs);
            }
        }

        public void SaveMap(string fileName)
        {
            using (var fs = new FileStream(fileName, FileMode.Create))
            {
                TileMap.SaveMap(fs);
            }
        }
    }
}