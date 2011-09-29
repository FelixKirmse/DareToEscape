using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using DareToEscape.Menus;

namespace DareToEscape.Managers
{
    static class MenuManager
    {
        private static MainMenu mainMenu;
        private static IngameMenu ingameMenu;
       
        public static void Initialize()
        {
            mainMenu = new MainMenu();
            ingameMenu = new IngameMenu();           
        }

        public static void Update()
        {
            switch (StateManager.MenuState)
            { 
                case MenuStates.Main:
                    mainMenu.Update();
                    break;

                case MenuStates.Ingame:
                    ingameMenu.Update();
                    break;
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            switch (StateManager.MenuState)
            { 
                case MenuStates.Main:
                    mainMenu.Draw(spriteBatch);
                    break;

                case MenuStates.Ingame:
                    ingameMenu.Draw(spriteBatch);
                    break;
            }
        }
    }
}
