using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Providers;
using BlackDragonEngine.TileEngine;
using DareToEscape;
using DareToEscape.Helpers;
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
        private readonly IntPtr _drawSurface;
        private readonly Editor _editorForm;
        private readonly Control _gameControl;
        private readonly GraphicsDeviceManager _graphics;
        private readonly Form _parentForm;
        private readonly PictureBox _pictureBox;
        private readonly Viewport _renderViewport = new Viewport(0, 0, 640, 480);
        private readonly Viewport _standardViewport = new Viewport(0, 0, 320, 240);
        private GameObject _player;
        private RenderTarget2D _renderTarget;
        private SpriteBatch _spriteBatch;
        private TileMap<Map<TileCode>, TileCode> _tileMap;

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
            _tileMap = new TileMap<Map<TileCode>, TileCode>(8, 8, 0, Content.Load<SpriteFont>(@"fonts/mono8"),
                                                            Content.Load<Texture2D>(@"textures/tilesheets/tilesheet"))
                           {EditorMode = true};
            AnimationDictionaryProvider.Content = Content;
            _player = Factory.CreatePlayer();
            _renderTarget = new RenderTarget2D(GraphicsDevice, 320, 240, false,
                                               GraphicsDevice.DisplayMode.Format, DepthFormat.Depth24);
            GameInitializer.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Form.ActiveForm != _parentForm) return;
            InputProvider.Update();
            MouseState ms = InputProvider.MouseState;
            if ((ms.X > 0) && (ms.Y > 0) && (ms.X < Camera.ViewPortWidth) && (ms.Y < Camera.ViewPortHeight))
            {
                if ((ShortCuts.AreAnyKeysDown(new[] {Keys.LeftAlt, Keys.Space}) && ms.LeftButton == ButtonState.Pressed) ||
                    ms.MiddleButton == ButtonState.Pressed)
                    HandleCameraMovement(ms);
                else
                    HandleMouseActions(ms);
            }
            base.Update(gameTime);
        }

        private void HandleMouseActions(MouseState ms)
        {
            Coords cell = _tileMap.GetCellByPixel(Camera.ScreenToWorld(new Vector2(ms.X/2, ms.Y/2)));
            if (InputMapper.StrictLeftClick)
            {
                _tileMap.SetTileAtCell(cell, TileID);
            }
            if (InputMapper.StrictRightClick)
            {
                _tileMap.RemoveEverythingAtCell(cell);
            }
        }

        private void HandleCameraMovement(MouseState ms)
        {
            var currPos = new Vector2(ms.X, ms.Y);
            var lastPos = new Vector2(InputProvider.LastMouseState.X, InputProvider.LastMouseState.Y);
            Vector2 diff = currPos - lastPos;
            Camera.ForcePosition -= diff;
        }

        internal void InsertItem(Coords cell)
        {
            Item i = CurrentItem;
            MapSquare square = i.AddToExisting ? _tileMap.GetMapSquareAtCell(cell) : new MapSquare();
            List<TileCode> codes = i.AddToExisting ? _tileMap.GetCellCodes(cell) : i.Codes;

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(_renderTarget);
            GraphicsDevice.Viewport = _standardViewport;
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);
            _tileMap.Draw(_spriteBatch);
            _player.Draw(_spriteBatch);
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
    }
}