using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackDragonEngine.Entities;
using Microsoft.Xna.Framework;

namespace BlackDragonEngine.Components
{
    public abstract class Component
    {        
        public abstract void Receive<T>(string message, T obj);
        public abstract void Update(GameObject obj);
    }
}
