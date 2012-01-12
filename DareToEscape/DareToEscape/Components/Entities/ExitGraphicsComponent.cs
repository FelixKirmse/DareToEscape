using BlackDragonEngine.Components;
using DareToEscape.Providers;

namespace DareToEscape.Components.Entities
{
    internal class ExitGraphicsComponent : AnimatedGraphicsComponent
    {
        public ExitGraphicsComponent()
        {
            Animations = AnimationDictionaryProvider.GetExitAnimations();
            CurrentAnimation = "Idle";
            PlayAnimation(CurrentAnimation);
        }
    }
}