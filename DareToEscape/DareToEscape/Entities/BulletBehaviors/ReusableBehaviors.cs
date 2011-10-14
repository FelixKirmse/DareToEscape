
namespace DareToEscape.Entities.BulletBehaviors
{
    public static class ReusableBehaviors
    {
        public static IBehavior StandardBehavior;
        public static IBehavior TracingBehavior;

        public static void Initialize()
        {
            StandardBehavior = new StandardBehavior();
            TracingBehavior = new TracingBehavior();
        }
    }
}
