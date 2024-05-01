using MediatR;

namespace MotoShare.Domain.Commands.Motorcycle.Command;

public record RemoveMotorcycleCommand(Guid Id) : IRequest<ResultModel>;
