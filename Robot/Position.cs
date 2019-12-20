using System;

namespace Robot
{
    public struct Position
    {
        public Position(Coordinate coordinate, Direction direction)
        {
            Coordinate = coordinate;
            Direction = direction;
        }

        public Coordinate Coordinate { get; }
        public Direction Direction { get; } 
    }
}