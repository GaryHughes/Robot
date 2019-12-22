using System;

namespace Robot
{
    public class Robot
    {
        public Robot(World world)
        {
            World = world;
        }

        public bool Place(Position position)
        {
            if (World.Contains(position.Coordinate)) {
                Position = position;
                return true;
            }
            
            return false;
        }

        public bool Move()
        {
            if (Position is Position position) {
                
                var (deltaX, deltaY) = position switch {
                    var (_, _, direction) when direction == Direction.North => (0, 1),
                    var (_, _, direction) when direction == Direction.East => (1, 0),
                    var (_, y, direction) when direction == Direction.South && y > 0 => (0, -1),
                    var (x, _, direction) when direction == Direction.West && x > 0 => (-1, 0),
                    var (_, _, _) => (0, 0)
                };

                if (deltaX == 0 && deltaY == 0) {
                    return false;
                }

                int newX = ((int)position.Coordinate.X) + deltaX;
                int newY = ((int)position.Coordinate.Y) + deltaY;

                var newCoordinate = new Coordinate((uint)newX, (uint)newY);

                return Place(new Position(newCoordinate, position.Direction));
            }    

            return false;
        }

        public bool Left()
        {
            if (Position is Position position) {
                var newDirection = position.Direction switch {
                    Direction.North => Direction.West,
                    Direction.East => Direction.North,
                    Direction.South => Direction.East,
                    Direction.West => Direction.South,
                    _ => throw new Exception($"Invalid value for direction {position.Direction}")
                };
                Position = new Position(position.Coordinate, newDirection);
                return true;
            }

            return false;
        }

        public bool Right()
        {
            if (Position is Position position) {
                var newDirection = position.Direction switch {
                    Direction.North => Direction.East,
                    Direction.East => Direction.South,
                    Direction.South => Direction.West,
                    Direction.West => Direction.North,
                    _ => throw new Exception($"Invalid value for direction {position.Direction}")
                };
                Position = new Position(position.Coordinate, newDirection);
                return true;
            }

            return false;
        }

        public Position? Report() => Position;


        public World World { get; }

        public Position? Position { get; private set; }

    }
}
