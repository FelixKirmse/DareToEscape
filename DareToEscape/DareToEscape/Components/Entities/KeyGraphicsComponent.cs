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
        private Color drawColor;

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
            if (enabled)
            {
                spriteBatch.Draw(
                texture,
                obj.ScreenPosition,
                null,
                drawColor,
                0f,
                Vector2.Zero,
                1f,
                SpriteEffects.None,
                drawDepth);
            }
        }

        public override void Receive<T>(string message, T obj)
        {
            if (message == "KEYSTRING")
            {
                if (obj is string)
                {
                    keystring = (string)(object)obj;
                    switch (keystring)
                    { 
                        case "RED":
                            drawColor = Color.PaleVioletRed;
                            break;
                        
                        case "BLUE":
                            drawColor = Color.Blue;
                            break;

                        case "YELLOW":
                            drawColor = Color.Yellow;
                            break;

                        case "GREEN":
                            drawColor = Color.LightGreen;
                            break;

                        default:
                            drawColor = Color.White;
                            break;
                    }
                }
                    
            }
        }
    }
}
