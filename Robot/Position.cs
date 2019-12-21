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

        public void Deconstruct(out uint x, out uint y, out Direction direction)  
        {  
            (x, y, direction) = (Coordinate.X, Coordinate.Y, Direction);  
        }  

        public override string ToString() => $"{Coordinate} {Direction}";

        public override bool Equals(object obj)
        {
            if (obj is Position position) {
                return position.Coordinate == Coordinate && position.Direction == Direction;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Coordinate, Direction);
        }
    }
}