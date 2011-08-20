using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DareToEscape.Managers;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Providers;
using Microsoft.Xna.Framework.Input;

namespace DareToEscape.Menus
{
    static class Titlescreen
    {
        public static Texture2D TitleTexture { get; set; }

        public static void Update()
        {
            if(InputProvider.KeyState.GetPressedKeys().Length > 0)        
                StateManager.GameState = GameStates.Menu;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                TitleTexture,
                Vector2.Zero,
                Color.White);
        }
    }
}
