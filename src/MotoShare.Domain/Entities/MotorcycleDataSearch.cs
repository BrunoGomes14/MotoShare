using MotoShare.Domain.Entities;
using MotoShare.Domain.Repository.Base;

namespace MotoShare.Domain;

public class MotorcycleDataSearch : Entity
{
    public MotorcycleDataSearch(Motorcycle motorcycle)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;

        MotorcycleId = motorcycle.Id;
        Year = motorcycle.Year;
        Model = motorcycle.Model;
    }
    
    public Guid MotorcycleId { get; set; }
    public int Year { get; private set; }
    public string Model { get; private set; }

}
