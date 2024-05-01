using System.Text.RegularExpressions;
using MediatR;
using MotoShare.Domain.Commands.DeliveryMen.Command;
using MotoShare.Domain.Repository.Base;

namespace MotoShare.Domain.Commands.DeliveryMen.Handler;

public class CreateDeliveryManCommandHandler : IRequestHandler<CreateDeliveryManCommand, ResultModel>
{
    private readonly IRepository<Entities.DeliveryMen> _deliveryMenRepository;

    public CreateDeliveryManCommandHandler(IRepository<Entities.DeliveryMen> deliveryMenRepository)
    {
        _deliveryMenRepository = deliveryMenRepository;
    }

    public async Task<ResultModel> Handle(CreateDeliveryManCommand request, CancellationToken cancellationToken)
    {
        var deliveryMen = await _deliveryMenRepository.FindOneAsync(x => x.DriverLicenseNumber == request.DriverLicenseNumber);
        if (deliveryMen != null)
            return new ResultModel(false, "A carteira de habilitação informada já está cadastrada em nossa base de dados.");

        deliveryMen = await _deliveryMenRepository.FindOneAsync(x => x.CompanyDocumentNumber == request.CompanyDocumentNumber);
        if (deliveryMen != null)
            return new ResultModel(false, "O CNPJ informado já está cadastrado em nossa base de dados.");

        var normalizedDocument = Regex.Replace(request.CompanyDocumentNumber, "[-.]", "");
        deliveryMen = new Entities.DeliveryMen(
            request.Name,
            normalizedDocument,
            request.BirthDate,
            request.DriverLicenseNumber,
            request.DriverLicenseType
        );
        await _deliveryMenRepository.InsertOneAsync(deliveryMen);

        return new ResultModel(true, "Entregador(a) cadastro com sucesso!", deliveryMen.Id);
    }
}
