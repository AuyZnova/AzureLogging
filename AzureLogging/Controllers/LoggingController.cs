using System;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

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
            _logger.LogInformation("ILogger: This is an info log.");
            Log.Information("Serilog: This is an info log.");

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
                _logger.LogError(e, "ILogger: This is an error log.");
                Log.Error(e, "Serilog: This is an error log.");
            }

            return Ok();
        }
    }
}
