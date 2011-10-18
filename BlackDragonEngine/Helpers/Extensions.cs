using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BlackDragonEngine.Helpers
{
    public static class Extensions
    {
        public static float NextFloat(this Random rand)
        {
            return (float)rand.NextDouble();
        }

        public static float NextFloat(this Random rand, float min, float max)
        { 
            if(max < min)
                throw new ArgumentOutOfRangeException("max cannot be less than min");

            return (float)rand.NextDouble() * (max - min) + min;
        }

        public static Vector2 ToVector2(this Point p)
        {
            return new Vector2(p.X, p.Y);
        }

        public static Point ToPoint(this Vector2 v)
        {
            return new Point((int)v.X, (int)v.Y);
        }
    }
}
