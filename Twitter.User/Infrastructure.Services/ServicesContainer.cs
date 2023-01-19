using System.Reflection;
using Core.Application.Contracts.Services;
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
            // services.AddHttpContextAccessor();

            
            return services;
        }
    }
}