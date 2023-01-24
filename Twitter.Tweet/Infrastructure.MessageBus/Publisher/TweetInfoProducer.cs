using System.Text;
using System.Threading.Tasks;
using Core.Application.Contracts.Services.Services.Publisher;
using Core.Domain.Events;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Infrastructure.MessageBus.Publisher
{
    public class TweetInfoProducer : ITweetInfoProducer
    {
        
        private readonly ConnectionFactory _connectionFactory;
        private readonly string _exchangeName;
        private readonly string _appId = "TweetInfo";

        public TweetInfoProducer(IConfiguration configuration, ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            _exchangeName = configuration["MessageBroker:ExchangeName"];
        }

        public Task SendMessageAsync<T>(MessageBody<T> message)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: _exchangeName, type: ExchangeType.Fanout);

            var json = JsonConvert.SerializeObject(message);

            var body = Encoding.UTF8.GetBytes(json);
            var properties = channel.CreateBasicProperties();

            properties.Type = message.Type;
            properties.AppId = _appId;

            channel.BasicPublish(exchange: _exchangeName, "", properties, body);

            return Task.CompletedTask;
        }
    }
}