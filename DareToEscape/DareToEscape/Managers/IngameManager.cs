using BlackDragonEngine;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Managers;
using BlackDragonEngine.Providers;
using DareToEscape.Helpers;
using Microsoft.Xna.Framework.Graphics;

namespace DareToEscape.Managers
{
    internal static class IngameManager
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