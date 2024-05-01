namespace MotoShare.Domain.Commands.Motorcycle.Command;

using MediatR;

public record CreateMotorcycleCommand(
    int Year,
    string Model,
    string LicensePlate) : IRequest<ResultModel>
{
}
