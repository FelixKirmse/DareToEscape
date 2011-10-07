using Microsoft.Xna.Framework.Graphics;

namespace BlackDragonEngine.GameStates
{
    public abstract class GameState
    {
        private bool isActive;

        public bool IsActive 
        {
            get { return isActive; }
            set
            {
                isActive = value;
                if (isActive)
                    OnActivate();
                else
                    OnDeactivate();
            }
        }

        public string Name { get; private set; }

        private GameState()
        {
        }

        public GameState(string name, bool isActive)
        {
            IsActive = isActive;
            Name = name;
        }

        public GameState(string name)
            : this(name, false) 
        {
        }

        public virtual void Update()
        { 
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        { 
        }

        protected virtual void OnActivate()
        { 
        }

        protected virtual void OnDeactivate()
        { 
        }        
    }
}
