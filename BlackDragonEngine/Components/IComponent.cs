using BlackDragonEngine.Entities;

namespace BlackDragonEngine.Components
{
    public interface IComponent
    {
        /// <summary>
        ///     Handles messages from other components
        /// </summary>
        /// <typeparam name="T"> Type of the object thats being sent </typeparam>
        /// <param name="message"> The message that was received </param>
        /// <param name="obj"> The attachment to the message </param>
        void Receive<T>(string message, T obj);

        void Update(GameObject obj);
    }
}