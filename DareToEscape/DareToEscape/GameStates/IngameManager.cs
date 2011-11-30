using BlackDragonEngine;
using BlackDragonEngine.GameStates;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Managers;
using BlackDragonEngine.Providers;
using DareToEscape.Helpers;
using DareToEscape.Managers;
using Microsoft.Xna.Framework.Graphics;

namespace DareToEscape.GameStates
{
    internal class IngameManager : IUpdateableGameState, IDrawableGameState
    {
        #region IDrawableGameState Members

        public bool DrawCondition
        {
            get
            {
                return GameStateManager.State == States.Ingame ||
                       GameStateManager.State == States.Tutorial ||
                       EngineStates.GameStates == EEngineStates.Paused || GameStateManager.PlayerDead;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            EntityManager.Draw(spriteBatch);
            LevelManager.Draw(spriteBatch);
        }

        #endregion

        #region IUpdateableGameState Members

        public bool UpdateCondition
        {
            get
            {
                return (GameStateManager.State == States.Ingame ||
                        GameStateManager.State == States.Tutorial) &&
                       EngineStates.GameStates == EEngineStates.Running && !GameStateManager.PlayerDead &&
                       EngineStates.DialogState == DialogueStates.Inactive;
            }
        }

        public bool Update()
        {
            if (InputMapper.StrictCancel)
            {
                EngineStates.GameStates = EEngineStates.Paused;
                MenuManager.MenuState = MenuStates.Ingame;
                GameStateManager.State = States.Menu;
                return false;
            }
            CodeManager.CheckPlayerCodes();
            EntityManager.Update();
            return true;
        }

        #endregion

        public void Activate()
        {
            VariableProvider.CurrentPlayer = Factory.CreatePlayer();
            EntityManager.SetPlayer();
        }
    }
}