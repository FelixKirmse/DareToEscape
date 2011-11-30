using System.Collections.Generic;
using BlackDragonEngine.Components;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Providers;
using Microsoft.Xna.Framework;

namespace DareToEscape.Entities
{
    internal sealed class Player : GameObject
    {
        public Player(List<IComponent> components)
            : base(components)
        {
            collisionCircle = new BCircle(6, 9, 4);
        }

        public BCircle PlayerBulletCollisionCircle
        {
            get { return CollisionCircle; }
        }

        public Vector2 PlayerBulletCollisionCircleCenter
        {
            get { return CollisionCircle.Position; }
        }

        public static float PlayerPosX
        {
            get { return ((Player) VariableProvider.CurrentPlayer).PlayerBulletCollisionCircleCenter.X; }
        }

        public static float PlayerPosY
        {
            get { return ((Player) VariableProvider.CurrentPlayer).PlayerBulletCollisionCircleCenter.Y; }
        }
    }
}