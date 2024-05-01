using MediatR;

namespace MotoShare.Domain.Commands.Motorcycle.Command;

public record UpdateMotorcycleLicensePlateCommand(Guid Id, string LicensePlate) : IRequest<ResultModel>;
