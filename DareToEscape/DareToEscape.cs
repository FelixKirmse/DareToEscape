using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Managers;
using BlackDragonEngine.Providers;
using BlackDragonEngine.Scripting;
using BlackDragonEngine.TileEngine;
using DareToEscape.Helpers;
using DareToEscape.Managers;
using DareToEscape.Providers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace DareToEscape
{
    public sealed class DareToEscape : Game
    {
        public const int ResolutionWidth = 320;
        public const int ResolutionHeight = 240;
        private RenderTarget2D _renderTarget;
        private Matrix _scaleMatrix;
        private SpriteBatch _spriteBatch;
        private GameStateManager _stateManager;

        public DareToEscape()
        {
            var task = Task.Factory.StartNew(() =>
            {
                if (!Keyboard.GetState().IsKeyDown(Keys.LeftControl))
                {
                    if (File.Exists(ResolutionChooser.Settings))
                    {
                        var fs = new FileStream(ResolutionChooser.Settings,
                            FileMode.Open);
                        var xmls =
                            new XmlSerializer(typeof(ResolutionInformation));
                        ResInfo = (ResolutionInformation) xmls.Deserialize(fs);
                        fs.Close();
                        return;
                    }
                }
                else
                {
                    File.Delete(ResolutionChooser.Settings);
                }

                Application.Run(new ResolutionChooser(this));
            });
            Task.WaitAll(task);
            Graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth =
                    ResInfo.FullScreen ? ResolutionWidth : ResInfo.Resolution.Width,
                PreferredBackBufferHeight =
                    ResInfo.FullScreen ? ResolutionHeight : ResInfo.Resolution.Height,
                PreferMultiSampling = false
            };
            _scaleMatrix = ResInfo.Matrix;
            Content.RootDirectory = "Content";
            //IsFixedTimeStep = false;
            //Graphics.SynchronizeWithVerticalRetrace = false;
            var engine = new ScriptEngine();
            VariableProvider.ScriptEngine = engine;
            GameVariableProvider.SaveManager = new SaveManager<SaveState>();
        }

        public ResolutionInformation ResInfo { private get; set; }

        public static GraphicsDeviceManager Graphics { get; private set; }

        protected override void BeginRun()
        {
            if (ResInfo.FullScreen)
            {
                Graphics.IsFullScreen = true;
                Graphics.PreparingDeviceSettings += FullScreenResolutionHack;
                Graphics.ApplyChanges();
                Graphics.PreparingDeviceSettings -= FullScreenResolutionHack;
            }
        }

        private void FullScreenResolutionHack(object sender, PreparingDeviceSettingsEventArgs e)
        {
            var pp = e.GraphicsDeviceInformation.PresentationParameters;
            pp.BackBufferWidth = ResInfo.Resolution.Width;
            pp.BackBufferHeight = ResInfo.Resolution.Height;
        }

        protected override void LoadContent()
        {
            LevelManager.OnLevelLoad += OnLevelLoad;
            Camera.ViewPortWidth = ResolutionWidth;
            Camera.ViewPortHeight = ResolutionHeight;
            GraphicsDevice.Viewport = new Viewport(0, 0, ResolutionWidth, ResolutionHeight);
            VariableProvider.CoordList = new CoordList();
            VariableProvider.GraphicsDevice = GraphicsDevice;
            VariableProvider.ClientBounds = Window.ClientBounds;
            VariableProvider.Content = Content;
            VariableProvider.Exit = Exit;
            VariableProvider.WhiteTexture = new Texture2D(GraphicsDevice, 1, 1);
            VariableProvider.WhiteTexture.SetData(new[] {Color.White});
            GameInitializer.Initialize();


            _spriteBatch = new SpriteBatch(GraphicsDevice);
            VariableProvider.SpriteBatch = _spriteBatch;
            ContentLoader.LoadContent(Content);
            _stateManager = new GameStateManager();
            _renderTarget = new RenderTarget2D(GraphicsDevice, ResolutionWidth, ResolutionHeight, false,
                GraphicsDevice.DisplayMode.Format, DepthFormat.Depth24);
        }

        protected override void OnDeactivated(object sender, EventArgs args)
        {
            var myForm = (Form) Control.FromHandle(Window.Handle);
            myForm.Activate();
        }

        protected override void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                VariableProvider.GameTime = gameTime;
                VariableProvider.ScriptEngine.Update(gameTime);
                InputProvider.Update();
                _stateManager.Update();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (!ResInfo.FullScreen)
            {
                GraphicsDevice.SetRenderTarget(_renderTarget);
                GraphicsDevice.Viewport = new Viewport(0, 0, ResolutionWidth, ResolutionHeight);
            }
            else
            {
                GraphicsDevice.Viewport = ResInfo.Viewport;
            }

            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            _stateManager.Draw();
            _spriteBatch.End();
            DrawHelper.Draw();

            if (!ResInfo.FullScreen)
            {
                GraphicsDevice.SetRenderTarget(null);
                GraphicsDevice.Viewport = ResInfo.Viewport;
                _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);
                _spriteBatch.Draw(_renderTarget,
                    new Rectangle(0, 0, ResInfo.Resolution.Width, ResInfo.Resolution.Height), Color.White);
                _spriteBatch.End();
            }

            base.Draw(gameTime);
        }

        public void OnLevelLoad()
        {
            CodeManager<TileCode>.CheckCodes<Map<TileCode>>();
            BulletManager.GetInstance().ClearAllBullets();
        }
    }
}