using MediatR;

namespace MotoShare.Domain;

public record SaveMotorcycleRentEndDateCommand(Guid RentId, DateTime EndDate) : IRequest<ResultModel>;
