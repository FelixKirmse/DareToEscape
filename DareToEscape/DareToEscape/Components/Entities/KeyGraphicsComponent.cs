using BlackDragonEngine.Components;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Managers;
using BlackDragonEngine.Providers;
using DareToEscape.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DareToEscape.Components.Entities
{
    internal class KeyGraphicsComponent : GraphicsComponent
    {
        private Color drawColor;
        private bool enabled = true;
        private string keystring;
        private bool setRectangle = true;

        public KeyGraphicsComponent()
        {
            Texture = VariableProvider.Game.Content.Load<Texture2D>(@"textures/entities/key");
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

        public override void Draw(GameObject obj)
        {
            if (enabled)
            {
                SpriteBatch.Draw(
                    Texture,
                    obj.ScreenPosition,
                    null,
                    drawColor,
                    0f,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    DrawDepth);
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