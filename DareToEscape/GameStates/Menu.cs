using BlackDragonEngine;
using BlackDragonEngine.GameStates;
using DareToEscape.Managers;
using DareToEscape.Menus;

namespace DareToEscape.GameStates
{
    internal sealed class Menu : IDrawableGameState, IUpdateableGameState
    {
        private readonly IngameMenu _ingameMenu;
        private readonly MainMenu _mainMenu;

        public Menu()
        {
            _mainMenu = new MainMenu();
            _ingameMenu = new IngameMenu();
        }

        public static MenuStates MenuState { private get; set; }

        #region IDrawableGameState Members

        public bool DrawCondition
        {
            get { return GameStateManager.State == States.Menu; }
        }

        public void Draw()
        {
            switch (MenuState)
            {
                case MenuStates.Main:
                    _mainMenu.Draw();
                    break;

                case MenuStates.Ingame:
                    _ingameMenu.Draw();
                    break;
            }
        }

        #endregion

        #region IUpdateableGameState Members

        public bool UpdateCondition
        {
            get
            {
                return GameStateManager.State == States.Menu &&
                       EngineState.DialogState == DialogueStates.Inactive && !GameStateManager.PlayerDead;
            }
        }

        public bool Update()
        {
            switch (MenuState)
            {
                case MenuStates.Main:
                    _mainMenu.Update();
                    break;

                case MenuStates.Ingame:
                    _ingameMenu.Update();
                    break;
            }
            return true;
        }

        #endregion
    }
}