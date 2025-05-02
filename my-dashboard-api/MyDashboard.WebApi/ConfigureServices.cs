using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MyDashboard.Service.Interface;
using MyDashboard.Service;
using MyDashboard.Model;
using System.Text;
using MyDashboard.Model.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using MyDashboard.Repository.Interface;
using MyDashboard.Repository;

namespace MyDashboard.WebApi
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddDbContextServices(this IServiceCollection services, MyDashboardSettings appSettings)
        {
            services.AddDbContext<MyDashboardlDbContext>(options =>
            {
                options.UseSqlServer(appSettings.ConnectionStrings.MyDashboardDb)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
            });
            return services;
        }

        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<ILoggerRepository, LoggerRepository>();
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<IAuthRepositoy, AuthRepositoy>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }

        public static void RegisterAppLogger(this IServiceCollection serviceCollections)
        {
            serviceCollections
            .AddLogging(option =>
            {
                option.ClearProviders();
                option.AddProvider(new CustomLoggerProvider(serviceCollections.BuildServiceProvider().GetRequiredService<ILoggerService>()));
            });
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, MyDashboardSettings appSettings)
        {
            var key = Encoding.UTF8.GetBytes(appSettings.Jwt.Key);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = appSettings.Jwt.Issuer,
                        ValidAudience = appSettings.Jwt.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddAuthorization();

            return services;
        }
    }
}
