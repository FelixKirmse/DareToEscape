using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BlackDragonEngine;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Managers;
using BlackDragonEngine.Providers;
using BlackDragonEngine.Scripting;
using BlackDragonEngine.TileEngine;
using DareToEscape;
using DareToEscape.Helpers;
using DareToEscape.Managers;
using DareToEscape.Providers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace MapEditor
{
    public sealed class MapEditor : Game
    {
        public const uint InteractiveLayer = 1;
        private const uint Layers = 3;
        private readonly BulletManager _bulletManager;
        private readonly IntPtr _drawSurface;
        private readonly Editor _editorForm;
        private readonly Control _gameControl;
        private readonly GraphicsDeviceManager _graphics;
        private readonly Form _parentForm;
        private readonly PictureBox _pictureBox;
        private readonly Viewport _renderViewport = new Viewport(0, 0, 640, 480);
        private readonly Viewport _standardViewport = new Viewport(0, 0, 320, 240);
        public TileMap<Map<TileCode>, TileCode> TileMap;
        private CoordList _coordList;
        private Item _draggingItem;
        private bool _draggingItemBool;
        private Coords _lastCell;
        private GameObject _player;
        private RenderTarget2D _renderTarget;
        private SpriteBatch _spriteBatch;
        public bool MapLoaded { get; set; }

        public MapEditor()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _editorForm = new Editor();
            _editorForm.Show();
            _bulletManager = BulletManager.GetInstance();
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
            VariableProvider.ScriptEngine = new ScriptEngine(this);
            Components.Add(VariableProvider.ScriptEngine);
            GameVariableProvider.SaveManager = new SaveManager<SaveState>();
            Layer = InteractiveLayer;
            DrawAll = true;
            MapLoaded = false;
        }

        public bool DrawAll { get; set; }

        public uint Layer { get; set; }

        public bool Playing { get; set; }

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
            Camera.ViewPortWidth = 320;
            Camera.ViewPortHeight = 240;
            _graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            VariableProvider.SpriteBatch = _spriteBatch;
            TileMap = new TileMap<Map<TileCode>, TileCode>(8, 8, 0, Content.Load<SpriteFont>(@"fonts/mono8"),
                                                           Content.Load<Texture2D>(@"textures/tilesheets/tilesheet"),
                                                           Layers);
            AnimationDictionaryProvider.Content = Content;
            _player = Factory.CreatePlayer();
            _player.Position = new Vector2(float.MaxValue, float.MaxValue);
            VariableProvider.CurrentPlayer = _player;
            EntityManager.SetPlayer();
            _renderTarget = new RenderTarget2D(GraphicsDevice, 320, 240, false,
                                               GraphicsDevice.DisplayMode.Format, DepthFormat.Depth24);
            GameInitializer.Initialize();
            _coordList = VariableProvider.CoordList;
            EngineState.GameState = EngineStates.Editor;
            BulletInformationProvider.LoadBulletData(Content);
            FontProvider.AddFont("Mono8", Content.Load<SpriteFont>("fonts/mono8"));
        }

        protected override void Update(GameTime gameTime)
        {
            if (Form.ActiveForm != _parentForm || !MapLoaded) return;
            InputProvider.Update();
            VariableProvider.GameTime = gameTime;
            if (Playing)
            {
                UpdateIngame();
            }
            else
            {
                UpdateEditor();
            }
            base.Update(gameTime);
        }

        private void UpdateIngame()
        {
            _bulletManager.Update();
            EntityManager.Update();
            CodeManager<TileCode>.CheckPlayerCodes(TileMap);
        }

        private void UpdateEditor()
        {
            MouseState ms = InputProvider.MouseState;
            EntityManager.ClearEntities();
            VariableProvider.ScriptEngine.StopAllScripts();
            GameVariableProvider.Bosses.Clear();
            CodeManager<TileCode>.CheckCodes<Map<TileCode>>();
            if ((ms.X > 0) && (ms.Y > 0) && (ms.X < 640) && (ms.Y < 480))
            {
                if ((ShortCuts.AreAnyKeysDown(new[] {Keys.LeftAlt, Keys.Space}) && InputMapper.LeftClick) ||
                    ms.MiddleButton == ButtonState.Pressed)
                    HandleCameraMovement(ms);
                else
                    HandleMouseActions(ms);
            }
        }

        private void HandleMouseActions(MouseState ms)
        {
            Coords cell = TileMap.GetCellByPixel(Camera.ScreenToWorld(_coordList[ms.X/2, ms.Y/2]));
            _editorForm._positionLabel.Text = string.Format(
                @"DebugInfo:
    Coords GetCellByPixel: ({0}|{1})
    Coords Precise: ({2}|{3})
    WorldPixelCoords: ({4}|{5})
    ScreenPixelCoords: ({6}|{7})
    TileCode: {8}",
                cell.X, cell.Y, Camera.ScreenToWorld(new Vector2(ms.X/2)).X/TileMap.TileWidth,
                Camera.ScreenToWorld(new Vector2(ms.Y/2)).Y/TileMap.TileHeight,
                Camera.ScreenToWorld(new Vector2(ms.X/2, ms.Y/2)).X, Camera.ScreenToWorld(new Vector2(ms.X/2, ms.Y/2)).Y,
                ms.X/2, ms.Y/2, TileMap.Map.Codes.ContainsKey(cell) ? TileMap.Map.Codes[cell][0].ToString() : "null");
            if (!InputMapper.LeftClick)
            {
                _lastCell = null;
                _draggingItemBool = false;
            }
            if (ShortCuts.IsKeyDown(Keys.LeftControl) && InputMapper.LeftClick && cell != _lastCell)
            {
                if (!_draggingItemBool)
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

                if (_lastCell != null)
                    TileMap.RemoveEverythingAtCell(_lastCell);
                TileMap.RemoveEverythingAtCell(cell);
                InsertItem(cell, _draggingItem);
                _lastCell = cell;
                return;
            }
            if (InputMapper.StrictLeftClick || ShortCuts.IsKeyDown(Keys.LeftShift) && InputMapper.LeftClick)
            {
                if (_lastCell == null)
                {
                    InsertItem(cell);
                    _lastCell = cell;
                    return;
                }
                List<Vector2> path = PathFinder<Map<TileCode>, TileCode>.FindPath(_lastCell, cell, TileMap, true);
                foreach (var thisCell in path)
                {
                    InsertItem(thisCell);
                }
                _lastCell = cell;
                return;
            }
            if (InputMapper.StrictRightClick || ShortCuts.IsKeyDown(Keys.LeftShift) && InputMapper.RightClick)
            {
                if (_lastCell == null)
                {
                    TileMap.RemoveEverythingAtCell(cell);
                    _lastCell = cell;
                    return;
                }

                List<Vector2> path = PathFinder<Map<TileCode>, TileCode>.FindPath(_lastCell, cell, TileMap, true);
                foreach (var thisCell in path)
                {
                    TileMap.RemoveEverythingAtCell(thisCell);
                }
                _lastCell = cell;
                return;
            }
        }

        private void HandleCameraMovement(MouseState ms)
        {
            var currPos = new Vector2(ms.X, ms.Y);
            var lastPos = new Vector2(InputProvider.LastMouseState.X, InputProvider.LastMouseState.Y);
            Vector2 diff = currPos - lastPos;
            Camera.ForcePosition -= diff/2;
        }

        private void InsertItem(Coords cell, Item item)
        {
            Item i = item;
            if (i.Codes != null)
                i.Codes = item.Codes.ToList();

            MapSquare? square = null;
            if (i.TileID.HasValue)
            {
                MapSquare? existingSquare = TileMap.GetMapSquareAtCell(cell);
                if (existingSquare.HasValue)
                {
                    square = existingSquare;
                    square.Value.LayerTiles[Layer] = i.TileID;
                    if (Layer == InteractiveLayer)
                    {
                        MapSquare temp = square.Value;
                        temp.Passable = i.Passable.GetValueOrDefault();
                        square = temp;
                    }
                }
                else
                {
                    square = new MapSquare(i.TileID.Value, i.Passable, Layer);
                }
            }

            if (i.IsTurret)
            {
                string add = !TileMap.CellIsPassable(cell.Up)
                                 ? "UP"
                                 : !TileMap.CellIsPassable(cell.Down)
                                       ? "DOWN"
                                       : !TileMap.CellIsPassable(cell.Left)
                                             ? "LEFT"
                                             : !TileMap.CellIsPassable(cell.Right) ? "RIGHT" : "DOWN";
                TileCode tmp = i.Codes[0];
                tmp.Message += add;
                i.Codes[0] = tmp;
            }

            List<TileCode> codes = i.AddToExisting
                                       ? TileMap.GetCellCodes(cell).Union(i.Codes).ToList()
                                       : i.Codes == null ? null : i.Codes.ToList();

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
                if (codeToRemove.HasValue)
                {
                    codes = i.Codes.ToList();
                    TileMap.RemoveCodeFromCell(cellToRemoveFrom, codeToRemove.Value);
                }
            }
            TileMap.SetEverythingAtCell(square, codes, cell);

            if (i.ExtraCells == null) return;
            for (int j = 0; j < i.ExtraCells.Count; ++j)
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
            if(!MapLoaded)
            {
                _spriteBatch.DrawString(FontProvider.GetFont("Mono8"), "Please load a map.", ShortCuts.ScreenCenter - ShortCuts.GetFontCenter("Mono8", "Please load a map."), Color.Green);
            }
            else if (DrawAll)
                TileMap.Draw();
            else
                TileMap.Draw(Layer);
            EntityManager.Draw();
            _bulletManager.Draw();
            _spriteBatch.End();
            DrawHelper.Draw();
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
            EntityManager.ClearEntities();
            _bulletManager.ClearAllBullets();
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