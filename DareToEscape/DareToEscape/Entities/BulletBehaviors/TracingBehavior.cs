namespace DareToEscape.Entities.BulletBehaviors
{
    internal class TracingBehavior : IBehavior
    {
        #region IBehavior Members

        public void Update(ref Bullet bullet)
        {
            bullet.Position += bullet.DirectionVectorToPlayer*bullet.Velocity;
        }

        #endregion

        public void FreeRessources()
        {
        }
    }
}