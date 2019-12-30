using System.IO;
using System.Net;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Robot.Backend.Controllers;

namespace Robot.Backend.Tests
{
    [TestClass]
    public class ApiControllerTests
    {
        ApiController Controller;

        [TestInitialize]
        public void TestInitialize()
        {
            var loggerFactory = new LoggerFactory();
            var logger = new Logger<ApiController>(loggerFactory);
            
            var stream = new MemoryStream(Encoding.ASCII.GetBytes(
                @"{
                    ""Robot"": {
                        ""World"": {
                            ""Width"": 5,
                            ""Height"": 5
                        }
                    }
                }"
            ));
            
            var configuration = new ConfigurationBuilder()
                .AddJsonStream(stream)
                .Build();
            
            Controller = new ApiController(logger, configuration, null);

            Controller.ControllerContext.HttpContext = new TestHttpContext {
                Session = new TestSession()
            };
        }

        [TestMethod]
        public void TestPlacementAtInvalidXCoordinateIsIgnored()
        {
            var result = Controller.Place(5, 0, Direction.North) as OkResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void TestPlacementAtInvalidYCoordinateIsIgnored()
        {
            var result = Controller.Place(0, 5, Direction.North) as OkResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void TestPlacementAtInvalidXAndYCoordinateIsIgnored()
        {
            var result = Controller.Place(5, 5, Direction.North) as OkResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void TestPlacementAtValidCoordinateIsExecuted()
        {
            var result = Controller.Place(1, 2, Direction.North) as JsonResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            object expected = new { X = 1u, Y = 2u, Direction = Direction.North.ToString() };
            object actual = result.Value;
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }

        [TestMethod]
        public void TestSuccessiveValidPlacements()
        {
            var result = Controller.Place(1, 2, Direction.North) as JsonResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            object expected = new { X = 1u, Y = 2u, Direction = Direction.North.ToString() };
            object actual = result.Value;
            Assert.AreEqual(expected.ToString(), actual.ToString());
            result = Controller.Place(3, 4, Direction.North) as JsonResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            expected = new { X = 3u, Y = 4u, Direction = Direction.North.ToString() };
            actual = result.Value;
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }

        [TestMethod]
        public void TestInvalidPlacementAfterValidPlacementIsIgnored()
        {
            var result = Controller.Place(1, 2, Direction.North) as JsonResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            object expected = new { X = 1u, Y = 2u, Direction = Direction.North.ToString() };
            object actual = result.Value;
            Assert.AreEqual(expected.ToString(), actual.ToString());
            result = Controller.Place(10, 4, Direction.North) as JsonResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            expected = new { X = 1u, Y = 2u, Direction = Direction.North.ToString() };
            actual = result.Value;
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }

        [TestMethod]
        public void TestLeftIsIgnoredBeforeValidPlacement()
        {
            var result = Controller.Left() as OkResult;
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void TestRightIsIgnoredBeforeValidPlacement()
        {
            var result = Controller.Right() as OkResult;
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void TestLeftAfterInvalidPlacement()
        {
            var result = Controller.Place(10, 10, Direction.North) as OkResult;
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            result = Controller.Left() as OkResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public void TestRightAfterInvalidPlacement()
        {
           var result = Controller.Place(10, 10, Direction.North) as OkResult;
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            result = Controller.Right() as OkResult;
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void TestLeftAfterValidPlacement()
        {
            var result = Controller.Place(1, 2, Direction.North) as JsonResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            object expected = new { X = 1u, Y = 2u, Direction = Direction.North.ToString() };
            object actual = result.Value;
            Assert.AreEqual(expected.ToString(), actual.ToString());
            result = Controller.Left() as JsonResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            expected = new { X = 1u, Y = 2u, Direction = Direction.West.ToString() };
            actual = result.Value;
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }

        [TestMethod]
        public void TestRightAfterValidPlacement()
        {
            var result = Controller.Place(1, 2, Direction.North) as JsonResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            object expected = new { X = 1u, Y = 2u, Direction = Direction.North.ToString() };
            object actual = result.Value;
            Assert.AreEqual(expected.ToString(), actual.ToString());
            result = Controller.Right() as JsonResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            expected = new { X = 1u, Y = 2u, Direction = Direction.East.ToString() };
            actual = result.Value;
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }

        [TestMethod]
        public void TestInvalidMoveNorth()
        {
            var result = Controller.Place(0, 4, Direction.North) as JsonResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            object expected = new { X = 0u, Y = 4u, Direction = Direction.North.ToString() };
            object actual = result.Value;
            Assert.AreEqual(expected.ToString(), actual.ToString());
            result = Controller.Move() as JsonResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            expected = new { X = 0u, Y = 4u, Direction = Direction.North.ToString() };
            actual = result.Value;
            Assert.AreEqual(expected.ToString(), actual.ToString());
        } 

        [TestMethod]
        public void TestInvalidMoveEast()
        {
            var result = Controller.Place(4, 0, Direction.East) as JsonResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            object expected = new { X = 4u, Y = 0u, Direction = Direction.East.ToString() };
            object actual = result.Value;
            Assert.AreEqual(expected.ToString(), actual.ToString());
            result = Controller.Move() as JsonResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            expected = new { X = 4u, Y = 0u, Direction = Direction.East.ToString() };
            actual = result.Value;
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }

        [TestMethod]
        public void TestInvalidMoveSouth()
        {
            var result = Controller.Place(0, 0, Direction.South) as JsonResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            object expected = new { X = 0u, Y = 0u, Direction = Direction.South.ToString() };
            object actual = result.Value;
            Assert.AreEqual(expected.ToString(), actual.ToString());
            result = Controller.Move() as JsonResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            expected = new { X = 0u, Y = 0u, Direction = Direction.South.ToString() };
            actual = result.Value;
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }

        [TestMethod]
        public void TestInvalidMoveWest()
        {
            var result = Controller.Place(0, 0, Direction.West) as JsonResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            object expected = new { X = 0u, Y = 0u, Direction = Direction.West.ToString() };
            object actual = result.Value;
            Assert.AreEqual(expected.ToString(), actual.ToString());
            result = Controller.Move() as JsonResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            expected = new { X = 0u, Y = 0u, Direction = Direction.West.ToString() };
            actual = result.Value;
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }

        [TestMethod]
        public void TestValidMoveNorth()
        {
            var result = Controller.Place(0, 3, Direction.North) as JsonResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            object expected = new { X = 0u, Y = 3u, Direction = Direction.North.ToString() };
            object actual = result.Value;
            Assert.AreEqual(expected.ToString(), actual.ToString());
            result = Controller.Move() as JsonResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            expected = new { X = 0u, Y = 4u, Direction = Direction.North.ToString() };
            actual = result.Value;
            Assert.AreEqual(expected.ToString(), actual.ToString());
        } 

        [TestMethod]
        public void TestValidMoveEast()
        {
            var result = Controller.Place(3, 0, Direction.East) as JsonResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            object expected = new { X = 3u, Y = 0u, Direction = Direction.East.ToString() };
            object actual = result.Value;
            Assert.AreEqual(expected.ToString(), actual.ToString());
            result = Controller.Move() as JsonResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            expected = new { X = 4u, Y = 0u, Direction = Direction.East.ToString() };
            actual = result.Value;
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }

        [TestMethod]
        public void TestValidMoveSouth()
        {
            var result = Controller.Place(0, 1, Direction.South) as JsonResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            object expected = new { X = 0u, Y = 1u, Direction = Direction.South.ToString() };
            object actual = result.Value;
            Assert.AreEqual(expected.ToString(), actual.ToString());
            result = Controller.Move() as JsonResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            expected = new { X = 0u, Y = 0u, Direction = Direction.South.ToString() };
            actual = result.Value;
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }

        [TestMethod]
        public void TestValidMoveWest()
        {
            var result = Controller.Place(1, 0, Direction.West) as JsonResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            object expected = new { X = 1u, Y = 0u, Direction = Direction.West.ToString() };
            object actual = result.Value;
            Assert.AreEqual(expected.ToString(), actual.ToString());
            result = Controller.Move() as JsonResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            expected = new { X = 0u, Y = 0u, Direction = Direction.West.ToString() };
            actual = result.Value;
            Assert.AreEqual(expected.ToString(), actual.ToString());
        } 

        /*
        // TODO: This requires a lot of extra plumbing for the DI container.
        [TestMethod]
        public void TestUsage()
        {
            Controller.Place(1, 0, Direction.West);
            Controller.Move();
            var result = Controller.Usage() as JsonResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            var expected = new { Counts = new [] { 
                new { Action = "Place", Count = 1},
                new { Action = "Move", Count = 1} } 
            };
            var actual = result.Value;
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }
        */ 
    }
}