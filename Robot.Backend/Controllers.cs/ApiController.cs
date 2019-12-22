using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace Robot.Backend.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("[controller]/{version:apiVersion}/[action]")]
    public class ApiController : ControllerBase
    {
        readonly ILogger<ApiController> _logger;
        readonly IConfiguration _configuration;

        public ApiController(ILogger<ApiController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        Robot GetRobot()
        {
            var robot = HttpContext.Session.Get();
            if (robot is null) {
                uint width = Convert.ToUInt32(_configuration["Robot:World:Width"]);
                uint height = Convert.ToUInt32(_configuration["Robot:World:Height"]);
                robot = new Robot(new World(width, height));
            }
            return robot;
        }

        void PerformRobotAction(Action<Robot> action)
        {
            var robot = GetRobot();
            action(robot);
            HttpContext.Session.Set(robot);
        }

        [HttpGet]
        public IActionResult Report()
        {
            try {
                if (GetRobot().Report() is Position position) { 
                    return Accepted(new {
                        position.Coordinate.X,
                        position.Coordinate.Y,
                        Direction = position.Direction.ToString()
                    });
                }
            }    
            catch (Exception ex) {
                return BadRequest(ex);
            }

            return Accepted();
        }

        [HttpPut]
        public IActionResult Move()
        {
            try {
                PerformRobotAction(robot => robot.Move());
            }
            catch (Exception ex) {
                return BadRequest(ex);
            }

            return Accepted();    
        }

        [HttpPut]
        public IActionResult Left()
        {
            try {
                PerformRobotAction(robot => robot.Left());
            }
            catch (Exception ex) {
                return BadRequest(ex);
            }

            return Accepted();    
        }

        [HttpPut]
        public IActionResult Right()
        {
            try {
                PerformRobotAction(robot => robot.Right());
            }
            catch (Exception ex) {
                return BadRequest(ex);
            }

            return Accepted();    
        }

        [HttpPut("{x}/{y}/{direction}")]
        public IActionResult Place(uint x, uint y, Direction direction)
        {
            if (!ModelState.IsValid) {
                return BadRequest();
            }

            try {
                PerformRobotAction(robot => robot.Place(new Position(new Coordinate(x, y), direction)));
            }
            catch (Exception ex) {
                return BadRequest(ex);
            }

            return Accepted();    
        }

    }
}