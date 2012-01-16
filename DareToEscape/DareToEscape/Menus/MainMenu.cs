using System.IO;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Managers;
using BlackDragonEngine.Menus;
using BlackDragonEngine.Providers;
using BlackDragonEngine.TileEngine;
using DareToEscape.GameStates;
using DareToEscape.Helpers;
using DareToEscape.Managers;
using DareToEscape.Providers;
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
            MenuItems.Add(new MenuItem(Resume, FontName, File.Exists(GameVariableProvider.SaveManager.CurrentSaveFile),
                                       new Color(255, 0, 0), new Color(0, 255, 0)));
            MenuItems.Add(new MenuItem(NewGame, FontName, !File.Exists(GameVariableProvider.SaveManager.CurrentSaveFile),
                                       new Color(255, 0, 0), new Color(0, 255, 0)));
            MenuItems.Add(new MenuItem(Quit, FontName, false, new Color(255, 0, 0), new Color(0, 255, 0)));

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
                    LevelManager.LoadLevel<Map<TileCode>, TileCode>("Level1");
                    GameVariableProvider.SaveManager.Save();
                    break;
                case Resume:
                    GameStateManager.State = States.Ingame;
                    ingameManager.Activate();
                    GameVariableProvider.SaveManager.Load(VariableProvider.SaveSlot);
                    break;

                case Quit:
                    VariableProvider.Game.Exit();
                    break;
            }
        }
    }
}