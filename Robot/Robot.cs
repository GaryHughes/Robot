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
        World _world;

    }
}
