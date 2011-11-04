using BlackDragonEngine.Components;
using DareToEscape.Providers;

namespace DareToEscape.Components.Entities
{
    internal class ExitGraphicsComponent : AnimatedGraphicsComponent
    {
        public ExitGraphicsComponent()
        {
            animations = AnimationDictionaryProvider.GetExitAnimations();
            currentAnimation = "Idle";
            PlayAnimation(currentAnimation);
        }
    }
}