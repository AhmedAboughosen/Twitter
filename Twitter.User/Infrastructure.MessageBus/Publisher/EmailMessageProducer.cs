using System.Text;
using System.Threading.Tasks;
using Core.Application.Contracts.Services;
using Core.Domain.Model.MessageBroker;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Infrastructure.MessageBus.Publisher
{
    public class EmailMessageProducer : IEmailMessagePublisher
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly string _queueName;

        public EmailMessageProducer(IConfiguration configuration, ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            _queueName = configuration["MessageBroker:EmailSenderQueueName"];
        }

        public Task SendMessageAsync<T>(MessageBody<T> message)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: _queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);


            var json = JsonConvert.SerializeObject(message);

            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish("", _queueName, null, body);

            return Task.CompletedTask;
        }
    }
}