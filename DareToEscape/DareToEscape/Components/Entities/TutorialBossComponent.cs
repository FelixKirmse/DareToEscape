using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using BlackDragonEngine.Providers;

namespace DareToEscape.Components.Entities
{
    class TutorialBossComponent : Boss1Component
    {
        public TutorialBossComponent()                 
        {
            texture = VariableProvider.Game.Content.Load<Texture2D>(@"textures/entities/boss1");
            waveCount = 8;
            waveTimer = 3000;
            bulletTimer = 64;
        }
    }
}
