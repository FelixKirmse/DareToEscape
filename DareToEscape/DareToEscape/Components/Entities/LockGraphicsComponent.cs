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
using BlackDragonEngine.TileEngine;

namespace DareToEscape.Components.Entities
{
    class LockGraphicsComponent : GraphicsComponent
    {
        private bool setRectangle = true;
        private bool enabled = true;
        private string keystring;
        private Color drawColor;

        public LockGraphicsComponent()
        {
            texture = VariableProvider.Game.Content.Load<Texture2D>(@"textures/entities/lock");
        }

        public override void Update(GameObject obj)
        {
            if (enabled)
            {
                if (setRectangle)
                {
                    setRectangle = false;
                    obj.CollisionRectangle = new Rectangle(-16, -16, 48, 48);
                }

                if (SaveManager<SaveState>.CurrentSaveState.Keys.Contains(keystring))
                {
                    if (obj.CollisionRectangle.Intersects(VariableProvider.CurrentPlayer.CollisionRectangle))
                    {
                        enabled = false;
                        TileMap.GetMapSquareAtPixel(obj.Position).Passable = true;
                    }
                }
                else 
                {
                    TileMap.GetMapSquareAtPixel(obj.Position).Passable = false;
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
