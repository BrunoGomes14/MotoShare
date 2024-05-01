using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MotoShare.Domain;

namespace MotoShare.Api;

/// <summary>
/// Cria um padrão para retornos automáticos da aplicação
/// </summary>
public static class InvalidModelStateConfiguration
{
    /// <summary>
    /// Aplica padrão
    /// </summary>
    public static IActionResult ConfigureResult(this ActionContext context)
    {
        var messages = context.ModelState.Values
            .Where(x => x.ValidationState == ModelValidationState.Invalid)
            .SelectMany(x => x.Errors)
            .Select(x => x.ErrorMessage)
            .ToList();

        return new BadRequestObjectResult(new ResultModel(false, messages.First()));
    }
}
