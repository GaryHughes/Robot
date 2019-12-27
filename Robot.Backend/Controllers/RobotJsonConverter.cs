using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Robot;

namespace Robot.Backend
{
    public class RobotJsonConverter : JsonConverter<Robot>
    {
        public override Robot Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.Read();

            reader.Read();
            var width = reader.GetUInt32();
            reader.Read();
            reader.Read();
            var height = reader.GetUInt32();
            var robot = new Robot(new World(width, height));

            reader.Read();
            if (reader.TokenType != JsonTokenType.EndObject) {
                reader.Read();
                var x = reader.GetUInt32();
                reader.Read();
                reader.Read();
                var y = reader.GetUInt32();
                reader.Read();
                reader.Read();
                var direction = (Direction)Enum.Parse(typeof(Direction), reader.GetString());
                robot.Place(new Position(new Coordinate(x, y), direction));
            }

            reader.Read();

            return robot;
        }

        public override void Write(Utf8JsonWriter writer, Robot robot, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteNumber("Width", robot.World.Width);
            writer.WriteNumber("Height", robot.World.Height);

            if (robot.Position is Position position) {
                writer.WriteNumber("X", position.Coordinate.X);
                writer.WriteNumber("Y", position.Coordinate.Y);
                writer.WriteString("Direction", position.Direction.ToString());
            }

            writer.WriteEndObject();
        }
    }
}