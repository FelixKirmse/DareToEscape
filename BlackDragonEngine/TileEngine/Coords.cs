﻿using System;
using BlackDragonEngine.Providers;
using Microsoft.Xna.Framework;

namespace BlackDragonEngine.TileEngine
{
    [Serializable]
    public sealed class Coords
    {
        public readonly int X;
        public readonly int Y;

        public Coords()
        {
        }

        public Coords(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Coords Left => VariableProvider.CoordList[X - 1, Y];

        public Coords Right => VariableProvider.CoordList[X + 1, Y];

        public Coords Up => VariableProvider.CoordList[X, Y - 1];

        public Coords Down => VariableProvider.CoordList[X, Y + 1];

        public Coords UpLeft => VariableProvider.CoordList[X - 1, Y - 1];

        public Coords UpRight => VariableProvider.CoordList[X + 1, Y - 1];

        public Coords DownLeft => VariableProvider.CoordList[X - 1, Y + 1];

        public Coords DownRight => VariableProvider.CoordList[X + 1, Y + 1];

        public override int GetHashCode()
        {
            return X ^ Y;
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
            return VariableProvider.CoordList[coords.X * multiplicator, coords.Y * multiplicator];
        }

        public static implicit operator Vector2(Coords c)
        {
            return new Vector2(c.X, c.Y);
        }

        public static implicit operator Coords(Vector2 v)
        {
            return VariableProvider.CoordList[(int) v.X, (int) v.Y];
        }

        public override string ToString()
        {
            return X + "," + Y;
        }
    }
}