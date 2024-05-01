using MediatR;
using MotoShare.Domain.Commands.Motorcycle.Command;
using MotoShare.Domain.Repository.Base;

namespace MotoShare.Domain.Commands.Motorcycle.Handler;

public class CreateMotorcycleCommandHandler : IRequestHandler<CreateMotorcycleCommand, ResultModel>
{
    private readonly IRepository<Entities.Motorcycle> _motorcyleRepository;

    public CreateMotorcycleCommandHandler(IRepository<Entities.Motorcycle> motorcyleRepository)
    {
        _motorcyleRepository = motorcyleRepository;
    }

    public async Task<ResultModel> Handle(CreateMotorcycleCommand request, CancellationToken cancellationToken)
    {
        var moto = await _motorcyleRepository.FindOneAsync(mt => mt.LicensePlate == request.LicensePlate.ToUpper());
        if (moto != null)
            return new ResultModel(false, "A placa informada já se encontra cadastrada em nossa base de dados.");

        moto = new Entities.Motorcycle(
            request.Year,
            request.Model,
            request.LicensePlate.ToUpper()
        );

        await _motorcyleRepository.InsertOneAsync(moto);

        return new ResultModel(true, "Cadastro realizado com sucesso!", moto.Id);
    }
}
