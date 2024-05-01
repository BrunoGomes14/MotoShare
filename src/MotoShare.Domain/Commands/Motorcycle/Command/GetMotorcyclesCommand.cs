using MediatR;

namespace MotoShare.Domain.Commands.Motorcycle.Command;

public record GetMotorcyclesCommand(string? LicensePlate) : IRequest<ResultModel>;
