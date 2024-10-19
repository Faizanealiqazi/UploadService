using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using UploadService.Models;

namespace UploadService.Services
{
    public class RabbitMQService
    {
        private readonly RabbitMQConfig _config;
        private readonly IConnection _connection;
        private readonly AccountService _accountService;
        private readonly ILogger<RabbitMQService> _logger;

        public RabbitMQService(IConfiguration configuration, AccountService accountService, ILogger<RabbitMQService> logger)
        {
            _config = new RabbitMQConfig
            {
                HostName = configuration["RabbitMQ:Hostname"],
                QueueName = configuration["RabbitMQ:QueueName"],
                UserName = configuration["RabbitMQ:UserName"],
                Password = configuration["RabbitMQ:Password"],
                Port = int.Parse(configuration["RabbitMQ:Port"])
            };

            var factory = new ConnectionFactory
            {
                HostName = _config.HostName,
                UserName = _config.UserName,
                Password = _config.Password,
                Port = _config.Port
            };
            _connection = factory.CreateConnection();
            _accountService = accountService;
            _logger = logger;

            InitializeRabbitMQListener();
        }

        private void InitializeRabbitMQListener()
        {
            _logger.LogInformation("InitializeRabbitMQListener() :: entered");

            using var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: _config.QueueName, durable: true, exclusive: false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var account = JsonSerializer.Deserialize<Account>(message);

                if (account != null)
                {
                    _logger.LogInformation("InitializeRabbitMQListener() :: Received account message from RabbitMQ: {@Account}", account);
                    await _accountService.UploadAccountFromApiDataAsync(account);
                    _logger.LogInformation("InitializeRabbitMQListener() :: Successfully uploaded account from RabbitMQ: {@Account}", account);
                }
                else
                {
                    _logger.LogWarning("InitializeRabbitMQListener() :: Received an empty or invalid account message from RabbitMQ");
                }

                channel.BasicAck(ea.DeliveryTag, false);
            };

            channel.BasicConsume(_config.QueueName, false, consumer);
            _logger.LogInformation("InitializeRabbitMQListener() :: Listening for messages on RabbitMQ queue: {QueueName}", _config.QueueName);

            _logger.LogInformation("InitializeRabbitMQListener() :: exited");
        }
    }
}
