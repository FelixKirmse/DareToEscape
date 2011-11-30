﻿using BlackDragonEngine;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Managers;
using BlackDragonEngine.Menus;
using BlackDragonEngine.Providers;
using DareToEscape.GameStates;
using DareToEscape.Helpers;
using DareToEscape.Managers;
using Microsoft.Xna.Framework;

namespace DareToEscape.Menus
{
    internal sealed class IngameMenu : Menu
    {
        private const string Resume = "Resume";
        private const string RestartCheck = "Restart from last checkpoint";
        private const string RestartLevel = "Restart level";
        private const string Mapeditor = "Edit this level";
        private const string ToggleFullscreen = "Toggle fullscreen";
        private const string Back = "Back to main menu";

        public IngameMenu()
        {
            menuItems.Add(new MenuItem(Resume, fontName, true, new Color(255, 0, 0), new Color(0, 255, 0)));
            menuItems.Add(new MenuItem(RestartCheck, fontName, false, new Color(255, 0, 0), new Color(0, 255, 0)));
            menuItems.Add(new MenuItem(RestartLevel, fontName, false, new Color(255, 0, 0), new Color(0, 255, 0)));
            menuItems.Add(new MenuItem(Mapeditor, fontName, false, new Color(255, 0, 0), new Color(0, 255, 0)));
            menuItems.Add(new MenuItem(ToggleFullscreen, fontName, false, new Color(255, 0, 0), new Color(0, 255, 0)));
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

                case Mapeditor:
                    EngineStates.GameStates = EEngineStates.Running;
                    EditorManager.GetInstance().Activate(LevelManager.CurrentLevel);
                    break;

                case ToggleFullscreen:
                    DareToEscape.ToggleFullScreen();
                    break;

                case Back:
                    EngineStates.GameStates = EEngineStates.Running;
                    MenuManager.MenuState = MenuStates.Main;
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