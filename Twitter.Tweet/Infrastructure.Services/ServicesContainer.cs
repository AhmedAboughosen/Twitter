using System.Reflection;
using Core.Application.Contracts.Services;
using Core.Application.Contracts.Services.Services.BaseService;
using Core.Application.Contracts.Services.Services.Publisher;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services
{
    public static class ServicesContainer
    {
        public static IServiceCollection AddServicesRegistration(this IServiceCollection services,
            IConfiguration configuration)
        {
          
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient<IMessageHandler, BaseService.MessageHandler>();

            return services;
        }
    }
}