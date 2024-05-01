using MediatR;
using Microsoft.AspNetCore.Http;
using MotoShare.Domain.Commands.DeliveryMen.Command;
using MotoShare.Domain.Repository.Base;

namespace MotoShare.Domain.Commands.DeliveryMen.Handler;

public class SaveDriverLicenseImageCommandHandler : IRequestHandler<SaveDriverLicenseImageCommand, ResultModel>
{
    private readonly IRepository<Entities.DeliveryMen> _deliveryMenRepository;
    private readonly string _directoryPath;

    public SaveDriverLicenseImageCommandHandler(
        IRepository<Entities.DeliveryMen> deliveryMenRepository)
    {
        _deliveryMenRepository = deliveryMenRepository;
        _directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Files");
    }

    public async Task<ResultModel> Handle(SaveDriverLicenseImageCommand request, CancellationToken cancellationToken)
    {
        var deliveryMen = await _deliveryMenRepository.FindByIdAsync(request.Id.ToString());
        if (deliveryMen == null)
            return new ResultModel(false, "Entregador informado não encontrado em nossa base de dados.");

        var imageId = await SaveFile(request.File);
        deliveryMen.AddDriverLIcenseImageName(imageId);

        await _deliveryMenRepository.ReplaceOneAsync(deliveryMen);

        return new ResultModel(true, "Imagem adicionada com sucesso.");
    }

    private async Task<string> SaveFile(IFormFile formFile)
    {
        if (!Path.Exists(_directoryPath))
            Directory.CreateDirectory(_directoryPath);

        var fileExtension = Path.GetExtension(formFile.FileName).ToLower();

        var identifier = Guid.NewGuid();
        var fileName = $"{identifier}{fileExtension}";

        var filePath = Path.Combine(_directoryPath, fileName);
        using (Stream fileStream = new FileStream(filePath, FileMode.Create))
        {
            await formFile.CopyToAsync(fileStream);
        }

        return fileName;
    }
}
