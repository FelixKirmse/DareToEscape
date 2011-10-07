using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackDragonEngine.Entities;
using Microsoft.Xna.Framework;
using BlackDragonEngine.Components;
using BlackDragonEngine.Providers;

namespace DareToEscape.Entities
{
    class Player : GameObject
    {
        public Player(List<Component> components)
            : base(components)
        {
        }

        public Rectangle PlayerBulletCollisionRect
        {
            get
            {
                return new Rectangle((int)position.X + 6, (int)position.Y + 9, 4, 4);
            }
        }

        public Vector2 PlayerBulletCollisionRectCenter
        {
            get 
            {
                return new Vector2((PlayerBulletCollisionRect.Right + PlayerBulletCollisionRect.Left) / 2, (PlayerBulletCollisionRect.Bottom + PlayerBulletCollisionRect.Top) / 2);
            }
        }
    }
}
