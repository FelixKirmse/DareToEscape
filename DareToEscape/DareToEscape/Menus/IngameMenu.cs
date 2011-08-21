using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackDragonEngine.Menus;
using BlackDragonEngine.Helpers;
using DareToEscape.Managers;
using BlackDragonEngine.Managers;
using DareToEscape.Helpers;
using BlackDragonEngine.Providers;
using Microsoft.Xna.Framework;

namespace DareToEscape.Menus
{
    class IngameMenu : Menu
    {
        private const string resume = "Resume";
        private const string restartCheck = "Restart from last checkpoint";        
        private const string toggleFullscreen = "Toggle fullscreen";
        private const string back = "Back to main menu";

        public IngameMenu()
        {
            menuItems.Add(new MenuItem(resume, fontName, true, new Color(255,0,0), new Color(0,255,0)));
            menuItems.Add(new MenuItem(restartCheck, fontName,false,new Color(255,0,0), new Color(0,255,0)));            
            menuItems.Add(new MenuItem(toggleFullscreen, fontName, false, new Color(255,0,0), new Color(0,255,0)));
            menuItems.Add(new MenuItem(back, fontName, false, new Color(255,0,0), new Color(0,255,0)));

            EnableMouseSelection = false;
            SetPositions();
        }

        public override void Update()
        {
            base.Update();
            if (InputMapper.STRICTCANCEL)
            {
                StateManager.GamePaused = false;
                StateManager.GameState = GameStates.Ingame;
            }
        }

        public override void SelectMenuItem()
        {     
            switch (SelectedItem)
            { 
                case resume:
                    StateManager.GamePaused = false;
                    StateManager.GameState = GameStates.Ingame;
                    break;

                case restartCheck:
                    StateManager.GamePaused = false;
                    SaveManager<SaveState>.Load(VariableProvider.SaveSlot);
                    StateManager.GameState = GameStates.Ingame;
                    break;                

                case toggleFullscreen:
                    DareToEscape.ToggleFullScreen();
                    break;

                case back:
                    StateManager.GamePaused = false;
                    StateManager.MenuState = MenuStates.Main;                    
                    break;
            }
        }
    }
}
