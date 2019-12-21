using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Robot.Tests
{
    [TestClass]
    public class RobotTests
    {
        [TestMethod]
        public void TestPlacementAtInvalidXCoordinateIsIgnored()
        {
            var world = new World(width:5, height:5);
            var robot = new Robot(world);
            Assert.IsFalse(robot.Place(new Position(new Coordinate(5, 0), Direction.North)));
            Assert.IsNull(robot.Report());
        }

        [TestMethod]
        public void TestPlacementAtInvalidYCoordinateIsIgnored()
        {
            var world = new World(width:5, height:5);
            var robot = new Robot(world);
            Assert.IsFalse(robot.Place(new Position(new Coordinate(0, 5), Direction.North)));
            Assert.IsNull(robot.Report());
        }

        [TestMethod]
        public void TestPlacementAtInvalidXAndYCoordinateIsIgnored()
        {
            var world = new World(width:5, height:5);
            var robot = new Robot(world);
            Assert.IsFalse(robot.Place(new Position(new Coordinate(5, 5), Direction.North)));
            Assert.IsNull(robot.Report());
        }

        [TestMethod]
        public void TestPlacementAtValidCoordinateIsExecuted()
        {
            var world = new World(width:5, height:5);
            var robot = new Robot(world);
            Assert.IsTrue(robot.Place(new Position(new Coordinate(1, 2), Direction.North)));
            var position = robot.Report();
            Assert.IsNotNull(position);
            Assert.AreEqual(new Position(new Coordinate(1, 2), Direction.North), position);
        }

        [TestMethod]
        public void TestSuccessiveValidPlacements()
        {
            var world = new World(width:5, height:5);
            var robot = new Robot(world);
            Assert.IsTrue(robot.Place(new Position(new Coordinate(1, 2), Direction.North)));
            var position = robot.Report();
            Assert.IsNotNull(position);
            Assert.AreEqual(new Position(new Coordinate(1, 2), Direction.North), position);
            Assert.IsTrue(robot.Place(new Position(new Coordinate(3, 4), Direction.North)));
            position = robot.Report();
            Assert.IsNotNull(position);
            Assert.AreEqual(new Position(new Coordinate(3, 4), Direction.North), position);
        }

        [TestMethod]
        public void TestInvalidPlacementAfterValidPlacementIsIgnored()
        {
            var world = new World(width:5, height:5);
            var robot = new Robot(world);
            Assert.IsTrue(robot.Place(new Position(new Coordinate(1, 2), Direction.North)));
            var position = robot.Report();
            Assert.IsNotNull(position);
            Assert.AreEqual(new Position(new Coordinate(1, 2), Direction.North), position);
            Assert.IsFalse(robot.Place(new Position(new Coordinate(10, 4), Direction.North)));
            position = robot.Report();
            Assert.IsNotNull(position);
            Assert.AreEqual(new Position(new Coordinate(1, 2), Direction.North), position);
        }

        [TestMethod]
        public void TestLeftIsIgnoredBeforeValidPlacement()
        {
            var world = new World(width:5, height:5);
            var robot = new Robot(world);
            Assert.IsNull(robot.Report());
            Assert.IsFalse(robot.Left());
            Assert.IsNull(robot.Report());
        }

        [TestMethod]
        public void TestRightIsIgnoredBeforeValidPlacement()
        {
            var world = new World(width:5, height:5);
            var robot = new Robot(world);
            Assert.IsNull(robot.Report());
            Assert.IsFalse(robot.Right());
            Assert.IsNull(robot.Report());
        }

        [TestMethod]
        public void TestLeftBeforeValidPlacement()
        {
            var world = new World(width:5, height:5);
            var robot = new Robot(world);
            Assert.IsFalse(robot.Left());
        }

        [TestMethod]
        public void TestRightBeforeValidPlacement()
        {
            var world = new World(width:5, height:5);
            var robot = new Robot(world);
            Assert.IsFalse(robot.Right());
        }

        [TestMethod]
        public void TestLeftAfterInvalidPlacement()
        {
            var world = new World(width:5, height:5);
            var robot = new Robot(world);
            Assert.IsFalse(robot.Place(new Position(new Coordinate(10, 10), Direction.North)));
            Assert.IsFalse(robot.Left());
        }

        [TestMethod]
        public void TestRightAfterInvalidPlacement()
        {
            var world = new World(width:5, height:5);
            var robot = new Robot(world);
            Assert.IsFalse(robot.Place(new Position(new Coordinate(10, 10), Direction.North)));
            Assert.IsFalse(robot.Right());
        }

        [TestMethod]
        public void TestLeftAfterValidPlacement()
        {
            var world = new World(width:5, height:5);
            var robot = new Robot(world);
            Assert.IsTrue(robot.Place(new Position(new Coordinate(1, 2), Direction.North)));
            Assert.IsTrue(robot.Left());
        }

        [TestMethod]
        public void TestRightAfterValidPlacement()
        {
            var world = new World(width:5, height:5);
            var robot = new Robot(world);
            Assert.IsTrue(robot.Place(new Position(new Coordinate(1, 2), Direction.North)));
            Assert.IsTrue(robot.Right());
        }

        [TestMethod]
        public void TestFullTurnLeftAfterValidPlacement()
        {
            var world = new World(width:5, height:5);
            var robot = new Robot(world);
            Assert.IsTrue(robot.Place(new Position(new Coordinate(1, 2), Direction.North)));
            Assert.IsTrue(robot.Left());
            Assert.AreEqual(Direction.West, robot.Report()?.Direction);
            Assert.IsTrue(robot.Left());
            Assert.AreEqual(Direction.South, robot.Report()?.Direction);
            Assert.IsTrue(robot.Left());
            Assert.AreEqual(Direction.East, robot.Report()?.Direction);
            Assert.IsTrue(robot.Left());
            Assert.AreEqual(Direction.North, robot.Report()?.Direction);
        }

        [TestMethod]
        public void TestFullTurnRightAfterValidPlacement()
        {
            var world = new World(width:5, height:5);
            var robot = new Robot(world);
            Assert.IsTrue(robot.Place(new Position(new Coordinate(1, 2), Direction.North)));
            Assert.IsTrue(robot.Right());
            Assert.AreEqual(Direction.East, robot.Report()?.Direction);
            Assert.IsTrue(robot.Right());
            Assert.AreEqual(Direction.South, robot.Report()?.Direction);
            Assert.IsTrue(robot.Right());
            Assert.AreEqual(Direction.West, robot.Report()?.Direction);
            Assert.IsTrue(robot.Right());
            Assert.AreEqual(Direction.North, robot.Report()?.Direction);
        }

        [TestMethod]
        public void TestInvalidMoveNorth()
        {
            var world = new World(width:5, height:5);
            var robot = new Robot(world);
            Assert.IsTrue(robot.Place(new Position(new Coordinate(0, 4), Direction.North)));
            Assert.IsFalse(robot.Move());
            Assert.AreEqual(new Position(new Coordinate(0, 4), Direction.North), robot.Report());
        } 

        [TestMethod]
        public void TestInvalidMoveEast()
        {
            var world = new World(width:5, height:5);
            var robot = new Robot(world);
            Assert.IsTrue(robot.Place(new Position(new Coordinate(4, 0), Direction.East)));
            Assert.IsFalse(robot.Move());
            Assert.AreEqual(new Position(new Coordinate(4, 0), Direction.East), robot.Report());
        }

        [TestMethod]
        public void TestInvalidMoveSouth()
        {
            var world = new World(width:5, height:5);
            var robot = new Robot(world);
            Assert.IsTrue(robot.Place(new Position(new Coordinate(0, 0), Direction.South)));
            Assert.IsFalse(robot.Move());
            Assert.AreEqual(new Position(new Coordinate(0, 0), Direction.South), robot.Report());
        }

        [TestMethod]
        public void TestInvalidMoveWest()
        {
            var world = new World(width:5, height:5);
            var robot = new Robot(world);
            Assert.IsTrue(robot.Place(new Position(new Coordinate(0, 0), Direction.West)));
            Assert.IsFalse(robot.Move());
            Assert.AreEqual(new Position(new Coordinate(0, 0), Direction.West), robot.Report());
        }

        [TestMethod]
        public void TestValidMoveNorth()
        {
            var world = new World(width:5, height:5);
            var robot = new Robot(world);
            Assert.IsTrue(robot.Place(new Position(new Coordinate(0, 3), Direction.North)));
            Assert.IsTrue(robot.Move());
            Assert.AreEqual(new Position(new Coordinate(0, 4), Direction.North), robot.Report());
        } 

        [TestMethod]
        public void TestValidMoveEast()
        {
            var world = new World(width:5, height:5);
            var robot = new Robot(world);
            Assert.IsTrue(robot.Place(new Position(new Coordinate(3, 0), Direction.East)));
            Assert.IsTrue(robot.Move());
            Assert.AreEqual(new Position(new Coordinate(4, 0), Direction.East), robot.Report());
        }

        [TestMethod]
        public void TestValidMoveSouth()
        {
            var world = new World(width:5, height:5);
            var robot = new Robot(world);
            Assert.IsTrue(robot.Place(new Position(new Coordinate(0, 1), Direction.South)));
            Assert.IsTrue(robot.Move());
            Assert.AreEqual(new Position(new Coordinate(0, 0), Direction.South), robot.Report());
        }

        [TestMethod]
        public void TestValidMoveWest()
        {
            var world = new World(width:5, height:5);
            var robot = new Robot(world);
            Assert.IsTrue(robot.Place(new Position(new Coordinate(1, 0), Direction.West)));
            Assert.IsTrue(robot.Move());
            Assert.AreEqual(new Position(new Coordinate(0, 0), Direction.West), robot.Report());
        }
    }
}
