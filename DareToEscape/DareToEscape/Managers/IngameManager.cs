using Microsoft.Xna.Framework.Graphics;
using DareToEscape.Helpers;
using BlackDragonEngine.Providers;
using BlackDragonEngine.Managers;
using BlackDragonEngine.Helpers;

namespace DareToEscape.Managers
{
    static class IngameManager
    {
        public static void Activate()
        {
            VariableProvider.CurrentPlayer = Factory.CreatePlayer();
            EntityManager.SetPlayer();
        }

        public static void Update()
        {
            if (!StateManager.GamePaused)
            {
                if (InputMapper.StrictCancel)
                {
                    StateManager.GamePaused = true;
                    StateManager.MenuState = MenuStates.Ingame;
                    StateManager.GameState = GameStates.Menu;
                    return;
                }
                CodeManager.CheckPlayerCodes();
                EntityManager.Update();                
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            EntityManager.Draw(spriteBatch);           
        }        
    }
}
