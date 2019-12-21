using System;

namespace Robot
{
    public struct Coordinate
    {
        public Coordinate(uint x, uint y)
        {
            X = x;
            Y = y;
        }

        public uint X { get; }
        public uint Y { get; }

        public override string ToString() => $"X={X} Y={Y}";

        public static bool operator==(Coordinate left, Coordinate right)
        {
            return left.Equals(right);
        } 

        public static bool operator!=(Coordinate left, Coordinate right)
        {
            return !left.Equals(right);
        } 

        public override bool Equals(object obj)
        {
            if (obj is Coordinate coordinate) {
                return coordinate.X == X && coordinate.Y == Y;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}