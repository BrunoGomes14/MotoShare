using FluentValidation;

namespace MotoShare.Domain;

public class SaveMotorcycleRentEndDateCommadValidator : AbstractValidator<SaveMotorcycleRentEndDateCommand>
{
    public SaveMotorcycleRentEndDateCommadValidator()
    {
        RuleFor(x => x.RentId)
            .NotEmpty()
            .WithMessage("O identificador do aluguel deve ser informado.");

        RuleFor(x => x.EndDate)
            .Must(x => x.Date >= DateTime.Now.Date)
            .WithMessage("A data de devolução não pode ser menor que a data atual.");
    }
}
