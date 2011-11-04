using Microsoft.Xna.Framework;

namespace DareToEscape.Entities.BulletBehaviors
{
    internal class StandardBehavior : IBehavior
    {
        #region IBehavior Members

        public void Update(ref Bullet bullet)
        {
            if (bullet.LaunchSpeed > bullet.SpeedLimit)
            {
                bullet.BaseSpeed = MathHelper.Max(bullet.SpeedLimit, bullet.BaseSpeed + bullet.Acceleration);
            }
            else if (bullet.LaunchSpeed < bullet.SpeedLimit)
            {
                bullet.BaseSpeed = MathHelper.Min(bullet.SpeedLimit, bullet.BaseSpeed + bullet.Acceleration);
            }
            if(bullet.TurnSpeed != 0f)
                bullet.Direction += bullet.TurnSpeed;
            bullet.Position += bullet.DirectionVector*bullet.BaseSpeed;
        }

        #endregion
    }
}