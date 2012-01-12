using System;
using System.Collections.Generic;
using BlackDragonEngine.GameStates;
using Microsoft.Xna.Framework.Graphics;

namespace BlackDragonEngine.Managers
{
    public abstract class StateManager
    {
        private readonly List<IDrawableGameState> _drawableStates;
        private readonly List<IUpdateableGameState> _updateableStates;

        protected StateManager()
        {
            _drawableStates = new List<IDrawableGameState>();
            _updateableStates = new List<IUpdateableGameState>();
        }

        public void AddGameState(object state)
        {
            bool implementedInterface = false;
            var drawableState = state as IDrawableGameState;
            if (drawableState != null)
            {
                _drawableStates.Add(drawableState);
                implementedInterface = true;
            }
            var updateableState = state as IUpdateableGameState;
            if (updateableState != null)
            {
                _updateableStates.Add(updateableState);
                implementedInterface = true;
            }
            if (!implementedInterface)
                throw new ArgumentException("Object " + state + " didn't implement any of the required interfaces");
        }

        public void Update()
        {
            foreach (var state in _updateableStates)
            {
                if (state.UpdateCondition)
                    if (!state.Update()) return;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var state in _drawableStates)
            {
                if (state.DrawCondition)
                    state.Draw(spriteBatch);
            }
        }
    }
}