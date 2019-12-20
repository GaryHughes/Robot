using System;

namespace Robot
{
    public class World
    {
        public World(uint width, uint height)
        {
            if (width == 0) {
                throw new ArgumentException($"{nameof(width)} cannot be zero", nameof(width));
            }

            if (height == 0) {
                throw new ArgumentException($"{nameof(height)} cannot be zero", nameof(height));
            }

            _width = width;
            _height = height;
        }

        public bool Contains(Coordinate coordinate)
        {
            return coordinate.X < _width && coordinate.Y < _height;
        }

        readonly uint _width;
        readonly uint _height;
    }
}