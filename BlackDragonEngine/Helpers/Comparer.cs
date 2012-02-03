using System;
using System.Collections.Generic;
using BlackDragonEngine.TileEngine;

namespace BlackDragonEngine.Helpers
{
    [Serializable]
    public sealed class CoordComparer : IEqualityComparer<Coords>
    {
        #region IEqualityComparer<Coords> Members

        public bool Equals(Coords c1, Coords c2)
        {
            return c1.X == c2.X && c1.Y == c2.Y;
        }

        public int GetHashCode(Coords c)
        {
            return c.GetHashCode();
        }

        #endregion
    }
}