using System;
using System.Collections.Generic;
using BlackDragonEngine.TileEngine;

namespace BlackDragonEngine.Helpers
{
    [Serializable]
    public class CoordComparer : IEqualityComparer<Coords>
    {
        #region IEqualityComparer<Coords> Members

        public bool Equals(Coords c1, Coords c2)
        {
            return c1 == c2;
        }

        public int GetHashCode(Coords c)
        {
            return c.GetHashCode();
        }

        #endregion
    }
}