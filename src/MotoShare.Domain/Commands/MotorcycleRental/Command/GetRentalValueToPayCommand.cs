using MediatR;

namespace MotoShare.Domain.Commands.MotorcycleRental.Command;

public record GetRentalValueToPayCommand(Guid RentalId) : IRequest<ResultModel>;
