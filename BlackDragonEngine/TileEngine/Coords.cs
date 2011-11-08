using System;

namespace BlackDragonEngine.TileEngine
{
    [Serializable]
    public sealed class Coords
    {
        public readonly int X;
        public readonly int Y;

        public Coords(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Coords Left
        {
            get { return new Coords(X - 1, Y); }
        }

        public Coords Right
        {
            get { return new Coords(X + 1, Y); }
        }

        public Coords Up
        {
            get { return new Coords(X, Y - 1); }
        }

        public Coords Down
        {
            get { return new Coords(X, Y + 1); }
        }

        public Coords UpLeft
        {
            get { return new Coords(X - 1, Y - 1); }
        }

        public Coords UpRight
        {
            get { return new Coords(X + 1, Y - 1); }
        }

        public Coords DownLeft
        {
            get { return new Coords(X - 1, Y + 1); }
        }

        public Coords DownRight
        {
            get { return new Coords(X + 1, Y + 1); }
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