using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackDragonEngine.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace BlackDragonEngine.Components
{
    public abstract class SoundComponent : Component
    {
        public override void Update(GameObject obj)
        { 
        }
        public override void Receive<T>(string message, T obj)
        { 
        }
    }
}
