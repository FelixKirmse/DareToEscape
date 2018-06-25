using BlackDragonEngine.Components;
using BlackDragonEngine.Entities;

namespace DareToEscape.Components.PlayerComponents
{
    internal class PlayerSoundComponent : SoundComponent
    {
        public override void Update(GameObject obj)
        {
        }

        public override void Receive<T>(string message, T desiredPosition)
        {
            string[] messageParts = message.Split('_');

            if (messageParts[0] == "SOUND")
            {
            }
        }
    }
}