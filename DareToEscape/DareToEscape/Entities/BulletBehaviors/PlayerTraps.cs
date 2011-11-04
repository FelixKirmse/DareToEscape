namespace DareToEscape.Entities.BulletBehaviors
{
    internal class PlayerTrap1 : IBehavior
    {
        private readonly float angle;
        private int frameCounter;

        public PlayerTrap1(float angle)
        {
            this.angle = angle;
        }

        #region IBehavior Members

        public void Update(Bullet bullet)
        {
            switch (frameCounter++)
            {
                case 110:
                    bullet.BaseSpeed = 0;
                    bullet.TurnSpeed = 0;
                    bullet.Direction = angle;
                    bullet.Acceleration = 0;
                    break;

                case 380:
                    bullet.BaseSpeed = -1;
                    bullet.TurnSpeed = .2f;
                    bullet.Direction = angle;
                    break;

                case 700:
                    bullet.TurnSpeed = 0;
                    bullet.LaunchSpeed = 0;
                    bullet.Acceleration = -.5f;
                    bullet.SpeedLimit = -3;
                    break;
            }
            ReusableBehaviors.StandardBehavior.Update(bullet);
        }

        #endregion
    }

    internal class PlayerTrap2 : IBehavior
    {
        private readonly float angle;
        private int frameCounter;

        public PlayerTrap2(float angle)
        {
            this.angle = angle;
        }

        #region IBehavior Members

        public void Update(Bullet bullet)
        {
            switch (frameCounter++)
            {
                case 110:
                    bullet.BaseSpeed = 0;
                    bullet.TurnSpeed = 0;
                    bullet.Direction = angle;
                    bullet.Acceleration = 0;
                    break;

                case 380:
                    bullet.BaseSpeed = 1;
                    bullet.LaunchSpeed = 1;
                    bullet.TurnSpeed = .05f;
                    bullet.Direction = angle;
                    break;

                case 700:
                    bullet.TurnSpeed = 0;
                    bullet.Acceleration = .5f;
                    bullet.SpeedLimit = 3;
                    break;
            }
            ReusableBehaviors.StandardBehavior.Update(bullet);
        }

        #endregion
    }
}