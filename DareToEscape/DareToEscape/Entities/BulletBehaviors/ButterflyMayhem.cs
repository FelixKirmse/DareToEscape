namespace DareToEscape.Entities.BulletBehaviors
{
    internal class ButterflyMayhem : IBehavior
    {
        private readonly int modifier;
        private readonly float speedMod;
        private int frameCounter;

        public ButterflyMayhem(int modifier, float speedMod)
        {
            this.modifier = modifier;
            this.speedMod = speedMod;
            frameCounter = 0;
        }
        public void FreeRessources()
        {
        }

        #region IBehavior Members

        public void Update(ref Bullet bullet)
        {
            switch (frameCounter++)
            {
                case 60:
                    bullet.LaunchSpeed = 2f;
                    bullet.Velocity = 2f;
                    bullet.TurnSpeed = 1*modifier;
                    bullet.Acceleration = .2f;
                    bullet.SpeedLimit = 2 + (speedMod/3);
                    break;

                case 120:
                    bullet.TurnSpeed = 0f;
                    bullet.Acceleration = 0f;
                    bullet.SpeedLimit = 0f;
                    break;
            }
            ReusableBehaviors.StandardBehavior.Update(ref bullet);
        }

        #endregion
    }
}