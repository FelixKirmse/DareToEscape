using System.Collections.Generic;
using BlackDragonEngine.Components;

namespace BlackDragonEngine.Entities
{
    public interface IGameObject
    {
        List<IComponent> Components { get; }
        void Update();
        void Draw();
    }
}