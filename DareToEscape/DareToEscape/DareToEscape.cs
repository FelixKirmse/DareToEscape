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
        public static GraphicsDeviceManager Graphics;
        private SpriteBatch _spriteBatch;
        private GameStateManager _stateManager;

        public DareToEscape()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //IsFixedTimeStep = false;
            //Graphics.SynchronizeWithVerticalRetrace = false;
            var engine = new ScriptEngine(this);
            Components.Add(engine);
            VariableProvider.ScriptEngine = engine;
        }

        protected override void Initialize()
        {
            VariableProvider.CoordList = new CoordList();
            VariableProvider.Game = this;
            GameInitializer.Initialize();

            Graphics.PreferredBackBufferWidth = 1280;
            Graphics.PreferredBackBufferHeight = 720;
            Graphics.IsFullScreen = false;
            Graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ContentLoader.LoadContent(Content);
            _stateManager = new GameStateManager();
        }

        protected override void UnloadContent()
        {
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
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            _stateManager.Draw(_spriteBatch);
            _spriteBatch.End();
            DrawHelper.Draw(_spriteBatch);

            base.Draw(gameTime);
        }

        public void OnLevelLoad()
        {
            CodeManager.CheckCodes();
            BulletManager.GetInstance().ClearAllBullets();
        }

        public static void ToggleFullScreen()
        {
            Graphics.IsFullScreen = !Graphics.IsFullScreen;
            Graphics.ApplyChanges();
        }
    }
}