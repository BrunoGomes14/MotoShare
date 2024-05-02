using MotoShare.Domain.Repository.Base;

namespace MotoShare.Domain.Entities;

public class Plan : Entity
{
    public Plan(int days, decimal valuePerDay, decimal percentPenaltyForEarlyTermination, decimal extraDayValue, bool isActive)
    {
        Days = days;
        ValuePerDay = valuePerDay;
        PercentPenaltyForEarlyTermination = percentPenaltyForEarlyTermination;
        ExtraDayValue = extraDayValue;
        IsActive = isActive;
    }

    public int Days { get; private set; }
    public decimal ValuePerDay { get; private set; }
    public decimal PercentPenaltyForEarlyTermination { get; private set; }
    public decimal ExtraDayValue { get; private set; }
    public bool IsActive { get; private set; }
}
