using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace MotoShare.Api;

/// <summary>
/// Configura biblioteca de validações 
/// </summary>
public static class FluentValidationConfiguration
{
    /// <summary>
    /// </summary>
    public static void AddFluentValidationConfiguration(this WebApplicationBuilder builder)
    {
        var assemblies = new Assembly[]
        {
            typeof(Domain.Bootstrapper).Assembly,
        };

        AssemblyScanner
            .FindValidatorsInAssemblies(assemblies)
            .ForEach(result =>
            {
                builder.Services.AddScoped(result.InterfaceType, result.ValidatorType);
            });

        builder.Services.AddFluentValidationAutoValidation();
    }
}
