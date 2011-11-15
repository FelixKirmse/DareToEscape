using BlackDragonEngine.Providers;

namespace DareToEscape.Entities.BulletBehaviors
{
    internal class Boss1Behavior : IBehavior
    {
        private float timer;

        #region IBehavior Members

        public void Update(ref Bullet bullet)
        {
            timer += ShortcutProvider.ElapsedMilliseconds;

            if (timer < 500f)
            {
                bullet.Position += bullet.DirectionVector*bullet.Velocity*2;
            }
            else if (timer < 1500f)
            {
                if (bullet.ChangedPosition)
                {
                    bullet.Direction = bullet.DirectionAngleToPlayer;
                }
            }
            else
            {
                ReusableBehaviors.StandardBehavior.Update(ref bullet);
            }
        }

        #endregion

        public void FreeRessources()
        {
        }
    }
}