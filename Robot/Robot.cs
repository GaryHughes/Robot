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
            return false;
        }

        public bool Right()
        {
            return false;
        }

        public Position? Report() => _position;

        Position? _position;
        readonly World _world;

    }
}
