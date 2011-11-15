using System;
using BlackDragonEngine.Providers;

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
            get { return VariableProvider.CoordList[X - 1, Y]; }
        }

        public Coords Right
        {
            get { return VariableProvider.CoordList[X + 1, Y ]; }
        }

        public Coords Up
        {
            get { return VariableProvider.CoordList[X, Y - 1]; }
        }

        public Coords Down
        {
            get { return VariableProvider.CoordList[X, Y + 1]; }
        }

        public Coords UpLeft
        {
            get { return VariableProvider.CoordList[X - 1, Y - 1]; }
        }

        public Coords UpRight
        {
            get { return VariableProvider.CoordList[X + 1, Y - 1]; }
        }

        public Coords DownLeft
        {
            get { return VariableProvider.CoordList[X - 1, Y + 1]; }
        }

        public Coords DownRight
        {
            get { return VariableProvider.CoordList[X + 1, Y + 1]; }
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
            return VariableProvider.CoordList[coords.X / divisor, coords.Y / divisor];
        }

        public static Coords operator +(Coords leftCoords, Coords rightCoords)
        {
            return VariableProvider.CoordList[leftCoords.X + rightCoords.X, leftCoords.Y + rightCoords.Y];
        }

        public static Coords operator *(Coords coords, int multiplicator)
        {
            return VariableProvider.CoordList[coords.X*multiplicator, coords.Y*multiplicator];
        }

        public override string ToString()
        {
            return X + "," + Y;
        }
    }
}