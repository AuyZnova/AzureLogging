using System;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AzureLogging.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoggingController : ControllerBase
    {
        private readonly ILogger _logger;

        public LoggingController(ILogger<LoggingController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("Info")]
        public IActionResult Info()
        {
            _logger.LogInformation("This is info log.");

            return Ok();
        }

        [HttpGet]
        [Route("Error")]
        public IActionResult Error()
        {
            try
            {
                throw new Exception("This is an exception.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "This is error log.");
            }

            return Ok();
        }
    }
}
