using MotoShare.Domain.Commands.MotorcycleRental.Handler;
using MotoShare.Domain.Entities;

namespace MotoShare.Tests;

public class MotorcycleRentalTests
{
    [Fact]
    public void Calculate_NoExtraDays_CalculatesCorrectly()
    {
        var rental = new MotorcycleRental(
            planId: Guid.Empty,
            deliveryMenId: Guid.Empty,
            motorcycleId: Guid.Empty,
            startDate: new DateTime(2024, 4, 1),
            endDatePrediction: new DateTime(2024, 4, 5));

        rental.AddEndDate(new DateTime(2024, 4, 5));

        var plan = new Plan(
            days: 5,
            valuePerDay: 100,
            percentPenaltyForEarlyTermination: 10,
            extraDayValue: 50,
            true);

        var handler = new CalculateMotorcycleRentValueCommandHandler(default!, default!, default!);
        var result = handler.Calculate(rental, plan);

        Assert.Equal(400, result); 
    }

    [Fact]
    public void Calculate_EarlyTermination_CalculatesWithPenalty()
    {
        // Arrange
        var rental = new MotorcycleRental(
            planId: Guid.Empty,
            deliveryMenId: Guid.Empty,
            motorcycleId: Guid.Empty,
            startDate: new DateTime(2024, 4, 1),
            endDatePrediction: new DateTime(2024, 4, 5));

        rental.AddEndDate(new DateTime(2024, 4, 3));

        var plan = new Plan(
            days: 5,
            valuePerDay: 100,
            percentPenaltyForEarlyTermination: 10,
            extraDayValue: 50,
            true);

        var handler = new CalculateMotorcycleRentValueCommandHandler(default!, default!, default!);
        var result = handler.Calculate(rental, plan);

        // Assert
        Assert.Equal(220, result); 
    }

    [Fact]
    public void Calculate_ExtraDays_CalculatesWithExtraDayCharge()
    {
        // Arrange
        var rental = new MotorcycleRental(
            planId: Guid.Empty,
            deliveryMenId: Guid.Empty,
            motorcycleId: Guid.Empty,
            startDate: new DateTime(2024, 4, 1),
            endDatePrediction: new DateTime(2024, 4, 5));

        rental.AddEndDate(new DateTime(2024, 4, 7));

        var plan = new Plan(
            days: 5,
            valuePerDay: 100,
            percentPenaltyForEarlyTermination: 10,
            extraDayValue: 50,
            true);

        var handler = new CalculateMotorcycleRentValueCommandHandler(default!, default!, default!);
        var result = handler.Calculate(rental, plan);

        // Assert
        Assert.Equal(600, result);
    }
}