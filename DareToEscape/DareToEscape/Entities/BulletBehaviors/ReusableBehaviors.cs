
namespace DareToEscape.Entities.BulletBehaviors
{
    public static class ReusableBehaviors
    {
        public static Behavior StandardBehavior;
        public static Behavior TracingBehavior;

        public static void Initialize()
        {
            StandardBehavior = new StandardBehavior();
            TracingBehavior = new TracingBehavior();
        }
    }
}
