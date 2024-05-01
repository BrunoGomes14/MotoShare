using MediatR;
using MotoShare.Domain.Commands.MotorcycleRental.Event;
using MotoShare.Domain.Messaging;
using MotoShare.Domain.Repository.Base;

namespace MotoShare.Domain.Commands.MotorcycleRental.Handler;

public class SaveMotorcycleRentEndDateCommandHandler : IRequestHandler<SaveMotorcycleRentEndDateCommand, ResultModel>
{
    private readonly IRepository<Entities.MotorcycleRental> _rentalRepository;
    private readonly IBusPublisher _busPublisher;

    public SaveMotorcycleRentEndDateCommandHandler(
        IRepository<Entities.MotorcycleRental> rentalRepository, 
        IBusPublisher busPublisher)
    {
        _rentalRepository = rentalRepository;
        _busPublisher = busPublisher;
    }

    public async Task<ResultModel> Handle(SaveMotorcycleRentEndDateCommand request, CancellationToken cancellationToken)
    {
        var rental = await _rentalRepository.FindByIdAsync(request.RentId.ToString());
        if (rental == null)
            return new ResultModel(false, "O registro de aluguel não foi encontrado em nossa base de dados.");

        if (rental.Status != Enumerators.StatusEnum.Created)
            return new ResultModel(false, "Este aluguel já foi finalizado.");

        rental.AddEndDate(request.EndDate);
        await _rentalRepository.ReplaceOneAsync(rental);

        var message = new EndDateReceivedCommandEvent(rental.Id);
        await _busPublisher.PublishAsync(message);

        return new ResultModel(true, "Data de devolução informada com sucesso!");
    }
}
