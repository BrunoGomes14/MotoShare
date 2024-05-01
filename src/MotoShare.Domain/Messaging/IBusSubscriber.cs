using MediatR;

namespace MotoShare.Domain.Messaging;

public interface IBusSubscriber
{
    IBusSubscriber SubscribeEvent<TEvent>() where TEvent : IEvent, IRequest;
}
