using MotoShare.Infrastructure.Repositoires.Base;

namespace MotoShare.Api.Configuration;

/// <summary>
/// Classe que configura dados de acesso ao banco mongo
/// </summary>
public static class MongoConfiguration
{
    /// <summary>
    /// </summary>
	public static void AddMongoConfiguration(this WebApplicationBuilder builder)
	{
		var section = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
		builder.Services.AddSingleton<MongoDbSettings>(section!);
	}
}
