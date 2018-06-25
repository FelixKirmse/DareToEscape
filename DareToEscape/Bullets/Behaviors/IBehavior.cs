namespace DareToEscape.Bullets.Behaviors
{
    public interface IBehavior
    {
        void Update(ref Bullet bullet);
        void FreeRessources();
    }
}