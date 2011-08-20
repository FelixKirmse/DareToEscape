using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackDragonEngine.Components;
using DareToEscape.Components;
using DareToEscape.Components.Player;
using BlackDragonEngine.Entities;
using DareToEscape.Components.Entities;

namespace DareToEscape.Helpers
{
    static class Factory
    {
        public static GameObject CreatePlayer()
        {
            List<Component> components = new List<Component>();
            components.Add(new PlayerGraphicsComponent());
            components.Add(new PlayerInputComponent());
            components.Add(new PlayerPhysicsComponent());
            components.Add(new PlayerSoundComponent());
            components.Add(new PlayerGeneralComponent());
            return new GameObject(components);
        }

        public static GameObject CreateCheckPoint()
        {
            List<Component> components = new List<Component>();
            components.Add(new CheckPointGraphicsComponent());
            return new GameObject(components);
        }
    }
}
