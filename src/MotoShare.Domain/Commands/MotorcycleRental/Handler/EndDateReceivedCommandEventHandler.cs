using MediatR;
using Microsoft.Extensions.Logging;
using MotoShare.Domain.Commands.MotorcycleRental.Event;
using MotoShare.Domain.Entities;
using MotoShare.Domain.Repository.Base;

namespace MotoShare.Domain;

public class EndDateReceivedCommandEventHandler : IRequestHandler<EndDateReceivedCommandEvent>
{
    private readonly IMediator _mediator;
    private readonly IRepository<MotorcycleRental> _repository;
    private readonly ILogger<EndDateReceivedCommandEventHandler> _logger;

    public EndDateReceivedCommandEventHandler(
        IMediator mediator,
        IRepository<MotorcycleRental> repository,
        ILogger<EndDateReceivedCommandEventHandler> logger)
    {
        _mediator = mediator;
        _repository = repository;
        _logger = logger;
    }

    public async Task Handle(EndDateReceivedCommandEvent request, CancellationToken cancellationToken)
    {
        try 
        {
            var calculateCommand = new CalculateMotorcycleRentValueCommand(request.RentalId);
            var result = await _mediator.Send(calculateCommand);

            if (!result.Success)
                await UpdateStatusToError(request.RentalId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            await UpdateStatusToError(request.RentalId);
        }
    }

    private async Task UpdateStatusToError(Guid id)
    {
        var rent = await _repository.FindByIdAsync(id.ToString());
        rent.UpdateStatus(Enumerators.StatusEnum.Error);

        await _repository.ReplaceOneAsync(rent);
    }
}
