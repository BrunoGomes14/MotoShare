namespace MotoShare.Domain.Commands.DeliveryMen.Command;

using MediatR;
using Microsoft.AspNetCore.Http;

public record SaveDriverLicenseImageCommand(
    Guid Id,
    IFormFile File) : IRequest<ResultModel>;
