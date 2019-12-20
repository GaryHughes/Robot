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
    }
}