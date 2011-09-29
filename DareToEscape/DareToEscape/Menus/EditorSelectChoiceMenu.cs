using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackDragonEngine.Menus;
using Microsoft.Xna.Framework;
using BlackDragonEngine.Helpers;
using DareToEscape.Managers;
using BlackDragonEngine.Providers;

namespace DareToEscape.Menus
{
    class EditorSelectChoiceMenu : Menu
    {
        private const string create = "Create New Map";
        private const string load = "Load Map";

        public EditorSelectChoiceMenu()
        {
            menuItems.Add(new MenuItem(create, fontName, true, new Color(255, 0, 0), new Color(0, 255, 0)));
            menuItems.Add(new MenuItem(load, fontName, false, new Color(255, 0, 0), new Color(0, 255, 0)));
            EnableMouseSelection = false;

            SetPositions();
        }

        public override void Update()
        {
            if (InputMapper.StrictCancel)
                StateManager.MenuState = MenuStates.Main;
            base.Update();
        }

        public override void SelectMenuItem()
        {
            switch (SelectedItem)
            { 
                case create:
                    StateManager.GameState = GameStates.Editor;
                    Editor.MapEditor.Activate();
                    break;

                case load:
                    StateManager.MenuState = MenuStates.MapLoad;
                    break;
            }
        }
    }
}
