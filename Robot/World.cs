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

            Width = width;
            Height = height;
        }

        public bool Contains(Coordinate coordinate)
        {
            return coordinate.X < Width && coordinate.Y < Height;
        }

        public uint Width { get; }
        public uint Height { get; }

        public override string ToString() => $"Width={Width} Height={Height}";

        public override bool Equals(object obj)
        {
            if (obj is World world) {
                return world.Width == Width && world.Height == Height;
            }

            return false;
        }

        public override int GetHashCode() => HashCode.Combine(Width, Height);

    }
}