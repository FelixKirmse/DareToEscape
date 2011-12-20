using System.Threading.Tasks;
using System.Windows.Forms;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Managers;
using BlackDragonEngine.Providers;
using BlackDragonEngine.Scripting;
using BlackDragonEngine.TileEngine;
using DareToEscape.Helpers;
using DareToEscape.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DareToEscape
{
    public sealed class DareToEscape : Game
    {
        public const int ResolutionWidth = 320;
        public const int ResolutionHeight = 240;
        private Matrix _scaleMatrix;
        private SpriteBatch _spriteBatch;
        private GameStateManager _stateManager;

        public DareToEscape()
        {
            Task task = Task.Factory.StartNew(() => Application.Run(new ResolutionChooser(this)));
            Task.WaitAll(task);
            Graphics = new GraphicsDeviceManager(this)
                           {
                               PreferredBackBufferWidth = ResolutionWidth,
                               PreferredBackBufferHeight = ResolutionHeight
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
                Graphics.IsFullScreen = true;
            Graphics.PreferredBackBufferWidth = ResInfo.Resolution.Width;
            Graphics.PreferredBackBufferHeight = ResInfo.Resolution.Height;
            Graphics.PreparingDeviceSettings += FullScreenResolutionHack;
            Graphics.ApplyChanges();
            Graphics.PreparingDeviceSettings -= FullScreenResolutionHack;

            var myForm = (Form) Control.FromHandle(Window.Handle);
            myForm.Activate();
        }

        private void FullScreenResolutionHack(object sender, PreparingDeviceSettingsEventArgs e)
        {
            PresentationParameters pp = e.GraphicsDeviceInformation.PresentationParameters;
            pp.BackBufferWidth = ResInfo.Resolution.Width;
            pp.BackBufferHeight = ResInfo.Resolution.Height;
        }

        protected override void Initialize()
        {
            VariableProvider.CoordList = new CoordList();
            VariableProvider.Game = this;
            GameInitializer.Initialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ContentLoader.LoadContent(Content);
            _stateManager = new GameStateManager();
        }

        protected override void Update(GameTime gameTime)
        {
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
            GraphicsDevice.Viewport = ResInfo.Viewport;
            GraphicsDevice.Clear(Color.Black);

            if(InputMapper.StrictUp)
            {
                _scaleMatrix = Matrix.CreateScale(1);
            }
            if (InputMapper.StrictDown)
            {
                _scaleMatrix = Matrix.CreateScale(2);
            }
            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, Matrix.Identity * _scaleMatrix);
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
            Factory.CreatePlayer().Draw(_spriteBatch);
            _spriteBatch.End();
            DrawHelper.Draw(_spriteBatch);

            base.Draw(gameTime);
        }

        public void OnLevelLoad()
        {
            CodeManager.CheckCodes();
            BulletManager.GetInstance().ClearAllBullets();
        }
    }
}