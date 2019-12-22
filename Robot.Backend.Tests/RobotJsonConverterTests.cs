using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Robot;

namespace Robot.Backend.Tests
{
    [TestClass]
    public class RobotJsonConverterTests
    {
        readonly JsonSerializerOptions _options = new JsonSerializerOptions
        { 
            Converters = { new RobotJsonConverter() },
            WriteIndented = false
        };

        [TestMethod]
        public void TestSerialiseWithNoPosition()
        {
            var robot = new Robot(new World(5, 5));
            var json = JsonSerializer.Serialize(robot, _options); 
            Assert.AreEqual(@"{""Width"":5,""Height"":5}", json);
        }

        [TestMethod]
        public void TestSerialiseWithPosition()
        {
            var robot = new Robot(new World(5, 5));
            robot.Place(new Position(new Coordinate(1, 1), Direction.North));
            var json = JsonSerializer.Serialize(robot, _options); 
            Assert.AreEqual(@"{""Width"":5,""Height"":5,""X"":1,""Y"":1,""Direction"":""North""}", json);
        }

        [TestMethod]
        public void TestDeserialiseWithNoPosition()
        {
            var json = @"{""Width"":5,""Height"":5}";
            var actual = JsonSerializer.Deserialize<Robot>(json, _options);
            var expected = new Robot(new World(5, 5));
            Assert.AreEqual(expected.World, actual.World);
            Assert.AreEqual(expected.Position, actual.Position);
        }

        [TestMethod]
        public void TestDeserialiseWithPosition()
        {
            var json = @"{""Width"":5,""Height"":5,""X"":1,""Y"":1,""Direction"":""North""}";
            var actual = JsonSerializer.Deserialize<Robot>(json, _options);
            var expected = new Robot(new World(5, 5));
            expected.Place(new Position(new Coordinate(1, 1), Direction.North));
            Assert.AreEqual(expected.World, actual.World);
            Assert.AreEqual(expected.Position, actual.Position);
        }
    }
}
