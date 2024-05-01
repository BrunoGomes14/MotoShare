using MediatR;
using MotoShare.Domain.Commands.MotorcycleRental.Command;
using MotoShare.Domain.Entities;
using MotoShare.Domain.Repository.Base;

namespace MotoShare.Domain;

public class GetMotorycycleRentPlansCommandHandler : IRequestHandler<GetMotorycycleRentPlansCommand, ResultModel>
{
    private readonly IRepository<Plan> _planRepository;

    public GetMotorycycleRentPlansCommandHandler(IRepository<Plan> planRepository)
    {
        _planRepository = planRepository;
    }

    public async Task<ResultModel> Handle(GetMotorycycleRentPlansCommand request, CancellationToken cancellationToken)
    {
        var plans = await _planRepository.FilterByAsync(x => x.IsActive);
        return new ResultModel(true, "Planos obtidos com sucesso.", plans);
    }
}
