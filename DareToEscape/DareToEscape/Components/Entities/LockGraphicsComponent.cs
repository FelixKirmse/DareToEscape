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
        private readonly TileMap<Map<TileCode>, TileCode> _tileMap;
        private Color _drawColor;
        private bool _enabled = true;
        private string _keystring;
        private bool _setRectangle = true;

        public LockGraphicsComponent()
        {
            texture = VariableProvider.Game.Content.Load<Texture2D>(@"textures/entities/lock");
            _tileMap = TileMap<Map<TileCode>, TileCode>.GetInstance();
        }

        public override void Update(GameObject obj)
        {
            if (_enabled)
            {
                if (_setRectangle)
                {
                    _setRectangle = false;
                    obj.CollisionRectangle = new Rectangle(-16, -16, 48, 48);
                }

                if (SaveManager<SaveState>.CurrentSaveState.Keys.Contains(_keystring))
                {
                    if (obj.CollisionRectangle.Intersects(VariableProvider.CurrentPlayer.CollisionRectangle))
                    {
                        _enabled = false;
                        Vector2 cell = _tileMap.GetCellByPixel(obj.Position);
                        _tileMap.SetPassabilityAtCell(cell, true);
                    }
                }
                else
                {
                    Vector2 cell = _tileMap.GetCellByPixel(obj.Position);
                    _tileMap.SetPassabilityAtCell(cell, false);
                }
            }
        }

        public override void Draw(GameObject obj, SpriteBatch spriteBatch)
        {
            if (_enabled)
            {
                spriteBatch.Draw(
                    texture,
                    obj.ScreenPosition,
                    null,
                    _drawColor,
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
                    _keystring = (string) (object) obj;
                    switch (_keystring)
                    {
                        case "RED":
                            _drawColor = Color.PaleVioletRed;
                            break;

                        case "BLUE":
                            _drawColor = Color.Blue;
                            break;

                        case "YELLOW":
                            _drawColor = Color.Yellow;
                            break;

                        case "GREEN":
                            _drawColor = Color.LightGreen;
                            break;

                        default:
                            _drawColor = Color.White;
                            break;
                    }
                }
            }
        }
    }
}