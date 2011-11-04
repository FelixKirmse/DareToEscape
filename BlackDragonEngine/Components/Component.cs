using BlackDragonEngine.Entities;

namespace BlackDragonEngine.Components
{
    public abstract class Component
    {
        /// <summary>
        /// Handles messages from other components
        /// </summary>
        /// <typeparam name="T">Type of the object thats being sent</typeparam>
        /// <param name="message">The message that was received</param>
        /// <param name="obj">The attachment to the message</param>
        public abstract void Receive<T>(string message, T obj);

        public abstract void Update(GameObject obj);
    }
}