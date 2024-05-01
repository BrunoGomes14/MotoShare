using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MotoShare.Domain.Messaging;
using RawRabbit;
using RawRabbit.Configuration.Exchange;

namespace MotoShare.Infrastructure;

public class BusSubscriber : IBusSubscriber
{
    private readonly IBusClient _busClient;
    private readonly IServiceProvider _serviceProvider;

    public BusSubscriber(IApplicationBuilder app)
    {
        _serviceProvider = app.ApplicationServices;
        _busClient = _serviceProvider.GetService<IBusClient>()!;
    }

    public IBusSubscriber SubscribeEvent<TEvent>() where TEvent : IEvent, IRequest
    {
        _busClient.SubscribeAsync<TEvent>(async (@event) =>
        {
            using var scope = _serviceProvider.CreateScope();
            var logger = _serviceProvider.GetService<ILogger<BusSubscriber>>()!;

            try
            {
                var mediator = scope.ServiceProvider.GetService<IMediator>();
                await mediator!.Send(@event);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }

        }, ctx => ctx.UseSubscribeConfiguration(cfg => cfg
            .Consume(c => c.WithRoutingKey(typeof(TEvent).Name))
            .FromDeclaredQueue(q => q
                .WithName(GetQueueName<TEvent>())
                .WithDurability()
                .WithAutoDelete(false))
            .OnDeclaredExchange(e => e
                .WithName("moto-share-calculate")
                .WithType(ExchangeType.Topic)
                .WithArgument("key", typeof(TEvent).Name.ToLower()))
        ));

        return this;
    }

    private static string GetQueueName<T>() => $"{Assembly.GetEntryAssembly()?.GetName().Name}/{typeof(T).Name}";
}
