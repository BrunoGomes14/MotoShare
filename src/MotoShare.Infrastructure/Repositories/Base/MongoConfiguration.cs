using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MotoShare.Domain.Repository.Base;
using MotoShare.Infrastructure.Repositoires.Base;

namespace MotoShare.Infrastructure.Repositoires;

public static class MongoConfiguration
{
    public static void AddMongoConfiguration(this IServiceCollection services)
    {
        BsonSerializer.RegisterSerializer(typeof(decimal), new DecimalSerializer(BsonType.Decimal128));
        BsonSerializer.RegisterSerializer(typeof(decimal?), new NullableSerializer<decimal>(new DecimalSerializer(BsonType.Decimal128)));
        BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(BsonType.String));
        
        // Implementação genérica para o Repositório do Mongo;
        services.AddScoped(typeof(IRepository<>), typeof(MongoRepository<>));
    }
}
