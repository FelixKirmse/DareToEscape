
namespace DareToEscape.Entities.BulletBehaviors
{
    class TracingBehavior : Behavior
    {
        public void Update(Bullet bullet)
        {                        
            bullet.Position += bullet.DirectionVectorToPlayer * bullet.BaseSpeed;
        }
    }
}
