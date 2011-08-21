using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackDragonEngine.Components;
using BlackDragonEngine.Providers;
using Microsoft.Xna.Framework.Graphics;
using BlackDragonEngine.Managers;
using BlackDragonEngine.Entities;
using Microsoft.Xna.Framework;
using DareToEscape.Helpers;

namespace DareToEscape.Components.Entities
{
    class KeyGraphicsComponent : GraphicsComponent
    {
        private bool setRectangle = true;
        private bool enabled = true;
        private string keystring;

        public KeyGraphicsComponent()
        {
            texture = VariableProvider.Game.Content.Load<Texture2D>(@"textures/entities/key");
        }

        public override void Update(GameObject obj)
        {
            if (enabled)
            {
                if (setRectangle)
                {
                    setRectangle = false;
                    obj.CollisionRectangle = new Rectangle(0, -16, 16, 32);
                }

                if (obj.CollisionRectangle.Intersects(VariableProvider.CurrentPlayer.CollisionRectangle))
                {
                    enabled = false;
                    SaveManager<SaveState>.CurrentSaveState.Keys.Add(keystring);
                }
            }            
        }

        public override void Draw(GameObject obj, SpriteBatch spriteBatch)
        {
            if(enabled)
                base.Draw(obj, spriteBatch);
        }

        public override void Receive<T>(string message, T obj)
        {
            if (message == "KEYSTRING")
            {
                if (obj is string)
                    keystring = (string)(object)obj;
            }
        }
    }
}
