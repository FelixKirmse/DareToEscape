using System;
using System.Collections.Generic;
using System.Linq;
using DareToEscape.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using BlackDragonEngine.Providers;
using DareToEscape.Managers;
using xTile;
using xTile.Display;
using xTile.Dimensions;
using BlackDragonEngine.Managers;
using BlackDragonEngine.Helpers;
using System.Diagnostics;


namespace DareToEscape
{    
    public class DareToEscape : Microsoft.Xna.Framework.Game
    {
        static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public DareToEscape()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            VariableProvider.Game = this;
            GameInitializer.Initialize();

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();            
            
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ContentLoader.LoadContent(Content);
            MenuManager.Initialize();

            VariableProvider.DisplayDevice = new XnaDisplayDevice(Content, GraphicsDevice);
            VariableProvider.Viewport = new xTile.Dimensions.Rectangle(new Size(800, 600));
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
                Camera.Update();
            }
            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);            
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            StateManager.Draw(spriteBatch);
            spriteBatch.End();
            
            base.Draw(gameTime);
        }

        public void OnLevelLoad()
        {            
            LevelManager.CurrentMap.Layers[LevelManager.CurrentMap.Properties["PlayerLayer"]].AfterDraw += IngameManager.OnAfterDraw;            
            CodeManager.CheckCodes();            
        }

        public static void ToggleFullScreen()
        {
            graphics.IsFullScreen = !graphics.IsFullScreen;
            graphics.ApplyChanges();
        }
    }
}
