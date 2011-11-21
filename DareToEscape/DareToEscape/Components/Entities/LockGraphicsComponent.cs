using BlackDragonEngine.Components;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Managers;
using BlackDragonEngine.Providers;
using BlackDragonEngine.TileEngine;
using DareToEscape.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DareToEscape.Components.Entities
{
    internal class LockGraphicsComponent : GraphicsComponent
    {
        private Color drawColor;
        private bool enabled = true;
        private string keystring;
        private bool setRectangle = true;

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
                        Vector2 cell = TileMap.GetCellByPixel(obj.Position);
                        TileMap.SetPassabilityAtCell(cell, true);
                    }
                }
                else
                {
                    Vector2 cell = TileMap.GetCellByPixel(obj.Position);
                    TileMap.SetPassabilityAtCell(cell, false);
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
                    keystring = (string) (object) obj;
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