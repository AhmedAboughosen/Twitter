using System;
using Core.Application.Contracts.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Configurations;
using Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence
{
    public static class PersistenceContainer
    {
        public static IServiceCollection AddPersistenceRegistration(this IServiceCollection services,
            IConfiguration configuration)
        {

            services.AddIdentity<User, IdentityRole>(opt =>
                {
                    opt.Password.RequiredLength = 7;
                    opt.Password.RequireDigit = false;
                    opt.Password.RequireUppercase = false;
                    opt.User.RequireUniqueEmail = true;
                    opt.SignIn.RequireConfirmedEmail = true;
                    opt.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";
                })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders()
                .AddTokenProvider<EmailConfirmationTokenProvider<User>>("emailconfirmation");
            
            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromHours(2));
            services.Configure<EmailConfirmationTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromDays(3));

            
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Default"), opt =>
                    opt.EnableRetryOnFailure(5,
                        TimeSpan.FromSeconds(30),
                        null)));
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}