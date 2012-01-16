using BlackDragonEngine.Components;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Managers;
using BlackDragonEngine.Providers;
using BlackDragonEngine.TileEngine;
using DareToEscape.Helpers;
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

        public  void Update(GameObject obj)
        {
            if (_enabled)
            {
                if (_setRectangle)
                {
                    _setRectangle = false;
                    obj.CollisionRectangle = new Rectangle(-8, -8, 16, 16);
                }

                if (SaveManager<SaveState>.CurrentSaveState.Keys.Contains(_keystring))
                {
                    if (obj.CollisionRectangle.Intersects(VariableProvider.CurrentPlayer.CollisionRectangle))
                    {
                        _enabled = false;
                        Coords cell = _tileMap.GetCellByPixel(obj.Position);
                        _tileMap.RemoveEverythingAtCell(cell);
                    }
                }
                else
                {
                    Coords cell = _tileMap.GetCellByPixel(obj.Position);
                    _tileMap.SetPassabilityAtCell(cell, false);
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
    }
}