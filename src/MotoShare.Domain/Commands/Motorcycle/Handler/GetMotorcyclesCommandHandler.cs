using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MotoShare.Domain.Commands.Motorcycle.Command;
using MotoShare.Domain.Repository.Base;

namespace MotoShare.Domain.Commands.Motorcycle.Handler;

public class GetMotorcyclesCommandHandler : IRequestHandler<GetMotorcyclesCommand, ResultModel>
{
    private readonly IRepository<Entities.Motorcycle> _motorcyleRepository;

    public GetMotorcyclesCommandHandler(IRepository<Entities.Motorcycle> motorcyleRepository)
    {
        _motorcyleRepository = motorcyleRepository;
    }

    public async Task<ResultModel> Handle(GetMotorcyclesCommand request, CancellationToken cancellationToken)
    {
        var result = await GetAsync(request.LicensePlate);
        return new ResultModel(true, "Dados obtidos com sucesso!", result);
    }

    public Task<List<Entities.Motorcycle>> GetAsync(string? licensePlate) => 
        Task.Run(() =>
        {
            var collection = _motorcyleRepository.AsQueryable();
            if (!string.IsNullOrEmpty(licensePlate))
            {
                collection = collection.Where(x => x.LicensePlate == licensePlate.ToUpper());
            }

            return collection.ToList();
        });
}
