using MediatR;
using MotoShare.Domain.Commands.Motorcycle.Command;
using MotoShare.Domain.Repository.Base;

namespace MotoShare.Domain.Commands.Motorcycle.Handler;

public class UpdateMotorcycleLicensePlateCommandHandler : IRequestHandler<UpdateMotorcycleLicensePlateCommand, ResultModel>
{
    private readonly IRepository<Entities.Motorcycle> _motorcyleRepository;

    public UpdateMotorcycleLicensePlateCommandHandler(IRepository<Entities.Motorcycle> motorcyleRepository)
    {
        _motorcyleRepository = motorcyleRepository;
    }

    public async Task<ResultModel> Handle(UpdateMotorcycleLicensePlateCommand request, CancellationToken cancellationToken)
    {
        var motorcycle = await _motorcyleRepository.FindOneAsync(m => m.Id == request.Id);
        if (motorcycle == null)
            return new ResultModel(false, "A moto informada não foi encontrada em nossa base de dados.");
        
        var motoWithPlate = await _motorcyleRepository.FindOneAsync(x => x.LicensePlate == request.LicensePlate.ToUpper() && x.Id != request.Id);
        if (motoWithPlate != null)
            return new ResultModel(false, "A placa informada já esta cadastra em nossa base de dados.");

        motorcycle.Update(request.LicensePlate.ToUpper());
        await _motorcyleRepository.ReplaceOneAsync(motorcycle);

        return new ResultModel(true, "Placa atualizada com sucesso!", motorcycle.Id);
    }
}
