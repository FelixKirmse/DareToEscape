using BlackDragonEngine.Entities;

namespace BlackDragonEngine.Components
{
    public abstract class SoundComponent : Component
    {
        public override void Update(GameObject obj)
        {
        }

        public override void Receive<T>(string message, T obj)
        {
        }
    }
}