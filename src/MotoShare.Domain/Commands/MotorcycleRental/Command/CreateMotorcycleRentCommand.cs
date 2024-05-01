using MediatR;

namespace MotoShare.Domain.Commands.MotorcycleRental.Command;

public record CreateMotorcycleRentCommand(Guid PlanId, Guid DeliveryManId) : IRequest<ResultModel>;
