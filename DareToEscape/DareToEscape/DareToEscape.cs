using System;
using System.Collections.Generic;
using System.Linq;
using DareToEscape.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using BlackDragonEngine.Providers;
using DareToEscape.Managers;
using BlackDragonEngine.Managers;
using BlackDragonEngine.Helpers;
using System.Diagnostics;
using DareToEscape.Entities;
using BlackDragonEngine;
using DareToEscape.Providers;
using BlackDragonEngine.Scripting;


namespace DareToEscape
{    
    public class DareToEscape : Microsoft.Xna.Framework.Game
    {
        public static GraphicsDeviceManager Graphics;
        SpriteBatch spriteBatch;

        public DareToEscape()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            ScriptEngine engine = new ScriptEngine(this);
            Components.Add(engine);
            VariableProvider.ScriptEngine = engine;
            BulletManager bulletManager = new BulletManager(this);
            Components.Add(bulletManager);
            GameVariableProvider.BulletManager = bulletManager;
        }
        
        protected override void Initialize()
        {
            VariableProvider.Game = this;
            GameInitializer.Initialize();            

            Graphics.PreferredBackBufferWidth = 800;
            Graphics.PreferredBackBufferHeight = 600;
            Graphics.ApplyChanges();            
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ContentLoader.LoadContent(Content);
            MenuManager.Initialize();
            Editor.EditorManager.Initialize();
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
            
            base.Draw(gameTime);
        }

        public void OnLevelLoad()
        {       
            CodeManager.CheckCodes();
        }

        public static void ToggleFullScreen()
        {
            Graphics.IsFullScreen = !Graphics.IsFullScreen;
            Graphics.ApplyChanges();
        }
    }
}
