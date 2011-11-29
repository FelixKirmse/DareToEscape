namespace DareToEscape.Bullets.Behaviors
{
    public static class ReusableBehaviors
    {
        public static IBehavior StandardBehavior;

        public static void Initialize()
        {
            StandardBehavior = new StandardBehavior();
        }
    }
}