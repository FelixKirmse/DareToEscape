using BlackDragonEngine.Components;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Providers;
using BlackDragonEngine.TileEngine;
using DareToEscape.Providers;
using Microsoft.Xna.Framework;

namespace DareToEscape.Components.Entities
{
    internal class LockComponent : IComponent
    {
        private readonly TileMap<Map<TileCode>, TileCode> _tileMap;
        private bool _enabled = true;
        private string _keystring;
        private bool _setRectangle = true;

        public LockComponent()
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
                    obj.CollisionRectangle = new Rectangle(-2, -2, 12, 12);
                }

                if (obj.CollisionRectangle.Intersects(VariableProvider.CurrentPlayer.CollisionRectangle) &&
                    GameVariableProvider.SaveManager.CurrentSaveState.Keys.Contains(_keystring))
                {
                    _enabled = false;
                    var cell = _tileMap.GetCellByPixel(obj.Position);
                    _tileMap.RemoveEverythingAtCell(cell);
                }
            }
        }

        public void Receive<T>(string message, T obj)
        {
            if (message == "KEYSTRING")
                if (obj is string)
                    _keystring = (string) (object) obj;
        }

        #endregion
    }
}