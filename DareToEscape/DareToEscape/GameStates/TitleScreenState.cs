using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackDragonEngine.GameStates;
using DareToEscape.Menus;
using Microsoft.Xna.Framework.Graphics;

namespace DareToEscape.GameStates
{
    class TitleScreenState : GameState
    {
        public TitleScreenState(string name)
            : base(name)
        { 
        }

        public TitleScreenState(string name, bool isActive)
            : base(name, isActive)
        { 
        }

        public override void Update()
        {
            Titlescreen.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Titlescreen.Draw(spriteBatch);
        }
    }
}
