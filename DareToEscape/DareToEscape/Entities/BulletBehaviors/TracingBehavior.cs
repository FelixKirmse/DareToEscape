namespace DareToEscape.Entities.BulletBehaviors
{
    internal class TracingBehavior : IBehavior
    {
        #region IBehavior Members

        public void Update(ref Bullet bullet)
        {
            bullet.Position += bullet.DirectionVectorToPlayer*bullet.BaseSpeed;
        }

        #endregion
    }
}