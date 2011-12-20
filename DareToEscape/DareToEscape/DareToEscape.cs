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
            Task task = Task.Factory.StartNew(() =>
                                                  {
                                                      if(!Keyboard.GetState().IsKeyDown(Keys.LeftControl))
                                                      {
                                                          if (File.Exists(ResolutionChooser.Settings))
                                                          {
                                                              var fs = new FileStream(ResolutionChooser.Settings, FileMode.Open);
                                                              var xmls = new XmlSerializer(typeof(ResolutionInformation));
                                                              ResInfo = (ResolutionInformation)xmls.Deserialize(fs);
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
                                   ResInfo.FullScreen ? ResolutionHeight : ResInfo.Resolution.Height
                           };
            _scaleMatrix = ResInfo.Matrix;
            Content.RootDirectory = "Content";
            //IsFixedTimeStep = false;
            //Graphics.SynchronizeWithVerticalRetrace = false;
            var engine = new ScriptEngine(this);
            Components.Add(engine);
            VariableProvider.ScriptEngine = engine;
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
            PresentationParameters pp = e.GraphicsDeviceInformation.PresentationParameters;
            pp.BackBufferWidth = ResInfo.Resolution.Width;
            pp.BackBufferHeight = ResInfo.Resolution.Height;
        }

        protected override void LoadContent()
        {
            GraphicsDevice.Viewport = new Viewport(0, 0, ResolutionWidth, ResolutionHeight);
            VariableProvider.CoordList = new CoordList();
            VariableProvider.Game = this;
            GameInitializer.Initialize();


            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ContentLoader.LoadContent(Content);
            _stateManager = new GameStateManager();
            _renderTarget = new RenderTarget2D(GraphicsDevice, ResolutionWidth, ResolutionHeight, true,
                                               GraphicsDevice.DisplayMode.Format, DepthFormat.Depth24);
        }

        protected override void Update(GameTime gameTime)
        {
            var myForm = (Form)Control.FromHandle(Window.Handle);
            myForm.Activate();
            if (IsActive)
            {
                VariableProvider.GameTime = gameTime;
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
            _stateManager.Draw(_spriteBatch);
            _spriteBatch.DrawString(
                FontProvider.GetFont("Mono8"),
                string.Format(
                    "Your backbuffer is {0}x{1}.",
                    GraphicsDevice.PresentationParameters.BackBufferWidth,
                    GraphicsDevice.PresentationParameters.BackBufferHeight),
                new Vector2(10f, 10f),
                Color.White);
            _spriteBatch.DrawString(
                FontProvider.GetFont("Mono8"),
                string.Format(
                    "Your viewport is {0}x{1} \nstarting at ({2}|{3})\nwith Scale {4}",
                    GraphicsDevice.Viewport.Width,
                    GraphicsDevice.Viewport.Height,
                    GraphicsDevice.Viewport.X,
                    GraphicsDevice.Viewport.Y,
                    _scaleMatrix.M11),
                new Vector2(10f, 30f),
                Color.White);
            _spriteBatch.End();
            DrawHelper.Draw(_spriteBatch);

            if (!ResInfo.FullScreen)
            {
                GraphicsDevice.SetRenderTarget(null);
                GraphicsDevice.Viewport = ResInfo.Viewport;
                _spriteBatch.Begin();
                _spriteBatch.Draw(_renderTarget,
                                  new Rectangle(0, 0, ResInfo.Resolution.Width, ResInfo.Resolution.Height), Color.White);
                _spriteBatch.End();
            }
            base.Draw(gameTime);
        }

        public void OnLevelLoad()
        {
            CodeManager.CheckCodes();
            BulletManager.GetInstance().ClearAllBullets();
        }
    }
}