using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MotoShare.Infrastructure.Messaging;
using MotoShare.Infrastructure.Repositoires;

namespace MotoShare.Infrastructure;

public static class Bootstrapper
{
    public static void ConfigureInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        ILoggingBuilder loggingBuilder)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        // Implementação genérica para o Repositório do Mongo;
        services.AddMongoConfiguration();

        // Configura a mensageria
        services.AddRabbitMqPublisher(configuration);

        // Adiciona logs por arquivo
        loggingBuilder.AddFileLogger();
    }
}
