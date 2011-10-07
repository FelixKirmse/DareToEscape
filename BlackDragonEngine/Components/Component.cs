using BlackDragonEngine.Entities;

namespace BlackDragonEngine.Components
{
    public abstract class Component
    {        
        public abstract void Receive<T>(string message, T obj);
        public abstract void Update(GameObject obj);
    }
}
