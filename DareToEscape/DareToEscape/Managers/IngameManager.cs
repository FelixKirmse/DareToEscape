using Microsoft.Xna.Framework.Graphics;
using DareToEscape.Helpers;
using BlackDragonEngine.Providers;
using BlackDragonEngine.Managers;
using BlackDragonEngine.Helpers;
using BlackDragonEngine;

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
            if (EngineStates.GameStates == EEngineStates.Running)
            {
                if (InputMapper.StrictCancel)
                {
                    EngineStates.GameStates = EEngineStates.Paused;
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
