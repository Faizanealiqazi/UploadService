using Microsoft.AspNetCore.Mvc;
using UploadService.Services;

namespace UploadService.Controllers;

public class RabbitMQController : ControllerBase
{
    private readonly RabbitMQService _rabbitMQService;

    public RabbitMQController(RabbitMQService rabbitMQService)
    {
        _rabbitMQService = rabbitMQService;
    }
}