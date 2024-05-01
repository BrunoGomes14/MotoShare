using MotoShare.Domain.Repository.Base;

namespace MotoShare.Domain.Entities;

public class Motorcycle : Entity
{
    public Motorcycle(int year, string model, string licensePlate)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;

        Year = year;
        Model = model;
        LicensePlate = licensePlate;
        IsAvailable = true;
    }

    public int Year { get; private set; }
    public string Model { get; private set; }
    public string LicensePlate { get; private set; }
    public bool IsAvailable { get; private set; }

    public void Update(string licensePlate)
    {
        LicensePlate = licensePlate;
        UpdatedAt = DateTime.Now;
    }

    public void UpdateStatus(bool isAvailable)
    {
        IsAvailable = isAvailable;
        UpdatedAt = DateTime.Now;
    }
}
