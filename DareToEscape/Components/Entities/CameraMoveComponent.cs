using System.Collections.Generic;
using BlackDragonEngine.Components;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Providers;
using BlackDragonEngine.TileEngine;
using DareToEscape.Components.PlayerComponents;
using Microsoft.Xna.Framework;

namespace DareToEscape.Components.Entities
{
    public sealed class CameraMoveComponent : IComponent
    {
        private static readonly Dictionary<string, CameraMoveComponent> Instances =
            new Dictionary<string, CameraMoveComponent>();

        private readonly Vector2 _direction;
        private int _frameCount;

        private CameraMoveComponent(string pointName, int frameCount)
        {
            foreach (var codes in TileMap<Map<TileCode>, TileCode>.GetInstance().Map.Codes)
            foreach (var code in codes.Value)
                if (code.Code == TileCodes.CameraFocusPoint && code.Message.Equals(pointName))
                    _direction = (new Vector2(codes.Key.X * 8, codes.Key.Y * 8) - Camera.Position) / frameCount;
            _frameCount = frameCount;
            foreach (var component in VariableProvider.CurrentPlayer.Components)
                if (component is PlayerGeneralComponent)
                    component.Receive("DISABLE", true);
        }

        public static CameraMoveComponent GetInstance(string pointName, int frameCount)
        {
            if (Instances.ContainsKey(pointName))
                return Instances[pointName];
            Instances.Add(pointName, new CameraMoveComponent(pointName, frameCount));
            return Instances[pointName];
        }

        #region IComponent Members

        public void Receive<T>(string message, T obj)
        {
        }

        public void Update(GameObject obj)
        {
            if (_frameCount > 0)
            {
                --_frameCount;
                Camera.ForcePosition += _direction;
            }
        }

        #endregion
    }
}