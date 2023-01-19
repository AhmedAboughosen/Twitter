using Core.Application.Contracts.Services;
using Infrastructure.MessageBus.Publisher;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Infrastructure.MessageBus
{
    public static class MessageBusContainer
    {
        public static IServiceCollection AddMessageBusRegistration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IEmailMessagePublisher, EmailMessageProducer>();
            services.AddScoped<IUserInfoPublisher, UserInfoProducer>();
            services.AddSingleton(s => new ConnectionFactory() {HostName = configuration["MessageBroker:HostName"]});

            return services;
        }
    }
}