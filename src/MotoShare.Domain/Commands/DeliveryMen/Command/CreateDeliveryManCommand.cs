using MediatR;

namespace MotoShare.Domain.Commands.DeliveryMen.Command;

public record CreateDeliveryManCommand(
    string Name,
    string CompanyDocumentNumber,
    DateTime BirthDate,
    string DriverLicenseNumber,
    string DriverLicenseType) : IRequest<ResultModel>;
