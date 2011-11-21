using BlackDragonEngine.Components;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Helpers;
using DareToEscape.Managers;
using Microsoft.Xna.Framework;

namespace DareToEscape.Components.PlayerComponents
{
    internal class PlayerGeneralComponent : IComponent
    {
        #region IComponent Members

        public void Update(GameObject obj)
        {
            if (obj.ScreenPosition.X > 1000)
                Camera.Position += (new Vector2((int) obj.ScreenPosition.X, 0) - new Vector2(1000, 0));

            if (obj.ScreenPosition.Y > 350)
                Camera.Position += (new Vector2(0, (int) obj.ScreenPosition.Y) - new Vector2(0, 350));

            if (obj.ScreenPosition.X < 300)
                Camera.Position += (-(new Vector2(300, 0) - new Vector2((int) obj.ScreenPosition.X, 0)));

            if (obj.ScreenPosition.Y < 350)
                Camera.Position += (-(new Vector2(0, 350) - new Vector2(0, (int) obj.ScreenPosition.Y)));
        }

        public void Receive<T>(string message, T obj)
        {
            if (message == "KILL")
            {
                StateManager.PlayerDead = true;
            }
        }

        #endregion
    }
}