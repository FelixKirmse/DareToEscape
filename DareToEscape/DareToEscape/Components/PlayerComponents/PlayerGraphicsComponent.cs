using BlackDragonEngine.Components;
using BlackDragonEngine.Entities;
using DareToEscape.Providers;

namespace DareToEscape.Components.PlayerComponents
{
    internal class PlayerGraphicsComponent : AnimatedGraphicsComponent
    {
        private bool _focused;
        private bool _onGround;

        public PlayerGraphicsComponent()
        {
            CurrentAnimation = "Idle";
            Animations = AnimationDictionaryProvider.GetPlayerAnimations();
            DrawDepth = .85f;
            _focused = false;
        }

        public override void Update(GameObject obj)
        {
            if (ReceivedAnimation == "")
            {
                if (_onGround)
                    ReceivedAnimation = "Idle";
            }

            if (ReceivedAnimation != CurrentAnimation && ReceivedAnimation != "")
            {
                PlayAnimation(ReceivedAnimation);
            }

            ReceivedAnimation = "";

            base.Update(obj);
        }

        public override void Receive<T>(string message, T obj)
        {
            string[] messageParts = message.Split('_');

            if (messageParts[0] == "GRAPHICS")
            {
                if (messageParts[1] == "SET")
                {
                    if (messageParts[2] == "ONGROUND")
                    {
                        if (obj is bool)
                            _onGround = (bool) (object) obj;
                    }

                    if (messageParts[2] == "FOCUSED")
                    {
                        if (obj is bool)
                            _focused = (bool) (object) obj;
                    }
                }
            }

            base.Receive(message, obj);
        }
    }
}