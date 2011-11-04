namespace BlackDragonEngine.TileEngine
{
    public struct Coords
    {
        public int X;
        public int Y;

        public Coords(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Coords Left
        {
            get { return new Coords(-1, 0); }
        }

        public static Coords Right
        {
            get { return new Coords(1, 0); }
        }

        public static Coords Up
        {
            get { return new Coords(0, -1); }
        }

        public static Coords Down
        {
            get { return new Coords(0, 1); }
        }

        public override bool Equals(object obj)
        {
            return obj is Coords && this == (Coords) obj;
        }

        public override int GetHashCode()
        {
            return X ^ Y;
        }

        public static bool operator ==(Coords leftCoords, Coords rightCoords)
        {
            return (rightCoords.X == leftCoords.X && rightCoords.Y == leftCoords.Y);
        }

        public static bool operator !=(Coords leftCoords, Coords rightCoords)
        {
            return !(leftCoords == rightCoords);
        }

        public static Coords operator /(Coords coords, int divisor)
        {
            return new Coords(coords.X/divisor, coords.Y/divisor);
        }

        public static Coords operator +(Coords leftCoords, Coords rightCoords)
        {
            return new Coords(leftCoords.X + rightCoords.X, leftCoords.Y + rightCoords.Y);
        }

        public static Coords operator *(Coords coords, int multiplicator)
        {
            return new Coords(coords.X*multiplicator, coords.Y*multiplicator);
        }

        public override string ToString()
        {
            return X + "," + Y;
        }
    }
}