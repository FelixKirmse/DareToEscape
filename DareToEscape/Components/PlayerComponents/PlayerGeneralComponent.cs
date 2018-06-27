using BlackDragonEngine.Components;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Helpers;
using DareToEscape.Managers;
using Microsoft.Xna.Framework;

namespace DareToEscape.Components.PlayerComponents
{
    internal class PlayerGeneralComponent : IComponent
    {
        private bool _disabled;

        #region IComponent Members

        public void Update(GameObject obj)
        {
            if (_disabled) return;
            if (obj.ScreenPosition.X > 250)
                Camera.Position += new Vector2((int) obj.ScreenPosition.X, 0) - new Vector2(250, 0);

            if (obj.ScreenPosition.Y > 200)
                Camera.Position += new Vector2(0, (int) obj.ScreenPosition.Y) - new Vector2(0, 200);

            if (obj.ScreenPosition.X < 50)
                Camera.Position += -(new Vector2(50, 0) - new Vector2((int) obj.ScreenPosition.X, 0));

            if (obj.ScreenPosition.Y < 120)
                Camera.Position += -(new Vector2(0, 120) - new Vector2(0, (int) obj.ScreenPosition.Y));
        }

        public void Receive<T>(string message, T obj)
        {
            if (message == "KILL") GameStateManager.PlayerDead = true;

            if (message == "DISABLE")
                _disabled = (bool) (object) obj;
        }

        #endregion
    }
}