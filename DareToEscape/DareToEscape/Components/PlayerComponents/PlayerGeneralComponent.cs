using BlackDragonEngine.Components;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Helpers;
using Microsoft.Xna.Framework;
using DareToEscape.Managers;

namespace DareToEscape.Components.PlayerComponents
{
    class PlayerGeneralComponent : Component
    {   
        public override void Update(GameObject obj)
        {    
            if (obj.ScreenPosition.X > 1000)
                Camera.Move(new Vector2((int)obj.ScreenPosition.X, 0) - new Vector2(1000, 0) );

            if (obj.ScreenPosition.Y > 350)
                Camera.Move(new Vector2(0, (int)obj.ScreenPosition.Y) - new Vector2(0, 350) );

            if (obj.ScreenPosition.X < 300)
                Camera.Move(-(new Vector2(300,0) - new Vector2((int)obj.ScreenPosition.X, 0)));

            if (obj.ScreenPosition.Y < 350)
                Camera.Move(-(new Vector2(0,350) - new Vector2(0, (int)obj.ScreenPosition.Y)));
        }

        public override void Receive<T>(string message, T obj)
        {
            if (message == "KILL")
            {                
                StateManager.PlayerDead = true;
            }
        }
    }
}