using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Contracts.Services.BaseService;
using Core.Domain.Model.MessageBroker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infrastructure.MessageBus.Listener
{
    public class EmailSenderListener : IHostedService
    {
        private readonly IServiceProvider _provider;
        private readonly ILogger<EmailSenderListener> _logger;
        private readonly ConnectionFactory _connectionFactory;
        private IModel _channel;
        private readonly string queueName;

        public EmailSenderListener(
            IServiceProvider provider,
            ILogger<EmailSenderListener> logger,
            ConnectionFactory connectionFactory,
            IConfiguration configuration)
        {
            _provider = provider;
            _logger = logger;
            _connectionFactory = connectionFactory;
             queueName = configuration["MessageBroker:QueueName"];
        }

        private async void Processor_ProcessMessageAsync(object obj, BasicDeliverEventArgs arg)
        {
            Task<bool> isHandledTask = HandelSubject(arg.BasicProperties.Type, arg);


            var isHandled = await isHandledTask;

            if (isHandled)
            {
                _channel.BasicAck(deliveryTag: arg.DeliveryTag, multiple: false);
            }


            //
            // var body = arg.Body.ToArray();
            // var message = Encoding.UTF8.GetString(body);
            // Console.WriteLine($"Recieved new message: {message}");
        }


        private Task<bool> HandelSubject(object subject, BasicDeliverEventArgs message)
        {
            return HandleAsync<EmailMessageModel>(message);
        }

        private async Task<bool> HandleAsync<T>(BasicDeliverEventArgs message)
        {
            _logger.LogInformation("Event handling started.");

            var bodyBytes = message.Body.ToArray();

            var json = Encoding.UTF8.GetString(bodyBytes);

            var body = JsonConvert.DeserializeObject<MessageBody<T>>(json);

            using var scope = _provider.CreateScope();

            var handler = scope.ServiceProvider.GetRequiredService<IMessageHandler>();

            return await handler.HandleAsync(body);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            using var connection = _connectionFactory.CreateConnection();

            using var channel = connection.CreateModel();

            _channel = channel;

            _channel.QueueDeclare(
                queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += Processor_ProcessMessageAsync;

            _channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);

            Console.WriteLine("Consuming");

            Console.ReadKey();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _channel.Close();
            // _processor.CloseAsync(cancellationToken);
            // _deadLetterProcessor.CloseAsync(cancellationToken);

            return Task.CompletedTask;
        }
    }
}