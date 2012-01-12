using BlackDragonEngine;
using BlackDragonEngine.GameStates;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Managers;
using BlackDragonEngine.Providers;
using BlackDragonEngine.TileEngine;
using DareToEscape.Helpers;
using DareToEscape.Managers;
using Microsoft.Xna.Framework.Graphics;

namespace DareToEscape.GameStates
{
    internal sealed class Ingame : IUpdateableGameState, IDrawableGameState
    {
        private static Ingame _instance;
        private readonly TileMap<Map<TileCode>, TileCode> _tileMap;

        private Ingame()
        {
            _tileMap = TileMap<Map<TileCode>, TileCode>.GetInstance();
        }

        #region IDrawableGameState Members

        public bool DrawCondition
        {
            get
            {
                return GameStateManager.State == States.Ingame ||
                       GameStateManager.State == States.Tutorial ||
                       EngineState.GameState == EngineStates.Paused || GameStateManager.PlayerDead;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            EntityManager.Draw(spriteBatch);
            _tileMap.Draw(spriteBatch);
        }

        #endregion

        #region IUpdateableGameState Members

        public bool UpdateCondition
        {
            get
            {
                return (GameStateManager.State == States.Ingame ||
                        GameStateManager.State == States.Tutorial) &&
                       EngineState.GameState == EngineStates.Running && !GameStateManager.PlayerDead &&
                       EngineState.DialogState == DialogueStates.Inactive;
            }
        }

        public bool Update()
        {
            if (InputMapper.StrictCancel)
            {
                EngineState.GameState = EngineStates.Paused;
                Menu.MenuState = MenuStates.Ingame;
                GameStateManager.State = States.Menu;
                return false;
            }
            CodeManager<TileCode>.CheckPlayerCodes(_tileMap);
            EntityManager.Update();
            return true;
        }

        #endregion

        public static Ingame GetInstance()
        {
            return _instance ?? (_instance = new Ingame());
        }

        public void Activate()
        {
            VariableProvider.CurrentPlayer = Factory.CreatePlayer();
            EntityManager.SetPlayer();
        }
    }
}