using Microsoft.Xna.Framework;

namespace DareToEscape.Entities.BulletBehaviors
{
    class PlayerTrap1 : IBehavior
    {
        private int frameCounter = 0;
        private float angle;

        public PlayerTrap1(float angle)
        {
            this.angle = angle;
        }

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
    }

    class PlayerTrap2 : IBehavior
    {
        private int frameCounter = 0;
        private float angle;

        public PlayerTrap2(float angle)
        {
            this.angle = angle;
        }
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
    }
}
