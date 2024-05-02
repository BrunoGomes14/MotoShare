using MediatR;
using MotoShare.Domain.Entities;
using MotoShare.Domain.Repository.Base;

namespace MotoShare.Domain;

public class MotorcycleCreatedEventHandler : IRequestHandler<MotorcycleCreatedEvent>
{
    private readonly IRepository<Motorcycle> _motorcycleRepository;
    private readonly IRepository<MotorcycleDataSearch> _motorcycleDataSearchRepository;

    public MotorcycleCreatedEventHandler(
        IRepository<Motorcycle> motorcycleRepository, 
        IRepository<MotorcycleDataSearch> motorcycleDataSearchRepository)
    {
        _motorcycleRepository = motorcycleRepository;
        _motorcycleDataSearchRepository = motorcycleDataSearchRepository;
    }

    public async Task Handle(MotorcycleCreatedEvent request, CancellationToken cancellationToken)
    {
        var moto = await _motorcycleRepository.FindByIdAsync(request.MotorcycleId.ToString());
        if (moto.Year != 2024)
            return;

        var dataSearch = new MotorcycleDataSearch(moto);
        await _motorcycleDataSearchRepository.InsertOneAsync(dataSearch);   
    }
}
