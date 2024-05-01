using MotoShare.Domain.Enumerators;
using MotoShare.Domain.Repository.Base;

namespace MotoShare.Domain.Entities;

public class MotorcycleRental : Entity
{
    public MotorcycleRental(Guid planId, Guid deliveryMenId, Guid motorcycleId, DateTime startDate, DateTime endDatePrediction)
    {
        Id = Guid.NewGuid();
        Status = StatusEnum.Created;
        CreatedAt = DateTime.Now;

        PlanId = planId;
        DeliveryMenId = deliveryMenId;
        MotorcycleId = motorcycleId;
        StartDate = startDate;
        EndDatePrediction = endDatePrediction;
    }

    public Guid PlanId { get; private set; }
    public Guid DeliveryMenId { get; private set; }
    public Guid MotorcycleId { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDatePrediction { get; private set; }
    public DateTime? EndDateReported { get; private set; }
    public StatusEnum Status { get; private set; }
    public decimal? ValueToPay { get; private set; }

    public void AddEndDate(DateTime endDate)
    {
        EndDateReported  = endDate;
        Status  = StatusEnum.EndDateReported;

        UpdatedAt = DateTime.Now;
    }

    public void UpdateStatus(StatusEnum status)
    {
        Status = status;
        UpdatedAt = DateTime.Now;
    } 

    public void AddValueToPay(decimal valueToPay)
    {
        ValueToPay = valueToPay;
        Status = StatusEnum.Finished;
        UpdatedAt = DateTime.Now;
    }
}
