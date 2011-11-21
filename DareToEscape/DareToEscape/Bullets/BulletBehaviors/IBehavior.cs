namespace DareToEscape.Bullets.BulletBehaviors
{
    public interface IBehavior
    {
        void Update(ref Bullet bullet);
        void FreeRessources();
    }
}