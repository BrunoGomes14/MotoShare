using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MotoShare.Domain.Commands.MotorcycleRental.Event;
using MotoShare.Domain.Messaging;
using RawRabbit;
using RawRabbit.Configuration;
using RawRabbit.Enrichers.GlobalExecutionId;
using RawRabbit.Enrichers.MessageContext;
using RawRabbit.Enrichers.MessageContext.Context;
using RawRabbit.Instantiation;

namespace MotoShare.Infrastructure.Messaging;

public static class RabbitMqConfiguration
{
    public static void AddRabbitMqPublisher(this IServiceCollection services, IConfiguration configuration)
    {
        var options = new RawRabbitConfiguration();
        configuration.GetSection("RabbitMq").Bind(options);

        var client = RawRabbitFactory.CreateSingleton(new RawRabbitOptions
        {
            ClientConfiguration = options,
            Plugins = p => p
                .UseGlobalExecutionId()
                .UseHttpContext()
                .UseMessageContext(c => new MessageContext { GlobalRequestId = Guid.NewGuid() })
        });

        services.AddSingleton<IBusClient>(_ => client);
        services.AddScoped<IBusPublisher, BusPublisher>();
    }

    public static void AddRabbitMqSubscriber(this IApplicationBuilder app)
    {
        var subscriber = new BusSubscriber(app);
        subscriber.SubscribeEvent<EndDateReceivedCommandEvent>();
    }
}
