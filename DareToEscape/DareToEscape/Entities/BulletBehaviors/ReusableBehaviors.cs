using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackDragonEngine.Providers;
using Microsoft.Xna.Framework;

namespace DareToEscape.Entities.BulletBehaviors
{
    public static class ReusableBehaviors
    {
        public static Behavior StandardBehavior;
        public static Behavior TracingBehavior;

        public static void Initialize()
        {
            StandardBehavior = new StandardBehavior();
            TracingBehavior = new TracingBehavior();
        }
    }
}
