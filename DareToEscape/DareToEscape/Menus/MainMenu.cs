using System.IO;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Managers;
using BlackDragonEngine.Menus;
using BlackDragonEngine.Providers;
using DareToEscape.GameStates;
using DareToEscape.Helpers;
using DareToEscape.Managers;
using Microsoft.Xna.Framework;
using Menu = BlackDragonEngine.Menus.Menu;

namespace DareToEscape.Menus
{
    internal sealed class MainMenu : Menu
    {
        private const string NewGame = "New Game";
        private const string Resume = "Resume";
        private const string Quit = "Quit";

        public MainMenu()
        {
            menuItems.Add(new MenuItem(Resume, fontName, File.Exists(SaveManager<SaveState>.CurrentSaveFile),
                                       new Color(255, 0, 0), new Color(0, 255, 0)));
            menuItems.Add(new MenuItem(NewGame, fontName, !File.Exists(SaveManager<SaveState>.CurrentSaveFile),
                                       new Color(255, 0, 0), new Color(0, 255, 0)));
            menuItems.Add(new MenuItem(Quit, fontName, false, new Color(255, 0, 0), new Color(0, 255, 0)));

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

            Ingame ingameManager = Ingame.GetInstance();

            switch (selectedItem)
            {
                case NewGame:
                    GameStateManager.State = States.Ingame;
                    ingameManager.Activate();
                    LevelManager.LoadLevel("Level1");
                    SaveManager<SaveState>.Save();
                    break;
                case Resume:
                    GameStateManager.State = States.Ingame;
                    ingameManager.Activate();
                    SaveManager<SaveState>.Load(VariableProvider.SaveSlot);
                    break;

                case Quit:
                    VariableProvider.Game.Exit();
                    break;
            }
        }
    }
}