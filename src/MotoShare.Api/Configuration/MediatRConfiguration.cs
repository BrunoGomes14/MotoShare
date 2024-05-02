using System.Reflection;

namespace MotoShare.Api;

/// <summary>
/// Configura biblioteca de Mediator
/// </summary>
public static class MediatRConfiguration
{
    /// <summary>
    /// </summary>
    public static void AddMediatRConfiguration(this WebApplicationBuilder builder)
    {
        if (builder == null)
            throw new ArgumentNullException(nameof(builder));

        builder.Services.AddMediatR(configuration =>
        {
            configuration.TypeEvaluator = new Func<Type, bool>((type) => true);
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); //API
            configuration.RegisterServicesFromAssembly(typeof(Domain.Bootstrapper).Assembly); //Dominio

        });
    }
}
