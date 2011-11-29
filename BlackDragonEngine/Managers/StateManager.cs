using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackDragonEngine.GameStates;
using Microsoft.Xna.Framework.Graphics;

namespace BlackDragonEngine.Managers
{
    public sealed class StateManager
    {
        private readonly List<IDrawableGameState> _drawableStates;
        private readonly List<IUpdateableGameState> _updateableStates;

        #region Static fields and methods
        private static readonly Dictionary<string, bool> Conditionals = new Dictionary<string, bool>();

        public static void AddConditional(string name, bool initialValue)
        {
            Conditionals.Add(name, initialValue);
        }

        public static bool GetConditional(string name)
        {
            return Conditionals[name];
        }
        #endregion

        public StateManager()
        {
            _drawableStates = new List<IDrawableGameState>();
            _updateableStates = new List<IUpdateableGameState>();
        }

        public void AddGameState(object state)
        {
            var drawableState = state as IDrawableGameState;
            if(drawableState != null)
            {
                _drawableStates.Add(drawableState);
                return;
            }
            var updateableState = state as IUpdateableGameState;
            if(updateableState != null)
            {
                _updateableStates.Add(updateableState);
                return;
            }
            throw new Exception("Passed object didn't implement any GameState-Interfaces");
        }

        public void Update()
        {
            foreach(var state in _updateableStates)
            {
                if(state.UpdateCondition)
                    state.Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(var state in _drawableStates)
            {
                if(state.DrawCondition)
                    state.Draw(spriteBatch);
            }
        }
    }
}
