using System;

namespace Robot
{
    public class World
    {
        public World(uint width, uint height)
        {
            _width = width;
            _height = height;
        }

        public bool Contains(Position position)
        {
            return false;    
        }

        uint _width;
        uint _height;
    }
}