using MediatR;
using MotoShare.Domain.Commands.MotorcycleRental.Command;
using MotoShare.Domain.Enumerators;
using MotoShare.Domain.Repository.Base;

namespace MotoShare.Domain.Commands.MotorcycleRental.Handler;

public class GetRentalValueToPayCommandHandler : IRequestHandler<GetRentalValueToPayCommand, ResultModel>
{
    private readonly IRepository<Entities.MotorcycleRental> _motorcycleRentalRepository;

    public GetRentalValueToPayCommandHandler(
        IRepository<Entities.MotorcycleRental> motorcycleRentalRepository)
    {
        _motorcycleRentalRepository = motorcycleRentalRepository;
    }

    public async Task<ResultModel> Handle(GetRentalValueToPayCommand request, CancellationToken cancellationToken)
    {
        var rental = await _motorcycleRentalRepository.FindByIdAsync(request.RentalId.ToString());
        if (rental == null)
            return new ResultModel(false, "Não foi possível obter o aluguel informado");

        if (rental.Status == StatusEnum.Error)
            return new ResultModel(false, "Infelizmente, houveram erros durante a finalização do seu aluguel. Entre em contato para mais informações.");

        if (rental.Status != StatusEnum.Finished)
            return new ResultModel(false, "Sua locação ainda não foi finalizada. Aguarde.");

        return new ResultModel(true, "Valor obtido com sucesso!", rental.ValueToPay);
    }
}
