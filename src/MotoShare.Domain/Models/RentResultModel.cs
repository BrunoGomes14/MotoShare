namespace MotoShare.Domain;

public class RentResultModel
{
    public RentResultModel(Guid id, DateTime startAt, string motorcycleLicensePlate)
    {
        Id = id;
        StartAt = startAt;
        MotorcycleLicensePlate = motorcycleLicensePlate;
    }

    public Guid Id { get; set; }
    public DateTime StartAt { get; set; }
    public string MotorcycleLicensePlate { get; set; }
}
