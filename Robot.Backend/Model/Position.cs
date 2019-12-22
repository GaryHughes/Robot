using System;

namespace Robot.Backend.Model
{
    struct Position
    {
        public uint X { get; set; }
        public uint Y { get; set; }
        public Direction Direction { get; set; }
    }
}