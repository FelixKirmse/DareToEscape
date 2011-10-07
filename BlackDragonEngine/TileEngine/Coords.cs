using Microsoft.Xna.Framework;

namespace BlackDragonEngine.TileEngine
{
    public struct Coords
    {
        private int x;
        private int y;

        public int X
        {
            get { return x; }
            set 
            {
                x = (int)MathHelper.Max(0, value);
            }
        }
        public int Y 
        {
            get { return y; }
            set
            {
                y = (int)MathHelper.Max(0, value);
            }
        }   

        public Coords(int x, int y)
        {
            this.x = 0;
            this.y = 0;
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Coords))
                return false;
            return (x == ((Coords)obj).X && y == ((Coords)obj).Y);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ (x*y);
        }

        public static bool operator == (Coords leftCoords, Coords rightCoords)
        {
            return (rightCoords.X == leftCoords.X && rightCoords.Y == leftCoords.Y);
        }

        public static bool operator != (Coords leftCoords, Coords rightCoords)
        {
            return !(leftCoords == rightCoords);
        }
    }
}
