using System.IO;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Managers;
using BlackDragonEngine.Menus;
using BlackDragonEngine.Providers;
using DareToEscape.Helpers;
using DareToEscape.Managers;
using DareToEscape.Providers;
using Microsoft.Xna.Framework;

namespace DareToEscape.Menus
{
    internal class MainMenu : Menu
    {
        private const string newGame = "New Game";
        private const string resume = "Resume";
        private const string tutorial = "Play tutorial";
        private const string editor = "Map Editor";
        private const string fullScreen = "Toggle Fullscreen";
        private const string quit = "Quit";

        public MainMenu()
        {
            menuItems.Add(new MenuItem(resume, fontName, File.Exists(SaveManager<SaveState>.CurrentSaveFile),
                                       new Color(255, 0, 0), new Color(0, 255, 0)));
            menuItems.Add(new MenuItem(newGame, fontName, !File.Exists(SaveManager<SaveState>.CurrentSaveFile),
                                       new Color(255, 0, 0), new Color(0, 255, 0)));
            menuItems.Add(new MenuItem(tutorial, fontName, false, new Color(255, 0, 0), new Color(0, 255, 0)));
            menuItems.Add(new MenuItem(editor, fontName, false, new Color(255, 0, 0), new Color(0, 255, 0)));
            menuItems.Add(new MenuItem(fullScreen, fontName, false, new Color(255, 0, 0), new Color(0, 255, 0)));
            menuItems.Add(new MenuItem(quit, fontName, false, new Color(255, 0, 0), new Color(0, 255, 0)));

            EnableMouseSelection = false;

            SetPositions();
        }

        public override void Update()
        {
            if (InputMapper.StrictCancel)
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
                    GameStateManager.State = States.Ingame;
                    GameVariableProvider.IngameManager.Activate();
                    LevelManager.LoadLevel("Level1");
                    SaveManager<SaveState>.Save();
                    break;
                case resume:
                    GameStateManager.State = States.Ingame;
                    GameVariableProvider.IngameManager.Activate();
                    SaveManager<SaveState>.Load(VariableProvider.SaveSlot);
                    break;

                case tutorial:
                    GameStateManager.State = States.Tutorial;
                    GameVariableProvider.IngameManager.Activate();
                    LevelManager.LoadLevel("Tutorial");
                    SaveManager<SaveState>.Save();
                    break;

                case editor:
                    GameVariableProvider.EditorManager.Activate();
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