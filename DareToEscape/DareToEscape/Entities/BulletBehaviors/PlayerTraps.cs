using Microsoft.Xna.Framework;

namespace DareToEscape.Entities.BulletBehaviors
{
    class PlayerTrap1 : Behavior
    {
        public int counter = 0;
        public void Update(Bullet bullet)
        {
            bullet.Active = true;
            if (counter < 110)
            {
                bullet.Position += bullet.DirectionVector * -3f;
                bullet.Direction += MathHelper.ToRadians(.1f);
            }
            else if (counter > 380)
            {
                bullet.Position += bullet.DirectionVector * -1f;
                bullet.Direction += MathHelper.ToRadians(.2f);
            }
            ++counter;
        }
    }

    class PlayerTrap2 : Behavior
    {
        public int counter = 0;
        public void Update(Bullet bullet)
        {
            bullet.Active = true;
            if (counter < 110)
            {
                bullet.Position += bullet.DirectionVector * -2f;
                bullet.Direction += MathHelper.ToRadians(.05f);
            }
            else if (counter > 380)
            {
                bullet.Position += bullet.DirectionVector * 1f;
                bullet.Direction += MathHelper.ToRadians(.05f);
            }
            ++counter;
        }
    }
}
