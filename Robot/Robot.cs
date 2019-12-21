using System;

namespace Robot
{
    public class Robot
    {
        public Robot(World world)
        {
            _world = world;
        }

        public bool Place(Position position)
        {
            if (_world.Contains(position.Coordinate)) {
                _position = position;
                return true;
            }
            
            return false;
        }

        public bool Move()
        {
            return false;
        }

        public bool Left()
        {
            if (_position is Position position) {
                var newDirection = position.Direction switch {
                    Direction.North => Direction.West,
                    Direction.East => Direction.North,
                    Direction.South => Direction.East,
                    Direction.West => Direction.South,
                    _ => throw new Exception($"Invalid value for direction {position.Direction}")
                };
                _position = new Position(position.Coordinate, newDirection);
                return true;
            }

            return false;
        }

        public bool Right()
        {
            if (_position is Position position) {
                var newDirection = position.Direction switch {
                    Direction.North => Direction.East,
                    Direction.East => Direction.South,
                    Direction.South => Direction.West,
                    Direction.West => Direction.North,
                    _ => throw new Exception($"Invalid value for direction {position.Direction}")
                };
                _position = new Position(position.Coordinate, newDirection);
                return true;
            }

            return false;
        }

        public Position? Report() => _position;

        Position? _position;
        readonly World _world;

    }
}
