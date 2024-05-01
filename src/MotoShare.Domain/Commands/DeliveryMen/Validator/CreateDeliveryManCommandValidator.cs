using FluentValidation;
using MotoShare.Domain.Commands.DeliveryMen.Command;

namespace MotoShare.Domain.Commands.DeliveryMen.Validator;

public class CreateDeliveryManCommandValidator : AbstractValidator<CreateDeliveryManCommand>
{
    public CreateDeliveryManCommandValidator()
    {
        RuleFor(x => x.Name.Trim())
            .NotEmpty()
            .WithMessage("O nome deve ser informado.");  

        RuleFor(x => x.CompanyDocumentNumber)
            .Matches("([0-9]{2}[\\.]?[0-9]{3}[\\.]?[0-9]{3}[\\/]?[0-9]{4}[-]?[0-9]{2})|([0-9]{3}[\\.]?[0-9]{3}[\\.]?[0-9]{3}[-]?[0-9]{2})")
            .WithMessage("O CNPJ informado é inválido.");

        RuleFor(x => x.BirthDate)
            .Must(x => x.AddYears(18).Date < DateTime.Now.Date)
            .WithMessage("Cadastro permitido somente para pessoas acima de 18 anos.");

        RuleFor(x => x.DriverLicenseNumber)
            .Matches("^\\d{11}$")
            .WithMessage("A carteira de habilitação informada é inválida.");

        var allowedTypes = new[] { "A", "B", "AB"};
        RuleFor(x => x.DriverLicenseType)
            .Must(x => allowedTypes.Contains(x.ToUpper()))
            .WithMessage("O tipo da habilitação informada não é permitido.");
    }
}
