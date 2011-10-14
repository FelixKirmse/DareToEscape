using Microsoft.Xna.Framework;

namespace DareToEscape.Entities.BulletBehaviors
{
    class StandardBehavior : IBehavior
    {
        public void Update(Bullet bullet)
        {
            if (bullet.LaunchSpeed > bullet.SpeedLimit)
            {
                bullet.BaseSpeed = MathHelper.Max(bullet.SpeedLimit, bullet.BaseSpeed + bullet.Acceleration);                
            }
            else if (bullet.LaunchSpeed < bullet.SpeedLimit)
            {
                bullet.BaseSpeed = MathHelper.Min(bullet.SpeedLimit, bullet.BaseSpeed + bullet.Acceleration);
            }            
            bullet.Direction += bullet.TurnSpeed;            
            bullet.Position += bullet.DirectionVector * bullet.BaseSpeed;
        }
    }
}
