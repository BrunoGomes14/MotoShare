using MediatR;
using MotoShare.Domain.Entities;
using MotoShare.Domain.Enumerators;
using MotoShare.Domain.Repository.Base;

namespace MotoShare.Domain.Commands.MotorcycleRental.Handler;

public class CalculateMotorcycleRentValueCommandHandler : IRequestHandler<CalculateMotorcycleRentValueCommand, ResultModel>
{
    private readonly IRepository<Entities.MotorcycleRental> _motorcycleRentalRepository;
    private readonly IRepository<Entities.Motorcycle> _motorcycleRepository;
    private readonly IRepository<Plan> _planRepository;

    public CalculateMotorcycleRentValueCommandHandler(
        IRepository<Entities.MotorcycleRental> motorcycleRentalRepository,
        IRepository<Entities.Motorcycle> motorcycleRepository,
        IRepository<Plan> planRepository)
    {
        _motorcycleRentalRepository = motorcycleRentalRepository;
        _motorcycleRepository = motorcycleRepository;
        _planRepository = planRepository;
    }

    public async Task<ResultModel> Handle(CalculateMotorcycleRentValueCommand request, CancellationToken cancellationToken)
    {
        var rental = await _motorcycleRentalRepository.FindByIdAsync(request.Id.ToString());
        if (rental == null)
            return new ResultModel(false, "Aluguel informado não foi encontrado");

        if (rental.Status != StatusEnum.EndDateReported)
            return new ResultModel(false, "Aluguel informado não possui data de término informada");

        rental.UpdateStatus(StatusEnum.CalculatingPrice);
        await _motorcycleRentalRepository.ReplaceOneAsync(rental);

        var plan = await _planRepository.FindByIdAsync(rental.PlanId.ToString());
        var valueToPay = Calculate(rental, plan);

        rental.AddValueToPay(valueToPay);
        await _motorcycleRentalRepository.ReplaceOneAsync(rental);

        var moto = await _motorcycleRepository.FindByIdAsync(rental.MotorcycleId.ToString());
        moto.UpdateStatus(true);
        await _motorcycleRepository.ReplaceOneAsync(moto);

        return new ResultModel(true, "Calculo realizado com sucesso!");
    }

    public decimal Calculate(Entities.MotorcycleRental rental, Plan plan)
    {
        var daysCount = (rental.EndDateReported!.Value.Date - rental.StartDate.Date).Days;
        var daysDiff = (rental.EndDateReported!.Value.Date - rental.EndDatePrediction.Date).Days;

        var muliplyValue = daysCount > plan.Days ? plan.Days : daysCount;
        decimal valueToPay = muliplyValue * plan.ValuePerDay;

        // se houve sobra de dias, aplicamos a multa
        if (daysDiff < 0)
        {
            var positiveDiff = Math.Abs(daysDiff);
            valueToPay += positiveDiff * (plan.ValuePerDay * (plan.PercentPenaltyForEarlyTermination / 100));
        }
        // se ultrapassou, aplicamos o valor de dias extras
        else if (daysDiff > 0)
        {
            valueToPay += daysDiff *  plan.ExtraDayValue;
        }

        return valueToPay;
    }
}
