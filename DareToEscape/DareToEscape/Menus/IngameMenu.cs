﻿using BlackDragonEngine;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Managers;
using BlackDragonEngine.Menus;
using BlackDragonEngine.Providers;
using DareToEscape.GameStates;
using DareToEscape.Helpers;
using DareToEscape.Managers;
using Microsoft.Xna.Framework;
using Menu = BlackDragonEngine.Menus.Menu;
using DMenu = DareToEscape.GameStates.Menu;

namespace DareToEscape.Menus
{
    internal sealed class IngameMenu : Menu
    {
        private const string Resume = "Resume";
        private const string RestartCheck = "Restart from last checkpoint";
        private const string RestartLevel = "Restart level";
        private const string Back = "Back to main menu";

        public IngameMenu()
        {
            menuItems.Add(new MenuItem(Resume, fontName, true, new Color(255, 0, 0), new Color(0, 255, 0)));
            menuItems.Add(new MenuItem(RestartCheck, fontName, false, new Color(255, 0, 0), new Color(0, 255, 0)));
            menuItems.Add(new MenuItem(RestartLevel, fontName, false, new Color(255, 0, 0), new Color(0, 255, 0)));
            menuItems.Add(new MenuItem(Back, fontName, false, new Color(255, 0, 0), new Color(0, 255, 0)));

            EnableMouseSelection = false;
            SetPositions();
        }

        public override void Update()
        {
            base.Update();
            if (InputMapper.StrictCancel)
            {
                EngineStates.GameStates = EEngineStates.Running;
                GameStateManager.State = States.Ingame;
            }
        }

        public override void SelectMenuItem()
        {
            switch (SelectedItem)
            {
                case Resume:
                    EngineStates.GameStates = EEngineStates.Running;
                    GameStateManager.State = States.Ingame;
                    break;

                case RestartCheck:
                    EngineStates.GameStates = EEngineStates.Running;
                    SaveManager<SaveState>.Load(VariableProvider.SaveSlot);
                    GameStateManager.State = States.Ingame;
                    break;

                case Back:
                    EngineStates.GameStates = EEngineStates.Running;
                    DMenu.MenuState = MenuStates.Main;
                    break;

                case RestartLevel:
                    EngineStates.GameStates = EEngineStates.Running;
                    GameStateManager.State = States.Ingame;
                    LevelManager.ReloadLevel();
                    SaveManager<SaveState>.Save();
                    break;
            }
        }
    }
}