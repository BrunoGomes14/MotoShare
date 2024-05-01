using System.Reflection;
using Microsoft.OpenApi.Models;

namespace MotoShare.Api;

/// <summary>
/// Configura apectos visuais referentes a documentação swagger
/// </summary>
public static class SwaggerConfiguration
{
    /// <summary>
    /// </summary>
    public static void AddSwaggerConfiguration(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "MotoShare",
                    Version = "v1"
                }
             );
             
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });
    }
}
