using FluentValidation;
using MotoShare.Domain.Commands.MotorcycleRental.Command;

namespace MotoShare.Domain.Commands.MotorcycleRental.Validator;

public class CreateMotorcycleRentCommandValidator : AbstractValidator<CreateMotorcycleRentCommand>
{
    public CreateMotorcycleRentCommandValidator()
    {
        RuleFor(x => x.PlanId)
            .NotEmpty()
            .WithMessage("O identificador do plano deve ser informado.");

        RuleFor(x => x.DeliveryManId)
            .NotEmpty()
            .WithMessage("O identificador do entregador deve ser informado.");
    }
}
