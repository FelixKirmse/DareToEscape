using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackDragonEngine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BlackDragonEngine.Providers;
using BlackDragonEngine.Entities;
using DareToEscape.Providers;

namespace DareToEscape.Components.Entities
{
    class BossKillerComponent : GraphicsComponent
    {
        public bool setRectangle = true;
        public bool enabled = true;

        public BossKillerComponent()
        {
            texture = VariableProvider.Game.Content.Load<Texture2D>("textures/entities/bosskiller");
        }

        public override void Update(GameObject obj)
        {
            if (enabled)
            {
                if (setRectangle)
                {
                    obj.CollisionRectangle = new Rectangle(6, 5, 39, 39);
                }

                if (VariableProvider.CurrentPlayer.CollisionRectangle.Intersects(obj.CollisionRectangle))
                {
                    enabled = false;
                    GameVariableProvider.Boss.Send<string>("INACTIVE", null);                    
                }
            }
        }

        public override void Draw(GameObject obj, SpriteBatch spriteBatch)
        {
            if(enabled)
                base.Draw(obj, spriteBatch);
        }
    }
}
