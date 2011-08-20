using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackDragonEngine.Components;
using BlackDragonEngine.Providers;
using Microsoft.Xna.Framework.Graphics;

namespace DareToEscape.Components.Entities
{
    class CheckPointGraphicsComponent : GraphicsComponent
    {

        public CheckPointGraphicsComponent()
        { 
            texture = VariableProvider.Game.Content.Load<Texture2D>("textures/entities/checkpoint");
        }
    }
}
