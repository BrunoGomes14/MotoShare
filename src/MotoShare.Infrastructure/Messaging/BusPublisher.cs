using System.Reflection;
using MotoShare.Domain.Messaging;
using RawRabbit;
using RawRabbit.Configuration.Exchange;

namespace MotoShare.Infrastructure;

public class BusPublisher : IBusPublisher
{
    private readonly IBusClient _busClient;
    
    public BusPublisher(IBusClient busClient)
    {
        _busClient = busClient;
    }

    public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent
    {
        await _busClient.PublishAsync(@event, context =>
            context.UsePublishConfiguration(cfg => cfg 
                   .OnDeclaredExchange(e => 
                        e.WithName("moto-share-calculate")
                         .WithType(ExchangeType.Topic))
                   .WithRoutingKey(typeof(TEvent).Name)));
    }

    private static string GetQueueName<T>() => $"{Assembly.GetEntryAssembly()?.GetName().Name}/{typeof(T).Name}";
}