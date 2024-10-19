using Microsoft.AspNetCore.Mvc;
using UploadService.Services;

namespace UploadService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RabbitMQController : ControllerBase
    {
        private readonly RabbitMQService _rabbitMQService;
        private readonly ILogger<RabbitMQController> _logger;

        public RabbitMQController(RabbitMQService rabbitMQService, ILogger<RabbitMQController> logger)
        {
            _rabbitMQService = rabbitMQService;
            _logger = logger;
        }

        [HttpGet("start")]
        public IActionResult StartRabbitMQ()
        {
            _logger.LogInformation("StartRabbitMQ() :: entered");
            _logger.LogInformation("StartRabbitMQ() :: exited");
            return Ok("RabbitMQ Service Started");
        }
    }
}