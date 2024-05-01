using MediatR;
using MotoShare.Domain.Commands.MotorcycleRental.Command;
using MotoShare.Domain.Entities;
using MotoShare.Domain.Repository.Base;

namespace MotoShare.Domain.Commands.MotorcycleRental.Handler;

public class CreateMotorcycleRentCommandHandler : IRequestHandler<CreateMotorcycleRentCommand, ResultModel>
{
    private readonly IRepository<Plan> _planRepository;
    private readonly IRepository<Entities.Motorcycle> _motorcycleRepository;
    private readonly IRepository<Entities.MotorcycleRental> _motorcycleRentalRepository;
    private readonly IRepository<Entities.DeliveryMen> _deliveryMenRepository;

    public CreateMotorcycleRentCommandHandler(
        IRepository<Plan> planRepository, 
        IRepository<Entities.Motorcycle> motorcycleRepository, 
        IRepository<Entities.MotorcycleRental> motorcycleRentalRepository, 
        IRepository<Entities.DeliveryMen> deliveryMenRepository)
    {
        _planRepository = planRepository;
        _motorcycleRepository = motorcycleRepository;
        _motorcycleRentalRepository = motorcycleRentalRepository;
        _deliveryMenRepository = deliveryMenRepository;
    }

    public async Task<ResultModel> Handle(CreateMotorcycleRentCommand request, CancellationToken cancellationToken)
    {
        var plan = await _planRepository.FindByIdAsync(request.PlanId.ToString());
        if (plan == null)
            return new ResultModel(true, "O plano informado não foi encontrado em nossa base de dados.");

        var deliveryMen = await _deliveryMenRepository.FindOneAsync(x => x.Id == request.DeliveryManId);
        if (deliveryMen == null)
            return new ResultModel(false, "Não foi possível encontrar o entregador informado.");

        if (!deliveryMen.DriverLicenseType.Contains("A"))
            return new ResultModel(false, "Infelizmente para alugar uma moto o entregador deve estar habilitado na categoria A");

        var motorcycle = await _motorcycleRepository.FindOneAsync(mt => mt.IsAvailable);
        if (motorcycle == null)
            return new ResultModel(false, "Infelizemente, não há motos disponíveis no momento.");

        // O inicio da locação obrigatóriamente é o primeiro dia após a data de criação.
        var startDate = DateTime.Now.AddDays(1).Date;
        var endDatePrediction = startDate.AddDays(plan.Days);

        var rent = new Entities.MotorcycleRental(
            plan.Id,
            deliveryMen.Id,
            motorcycle.Id,
            startDate,
            endDatePrediction);

        await _motorcycleRentalRepository.InsertOneAsync(rent);

        // definimos a moto como indisponível
        motorcycle.UpdateStatus(false);
        await _motorcycleRepository.ReplaceOneAsync(motorcycle);

        var result = new RentResultModel(rent.Id, startDate, motorcycle.LicensePlate);
        return new ResultModel(true, "Aluguel realizado com sucesso!", result);
    }
}
