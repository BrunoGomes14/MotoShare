namespace MotoShare.Domain.Repository.Base;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


public abstract class Entity
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; protected set; }

    public DateTime CreatedAt { get; protected set; }
    public DateTime? UpdatedAt { get; protected set; }
}
