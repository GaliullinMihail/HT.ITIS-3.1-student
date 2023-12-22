using Dotnet.Homeworks.Mailing.API.Configuration;
using MassTransit;

namespace Dotnet.Homeworks.Mailing.API.ServicesExtensions;

public static class AddMasstransitRabbitMqExtensions
{
    public static IServiceCollection AddMasstransitRabbitMq(this IServiceCollection services,
        IConfiguration mainConfig)
    {
        var rabbitConfig = new RabbitMqConfig
        {
            Hostname = mainConfig["RabbitMqConfig:Hostname"]!,
            Password = mainConfig["RabbitMqConfig:Password"]!,
            Username = mainConfig["RabbitMqConfig:Username"]!
        };
        services.AddMassTransit(busConfig =>
        {
            busConfig.UsingRabbitMq((context, config) =>
            {
                config.Host(rabbitConfig.Hostname, hostConfigurator =>
                {
                    hostConfigurator.Username(rabbitConfig.Username);
                    hostConfigurator.Password(rabbitConfig.Password);
                });
                config.ConfigureEndpoints(context);
            });
        });
        return services;
    }
}