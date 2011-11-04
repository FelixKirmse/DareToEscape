using BlackDragonEngine;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Managers;
using BlackDragonEngine.Menus;
using BlackDragonEngine.Providers;
using DareToEscape.Editor;
using DareToEscape.Helpers;
using DareToEscape.Managers;
using Microsoft.Xna.Framework;

namespace DareToEscape.Menus
{
    internal class IngameMenu : Menu
    {
        private const string resume = "Resume";
        private const string restartCheck = "Restart from last checkpoint";
        private const string restartLevel = "Restart level";
        private const string mapeditor = "Edit this level";
        private const string toggleFullscreen = "Toggle fullscreen";
        private const string back = "Back to main menu";

        public IngameMenu()
        {
            menuItems.Add(new MenuItem(resume, fontName, true, new Color(255, 0, 0), new Color(0, 255, 0)));
            menuItems.Add(new MenuItem(restartCheck, fontName, false, new Color(255, 0, 0), new Color(0, 255, 0)));
            menuItems.Add(new MenuItem(restartLevel, fontName, false, new Color(255, 0, 0), new Color(0, 255, 0)));
            menuItems.Add(new MenuItem(mapeditor, fontName, false, new Color(255, 0, 0), new Color(0, 255, 0)));
            menuItems.Add(new MenuItem(toggleFullscreen, fontName, false, new Color(255, 0, 0), new Color(0, 255, 0)));
            menuItems.Add(new MenuItem(back, fontName, false, new Color(255, 0, 0), new Color(0, 255, 0)));

            EnableMouseSelection = false;
            SetPositions();
        }

        public override void Update()
        {
            base.Update();
            if (InputMapper.StrictCancel)
            {
                EngineStates.GameStates = EEngineStates.Running;
                StateManager.GameState = GameStates.Ingame;
            }
        }

        public override void SelectMenuItem()
        {
            switch (SelectedItem)
            {
                case resume:
                    EngineStates.GameStates = EEngineStates.Running;
                    StateManager.GameState = GameStates.Ingame;
                    break;

                case restartCheck:
                    EngineStates.GameStates = EEngineStates.Running;
                    SaveManager<SaveState>.Load(VariableProvider.SaveSlot);
                    StateManager.GameState = GameStates.Ingame;
                    break;

                case mapeditor:
                    EngineStates.GameStates = EEngineStates.Running;
                    EditorManager.Activate(LevelManager.CurrentLevel);
                    break;

                case toggleFullscreen:
                    DareToEscape.ToggleFullScreen();
                    break;

                case back:
                    EngineStates.GameStates = EEngineStates.Running;
                    StateManager.MenuState = MenuStates.Main;
                    break;

                case restartLevel:
                    EngineStates.GameStates = EEngineStates.Running;
                    StateManager.GameState = GameStates.Ingame;
                    LevelManager.ReloadLevel();
                    SaveManager<SaveState>.Save();
                    break;
            }
        }
    }
}