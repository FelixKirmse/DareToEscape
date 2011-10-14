using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackDragonEngine.Helpers
{
    public static class RandomExtensions
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
    }
}
