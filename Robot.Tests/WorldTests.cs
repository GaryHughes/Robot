using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Robot.Tests
{
    [TestClass]
    public class WorldTests
    {
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void TestInvalidWidth()
        {
            new World(width:0, height:1);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void TestInvalidHeight()
        {
            new World(width:0, height:1);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void TestInvalidWidthAndHeight()
        {
            new World(width:0, height:0);
        }   

        [TestMethod]
        public void TestValidWidthAndHeight()
        {
            new World(width:1, height:1);
        }

        [TestMethod]
        public void TestSquareWorld()
        {
            new World(width:5, height:5);
        }

        [TestMethod]
        public void TestRectangularWorld()
        {
            new World(width:50, height:10);
        }

        [TestMethod]
        public void TestContains()
        {
            var world = new World(width:1, height:1);
            Assert.IsTrue(world.Contains(new Coordinate(x:0, y:0)));
            Assert.IsFalse(world.Contains(new Coordinate(x:1, y:0)));
            Assert.IsFalse(world.Contains(new Coordinate(x:0, y:1)));
            Assert.IsFalse(world.Contains(new Coordinate(x:1, y:1)));
        }

        [TestMethod]
        public void TestContainsWithLargerWorld()
        {
            var world = new World(width:100, height:100);
            Assert.IsTrue(world.Contains(new Coordinate(x:0, y:0)));
            Assert.IsTrue(world.Contains(new Coordinate(x:1, y:0)));
            Assert.IsTrue(world.Contains(new Coordinate(x:0, y:1)));
            Assert.IsTrue(world.Contains(new Coordinate(x:1, y:1)));
            Assert.IsTrue(world.Contains(new Coordinate(x:99, y:0)));
            Assert.IsTrue(world.Contains(new Coordinate(x:0, y:99)));
            Assert.IsFalse(world.Contains(new Coordinate(x:100, y:0)));
            Assert.IsFalse(world.Contains(new Coordinate(x:0, y:100)));
        }
    }
}
