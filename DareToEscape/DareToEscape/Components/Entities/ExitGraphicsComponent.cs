using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackDragonEngine.Components;
using BlackDragonEngine.Providers;
using Microsoft.Xna.Framework.Graphics;
using DareToEscape.Providers;
using BlackDragonEngine.Entities;
using Microsoft.Xna.Framework;

namespace DareToEscape.Components.Entities
{
    class ExitGraphicsComponent : AnimatedGraphicsComponent
    {
        public ExitGraphicsComponent()
        {
            animations = AnimationDictionaryProvider.GetExitAnimations();
            currentAnimation = "Idle";
            PlayAnimation(currentAnimation);
        }
    }
}
