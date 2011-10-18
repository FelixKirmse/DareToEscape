using System.Collections.Generic;
using BlackDragonEngine.Entities;
using Microsoft.Xna.Framework;
using BlackDragonEngine.Components;
using BlackDragonEngine.Providers;
using BlackDragonEngine.Helpers;

namespace DareToEscape.Entities
{
    class Player : GameObject
    {
        public Player(List<Component> components)
            : base(components)
        {
            collisionCircle = new BCircle(6, 9, 4);
        }               

        public BCircle PlayerBulletCollisionCircle
        {
            get
            {
                return CollisionCircle;
            }
        }

        public Vector2 PlayerBulletCollisionCircleCenter
        {
            get 
            {
                return CollisionCircle.Position;
            }
        }

        public static float PlayerPosX
        {
            get 
            {
                return ((Player)VariableProvider.CurrentPlayer).PlayerBulletCollisionCircleCenter.X;
            }
        }

        public static float PlayerPosY
        {
            get
            {
                return ((Player)VariableProvider.CurrentPlayer).PlayerBulletCollisionCircleCenter.Y;
            }
        }
    }
}
