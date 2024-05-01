namespace MotoShare.Domain.Commands.Motorcycle.Validator;

using FluentValidation;
using MotoShare.Domain.Commands.Motorcycle.Command;

public class CreateMotorcycleCommandValidator : AbstractValidator<CreateMotorcycleCommand>
{
    public CreateMotorcycleCommandValidator()
    {
        RuleFor(x => x.Model.Trim())
            .NotEmpty()
            .WithMessage("O modelo da moto deve ser informado.");   

        RuleFor(x => x.LicensePlate.Trim())
            .NotEmpty()
            .WithMessage("A placa da moto deve ser informada.");   

        RuleFor(x => x.LicensePlate)
            .Matches("[A-z]{3}-\\d[A-j0-9]\\d{2}")
            .WithMessage("A placa informada é inválida.");

        RuleFor(x => x.Year)
            .Must(x => x > DateTime.MinValue.Year)
            .WithMessage("Informe um ano válido.");   
    }
}
