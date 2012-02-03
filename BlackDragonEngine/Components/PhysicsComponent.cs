using BlackDragonEngine.Entities;

namespace BlackDragonEngine.Components
{
    public abstract class PhysicsComponent : IComponent
    {
        #region IComponent Members

        public virtual void Update(GameObject obj)
        {
        }

        public virtual void Receive<T>(string message, T obj)
        {
        }

        #endregion
    }
}