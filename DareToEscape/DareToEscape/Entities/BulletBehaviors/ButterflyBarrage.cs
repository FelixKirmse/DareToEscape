namespace DareToEscape.Entities.BulletBehaviors
{
    internal class ButterflyBarrage : IBehavior
    {
        private readonly float iterator;
        private readonly int modifier;
        private int frameCounter;

        public ButterflyBarrage(int modifier, float iterator)
        {
            this.modifier = modifier;
            this.iterator = iterator;
        }

        #region IBehavior Members

        public void Update(ref Bullet bullet)
        {
            switch (frameCounter++)
            {
                case 60:
                    bullet.BaseSpeed = 2;
                    bullet.LaunchSpeed = 2;
                    bullet.TurnSpeed = .5f*modifier;
                    bullet.Acceleration = .1f;
                    bullet.SpeedLimit = 1 + iterator/4;
                    break;

                case 120:
                    bullet.TurnSpeed = 0;
                    bullet.Acceleration = 0;
                    bullet.SpeedLimit = 0;
                    break;
            }
            ReusableBehaviors.StandardBehavior.Update(ref bullet);
        }

        #endregion
    }
}