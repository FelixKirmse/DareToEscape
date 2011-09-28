using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackDragonEngine.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlackDragonEngine.Managers
{
    public class GameStateManager
    {
        private List<GameState> gameStates;

        public GameState ActiveState
        {
            get
            {
                foreach (GameState state in gameStates)
                {
                    if (state.IsActive)
                    { 
                        return state;
                    } 
                }
                return null;
            }
        }

        public void Push(GameState state)
        {
            gameStates.Add(state);
        }

        public void Pop()
        {
            gameStates.Remove(ActiveState);
        }

        public void ActivateState(string name)
        {
            ActiveState.IsActive = false;
            for (int i = 0; i < gameStates.Count; ++i)
            {
                if (gameStates[i].Name == name)               
                    gameStates[i].IsActive = true;
            }
        }

        public void Update()
        {
            ActiveState.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            ActiveState.Draw(spriteBatch);
        }
    }
}
