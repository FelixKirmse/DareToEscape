
namespace DareToEscape.Entities.BulletBehaviors
{
    class TracingBehavior : IBehavior
    {
        public void Update(Bullet bullet)
        {                        
            bullet.Position += bullet.DirectionVectorToPlayer * bullet.BaseSpeed;
        }
    }
}
