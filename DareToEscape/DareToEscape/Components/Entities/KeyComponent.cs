using BlackDragonEngine.Components;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Providers;
using BlackDragonEngine.TileEngine;
using DareToEscape.Providers;
using Microsoft.Xna.Framework;

namespace DareToEscape.Components.Entities
{
    internal class KeyComponent : IComponent
    {
        private readonly TileMap<Map<TileCode>, TileCode> _tileMap;
        private bool _enabled = true;
        private string _keystring;
        private bool _setRectangle = true;

        public KeyComponent()
        {
            _tileMap = TileMap<Map<TileCode>, TileCode>.GetInstance();
        }

        #region IComponent Members

        public void Update(GameObject obj)
        {
            if (_enabled)
            {
                if (_setRectangle)
                {
                    _setRectangle = false;
                    obj.CollisionRectangle = new Rectangle(0, -8, 8, 16);
                }

                if (obj.CollisionRectangle.Intersects(VariableProvider.CurrentPlayer.CollisionRectangle))
                {
                    Coords cell = _tileMap.GetCellByPixel(obj.Position);
                    _tileMap.RemoveEverythingAtCell(cell);
                    _enabled = false;
                    GameVariableProvider.SaveManager.CurrentSaveState.Keys.Add(_keystring);
                }
            }
        }

        public void Receive<T>(string message, T obj)
        {
            if (message == "KEYSTRING")
            {
                if (obj is string)
                {
                    _keystring = (string) (object) obj;
                }
            }
        }

        #endregion
    }
}