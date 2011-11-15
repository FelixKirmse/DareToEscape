namespace DareToEscape.Entities.BulletBehaviors
{
    internal class StarBarrage : IBehavior
    {
        private readonly int modifier;
        private int counter;

        public StarBarrage(int modifier)
        {
            this.modifier = modifier;
            counter = 0;
        }

        #region IBehavior Members

        public void Update(ref Bullet bullet)
        {
            if (counter == modifier)
            {
                bullet.Acceleration = .1f;
                bullet.SpeedLimit = 3f;
                bullet.Velocity = 1f;
                bullet.LaunchSpeed = 1f;
            }
            ReusableBehaviors.StandardBehavior.Update(ref bullet);
            counter++;
        }

        #endregion

        public void FreeRessources()
        {
        }
    }
}