using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackDragonEngine.Components;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Managers;
using DareToEscape.Helpers;
using BlackDragonEngine.Providers;
using BlackDragonEngine.Helpers;
using Microsoft.Xna.Framework;

namespace DareToEscape.Components.Player
{
    class PlayerGeneralComponent : Component
    {
        private bool dead;        

        public PlayerGeneralComponent()
        {
            dead = false;
        }

        public override void Update(GameObject obj)
        {            
            if (dead)
            {
                SaveManager<SaveState>.Load(VariableProvider.SaveSlot);
                dead = false;
            }

            if (obj.ScreenPosition.X > 600)
                Camera.Move(new Vector2((int)obj.ScreenPosition.X, 0) - new Vector2(600, 0) );

            if (obj.ScreenPosition.Y > 300)
                Camera.Move(new Vector2(0, (int)obj.ScreenPosition.Y) - new Vector2(0, 300) );

            if (obj.ScreenPosition.X < 200)
                Camera.Move(-(new Vector2(200,0) - new Vector2((int)obj.ScreenPosition.X, 0)));

            if (obj.ScreenPosition.Y < 300)
                Camera.Move(-(new Vector2(0,300) - new Vector2(0, (int)obj.ScreenPosition.Y)));
        }

        public override void Receive<T>(string message, T obj)
        {            
        }
    }
}
