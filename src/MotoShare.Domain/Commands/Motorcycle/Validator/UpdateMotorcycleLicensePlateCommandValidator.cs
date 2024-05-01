using FluentValidation;
using MotoShare.Domain.Commands.Motorcycle.Command;

namespace MotoShare.Domain.Commands.Motorcycle.Validator;

public class UpdateMotorcycleLicensePlateCommandValidator : AbstractValidator<UpdateMotorcycleLicensePlateCommand>
{
    public UpdateMotorcycleLicensePlateCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("O identificador deve ser informado.");

        RuleFor(x => x.LicensePlate)
            .Matches("[A-z]{3}-\\d[A-j0-9]\\d{2}")
            .WithMessage("A placa informada é inválida.");
    }
}
