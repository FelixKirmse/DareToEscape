using BlackDragonEngine.Providers;

namespace DareToEscape.Entities.BulletBehaviors
{
    class Boss1Behavior : Behavior
    {
        private float timer;

        public void Update(Bullet bullet)
        {
            timer += ShortcutProvider.ElapsedMilliseconds;

            if (timer < 500f)
            {
                bullet.Position += bullet.DirectionVector * bullet.BaseSpeed * 2;
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
                ReusableBehaviors.StandardBehavior.Update(bullet);
            }
        }
    }
}
