using BlackDragonEngine.Components;
using BlackDragonEngine.Providers;
using DareToEscape.Providers;
using BlackDragonEngine.Entities;
using Microsoft.Xna.Framework;

namespace DareToEscape.Components.Entities
{
    class CheckPointGraphicsComponent : AnimatedGraphicsComponent
    {
        private bool setRectangle = true;

        public CheckPointGraphicsComponent()
        {
            animations = AnimationDictionaryProvider.GetCheckPointAnimations();
            currentAnimation = "Activated";
            PlayAnimation(currentAnimation);
        }

        public override void Update(GameObject obj)
        {
            if (setRectangle)
            {
                obj.CollisionRectangle = new Rectangle(0, 0, 16, 24);
                setRectangle = false;
            }

            if (obj.CollisionRectangle.Intersects(VariableProvider.CurrentPlayer.CollisionRectangle))
                currentAnimation = "Deactivated";
            base.Update(obj);
        }
    }
}
