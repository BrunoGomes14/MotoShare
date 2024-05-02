using MediatR;
using MotoShare.Domain.Messaging;

namespace MotoShare.Domain;

public class MotorcycleCreatedEvent : IEvent, IRequest
{
    public Guid MotorcycleId { get; private set; }

    public MotorcycleCreatedEvent(Guid motorcycleId)
    {
        MotorcycleId = motorcycleId;
    }
}
