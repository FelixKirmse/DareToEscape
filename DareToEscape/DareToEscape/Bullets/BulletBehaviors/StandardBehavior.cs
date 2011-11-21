using Microsoft.Xna.Framework;

namespace DareToEscape.Bullets.BulletBehaviors
{
    internal class StandardBehavior : IBehavior
    {
        #region IBehavior Members

        public void Update(ref Bullet bullet)
        {
            if (bullet.LaunchSpeed > bullet.SpeedLimit)
            {
                bullet.Velocity = MathHelper.Max(bullet.SpeedLimit, bullet.Velocity + bullet.Acceleration);
            }
            else if (bullet.LaunchSpeed < bullet.SpeedLimit)
            {
                bullet.Velocity = MathHelper.Min(bullet.SpeedLimit, bullet.Velocity + bullet.Acceleration);
            }
            if (bullet.TurnSpeed != 0f)
                bullet.Direction += bullet.TurnSpeed;
            bullet.Position += bullet.DirectionVector*bullet.Velocity;
        }

        public void FreeRessources()
        {
        }

        #endregion
    }
}