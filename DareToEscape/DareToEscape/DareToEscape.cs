using BlackDragonEngine.Helpers;
using BlackDragonEngine.Managers;
using BlackDragonEngine.Providers;
using BlackDragonEngine.Scripting;
using BlackDragonEngine.TileEngine;
using DareToEscape.Editor;
using DareToEscape.Helpers;
using DareToEscape.Managers;
using DareToEscape.Providers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DareToEscape
{
    public class DareToEscape : Game
    {
        public static GraphicsDeviceManager Graphics;
        private SpriteBatch spriteBatch;

        public DareToEscape()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsFixedTimeStep = false;
            Graphics.SynchronizeWithVerticalRetrace = false;
            var engine = new ScriptEngine(this);
            Components.Add(engine);
            VariableProvider.ScriptEngine = engine;
            
        }

        protected override void Initialize()
        {
            VariableProvider.CoordList = new CoordList();
            VariableProvider.Game = this;
            GameInitializer.Initialize();
            var bulletManager = new BulletManager(this);
            Components.Add(bulletManager);
            GameVariableProvider.BulletManager = bulletManager;

            Graphics.PreferredBackBufferWidth = 1280;
            Graphics.PreferredBackBufferHeight = 720;
            Graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ContentLoader.LoadContent(Content);
            MenuManager.Initialize();
            EditorManager.Initialize();
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
                StateManager.Update();
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            StateManager.Draw(spriteBatch);
            GameVariableProvider.BulletManager.Draw(spriteBatch);
            spriteBatch.End();
            DrawHelper.Draw(spriteBatch);

            base.Draw(gameTime);
        }

        public void OnLevelLoad()
        {
            CodeManager.CheckCodes();
            GameVariableProvider.BulletManager.ClearAllBullets();
        }

        public static void ToggleFullScreen()
        {
            Graphics.IsFullScreen = !Graphics.IsFullScreen;
            Graphics.ApplyChanges();
        }
    }
}