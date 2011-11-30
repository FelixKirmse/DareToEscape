using BlackDragonEngine;
using BlackDragonEngine.GameStates;
using DareToEscape.Managers;
using DareToEscape.Menus;
using Microsoft.Xna.Framework.Graphics;

namespace DareToEscape.GameStates
{
    internal class MenuManager : IDrawableGameState, IUpdateableGameState
    {
        private readonly MainMenu _mainMenu;
        private readonly IngameMenu _ingameMenu;
        public static MenuStates MenuState { private get; set; }

        public MenuManager()
        {
            _mainMenu = new MainMenu();
            _ingameMenu = new IngameMenu();
        }

        public bool UpdateCondition
        {
            get
            {
                return GameStateManager.State == States.Menu &&
                       EngineStates.DialogState == DialogueStates.Inactive && !GameStateManager.PlayerDead;
            }
        }

        public bool DrawCondition
        {
            get { return GameStateManager.State == States.Menu; }
        }

        public void Update()
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
        }

        public  void Draw(SpriteBatch spriteBatch)
        {
            switch (MenuState)
            {
                case MenuStates.Main:
                    _mainMenu.Draw(spriteBatch);
                    break;

                case MenuStates.Ingame:
                    _ingameMenu.Draw(spriteBatch);
                    break;
            }
        }
    }
}