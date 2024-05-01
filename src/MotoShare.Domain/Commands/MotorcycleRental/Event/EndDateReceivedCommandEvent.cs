using MediatR;
using MotoShare.Domain.Messaging;

namespace MotoShare.Domain.Commands.MotorcycleRental.Event;

public class EndDateReceivedCommandEvent : IEvent, IRequest
{
    public Guid RentalId { get; set; }

    public EndDateReceivedCommandEvent(Guid rentalId)
    {
        RentalId = rentalId;
    }
}
