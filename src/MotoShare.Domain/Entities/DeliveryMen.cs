using MotoShare.Domain.Repository.Base;

namespace MotoShare.Domain.Entities;

public class DeliveryMen : Entity
{
    public DeliveryMen(string name, string companyDocumentNumber, DateTime birthDate, string driverLicenseNumber, string driverLicenseType)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;

        Name = name;
        CompanyDocumentNumber = companyDocumentNumber;
        BirthDate = birthDate;
        DriverLicenseNumber = driverLicenseNumber;
        DriverLicenseType = driverLicenseType;
    }

    public string Name { get; private set; }
    public string CompanyDocumentNumber { get; private set; }
    public DateTime BirthDate { get; private set; }
    public string DriverLicenseNumber { get; private set; }
    public string DriverLicenseType { get; private set; }
    public string? DriverLicenseImageName { get; private set; }

    public void AddDriverLIcenseImageName(string driverLicenseImageName)
    {
        DriverLicenseImageName = driverLicenseImageName;
        UpdatedAt = DateTime.Now;
    }
}
