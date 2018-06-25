using BlackDragonEngine.Components;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Providers;
using DareToEscape.Providers;
using Microsoft.Xna.Framework;

namespace DareToEscape.Components.Entities
{
    internal class CheckPointGraphicsComponent : AnimatedGraphicsComponent
    {
        private bool _setRectangle = true;

        public CheckPointGraphicsComponent()
        {
            Animations = AnimationDictionaryProvider.GetCheckPointAnimations();
            CurrentAnimation = "Activated";
            PlayAnimation(CurrentAnimation);
        }

        public override void Update(GameObject obj)
        {
            if (_setRectangle)
            {
                obj.CollisionRectangle = new Rectangle(0, 0, 16, 24);
                _setRectangle = false;
            }

            if (obj.CollisionRectangle.Intersects(VariableProvider.CurrentPlayer.CollisionRectangle))
                CurrentAnimation = "Deactivated";
            base.Update(obj);
        }
    }
}