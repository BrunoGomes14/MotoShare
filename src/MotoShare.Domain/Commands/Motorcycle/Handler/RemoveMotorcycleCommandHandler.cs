using MediatR;
using MotoShare.Domain.Commands.Motorcycle.Command;
using MotoShare.Domain.Repository.Base;

namespace MotoShare.Domain.Commands.Motorcycle.Handler;

public class RemoveMotorcycleCommandHandler : IRequestHandler<RemoveMotorcycleCommand, ResultModel>
{
    private readonly IRepository<Entities.Motorcycle> _motorcyleRepository;

    public RemoveMotorcycleCommandHandler(IRepository<Entities.Motorcycle> motorcyleRepository)
    {
        _motorcyleRepository = motorcyleRepository;
    }

    public async Task<ResultModel> Handle(RemoveMotorcycleCommand request, CancellationToken cancellationToken)
    {
        var moto = await _motorcyleRepository.FindByIdAsync(request.Id.ToString());
        if (!moto.IsAvailable)
            return new ResultModel(false, "A moto informada encontra-se alugada e não pode ser removida.");

        await _motorcyleRepository.DeleteByIdAsync(moto.Id.ToString());
        return new ResultModel(true, "Remoção realizada com sucesso.");
    }
}
