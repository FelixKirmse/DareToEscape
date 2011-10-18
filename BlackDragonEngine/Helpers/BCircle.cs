using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BlackDragonEngine.Helpers
{
    public struct BCircle
    {
        public Vector2 Position;
        public float Radius;

        public BCircle(Vector2 position, float radius)
        {
            this.Position = position;
            this.Radius = radius;
        }

        public BCircle(float x, float y, float radius)
            : this(new Vector2(x, y), radius)
        { 
        }

        public bool Intersects(BCircle otherCircle)
        {
            float radiiSum = this.Radius * this.Radius + otherCircle.Radius * otherCircle.Radius;
            float distance;
            Vector2.DistanceSquared(ref this.Position, ref otherCircle.Position, out distance);            
            return radiiSum >= distance;
        }
    }
}
