using FluentValidation;
using MotoShare.Domain.Commands.DeliveryMen.Command;

namespace MotoShare.Domain.Commands.DeliveryMen.Validator;

public class SaveDriverLicenseImageCommandValidator : AbstractValidator<SaveDriverLicenseImageCommand>
{
    public SaveDriverLicenseImageCommandValidator()
    {
        var allowedTypes = new[] {".png", ".bmp"};

        RuleFor(x => x.File)
            .Must(x => allowedTypes.Contains(Path.GetExtension(x.FileName.ToLower())))
            .WithMessage($"Tipo de arquivo inválido. Tipos permitidos: {string.Join(", ", allowedTypes)}.");
    }
}
