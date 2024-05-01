using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace MotoShare.Api;

/// <summary>
/// Configura biblioteca Mediator
/// </summary>
public static class MediatRConfiguration
{
    /// <summary>
    /// </summary>
    public static void AddMediatRConfiguration(this WebApplicationBuilder builder)
    {
        if (builder == null)
            throw new ArgumentNullException(nameof(builder));

        //Mediatr
        builder.Services.AddMediatR(configuration =>
        {
            //Como o Handler genérico é registrado manual, devemos excluir ele do mapeamento.
            //var exclude = new[] { typeof(AllProcessInstanceDomainEventHandler<>) };
            configuration.TypeEvaluator = new Func<Type, bool>((type) =>
            {

                //var result = !exclude.Contains(type);
                //return result;
                return true;
            });

            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); //API
            configuration.RegisterServicesFromAssembly(typeof(Domain.Bootstrapper).Assembly); //Dominio

        });
    }
}
