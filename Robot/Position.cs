using System;

namespace Robot
{
    public struct Position
    {
        public Position(uint x, uint y, Direction direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }

        public uint X { get; }
        public uint Y { get; }
        public Direction Direction { get; } 
    }
}