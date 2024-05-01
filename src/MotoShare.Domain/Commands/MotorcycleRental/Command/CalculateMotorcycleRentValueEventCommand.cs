using MediatR;

namespace MotoShare.Domain;

public class CalculateMotorcycleRentValueCommand: IRequest<ResultModel>
{
    public Guid Id { get; private set; }

    public CalculateMotorcycleRentValueCommand(Guid id)
    {
        Id = id;
    }
}
