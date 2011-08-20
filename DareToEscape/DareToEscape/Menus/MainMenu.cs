using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackDragonEngine.Menus;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Providers;
using DareToEscape.Managers;
using BlackDragonEngine.Managers;
using Microsoft.Xna.Framework;
using System.IO;
using DareToEscape.Helpers;

namespace DareToEscape.Menus
{
    class MainMenu : Menu
    {        
        private const string newGame = "New Game";
        private const string resume = "Resume";
        private const string fullScreen = "Toggle Fullscreen";
        private const string quit = "Quit";

        public MainMenu()
        {
            menuItems.Add(new MenuItem(resume, fontName, File.Exists(SaveManager<SaveState>.CurrentSaveFile), new Color(255, 0, 0), new Color(0, 255, 0)));
            menuItems.Add(new MenuItem(newGame, fontName, !File.Exists(SaveManager<SaveState>.CurrentSaveFile), new Color(255, 0, 0), new Color(0, 255, 0)));
            menuItems.Add(new MenuItem(fullScreen, fontName, false, new Color(255, 0, 0), new Color(0, 255, 0)));
            menuItems.Add(new MenuItem(quit, fontName, false, new Color(255, 0, 0), new Color(0, 255, 0)));

            EnableMouseSelection = false;

            SetPositions();
        }      

        public override void Update()
        {
            if (InputMapper.STRICTCANCEL)
                VariableProvider.Game.Exit();
            base.Update();           
        }

        public override void SelectMenuItem()
        {
            string selectedItem;
            GetSelectedItem(out selectedItem);

            switch (selectedItem)
            {
                case newGame:
                    StateManager.GameState = GameStates.Ingame;
                    IngameManager.Activate();
                    LevelManager.LoadLevel("TestLevel");
                    SaveManager<SaveState>.Save();
                    break;
                case resume:
                    StateManager.GameState = GameStates.Ingame;
                    IngameManager.Activate();
                    SaveManager<SaveState>.Load(VariableProvider.SaveSlot);
                    break;  
              
                case fullScreen:
                    DareToEscape.ToggleFullScreen();
                    break;

                case quit:
                    VariableProvider.Game.Exit();
                    break;
            }
        }
    }
}
