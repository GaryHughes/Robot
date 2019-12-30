using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Robot.Backend.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("[controller]/{version:apiVersion}/[action]")]
    public class ApiController : ControllerBase
    {
        readonly ILogger<ApiController> _logger;
        readonly IConfiguration _configuration;
        readonly IServiceScopeFactory _serviceScopeFactory;

        public ApiController(ILogger<ApiController> logger, 
                             IConfiguration configuration,
                             IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _configuration = configuration;
            _serviceScopeFactory = serviceScopeFactory;
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

        [HttpGet("/")]
        public IActionResult Index()
        {
            // This endpoint is used by GCP health checks.
            return Ok();
        }

        [HttpGet]
        public IActionResult Usage()
        {
            // If the ApiUsageFilter was generalised this method would move to it's own controller.
            try {
                using var scope = _serviceScopeFactory.CreateScope();
                var usage = scope.ServiceProvider.GetService<ApiUsageFilter>();
                return new JsonResult(new {
                    Counts = (from count in usage.ActionCounts 
                              select new {
                                  Action = count.Key,
                                  count.Value.Count
                              }).ToArray()             
                });
            }
            catch (Exception ex) {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult World()
        {
            try {
                var robot = GetRobot();
                return new JsonResult(new {
                    robot.World.Width,
                    robot.World.Height
                });
            }    
            catch (Exception ex) {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Report()
        {
            try {
                if (GetRobot().Report() is Position position) { 
                    return new JsonResult(new {
                        position.Coordinate.X,
                        position.Coordinate.Y,
                        Direction = position.Direction.ToString()
                    }) {
                        StatusCode = (int)HttpStatusCode.OK
                    };
                }
            }    
            catch (Exception ex) {
                return Problem(ex.Message);
            }

            return Ok();
        }

        [HttpPut]
        public IActionResult Move()
        {
            try {
                PerformRobotAction(robot => robot.Move());
            }
            catch (Exception ex) {
                return Problem(ex.Message);
            }

            return Report();    
        }

        [HttpPut]
        public IActionResult Left()
        {
            try {
                PerformRobotAction(robot => robot.Left());
            }
            catch (Exception ex) {
                return Problem(ex.Message);
            }

            return Report();    
        }

        [HttpPut]
        public IActionResult Right()
        {
            try {
                PerformRobotAction(robot => robot.Right());
            }
            catch (Exception ex) {
                return Problem(ex.Message);
            }

            return Report();    
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
                return Problem(ex.Message);
            }

            return Report();    
        }

    }
}