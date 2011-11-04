namespace DareToEscape.Entities.BulletBehaviors
{
    internal class TracingBehavior : IBehavior
    {
        #region IBehavior Members

        public void Update(Bullet bullet)
        {
            bullet.Position += bullet.DirectionVectorToPlayer*bullet.BaseSpeed;
        }

        #endregion
    }
}