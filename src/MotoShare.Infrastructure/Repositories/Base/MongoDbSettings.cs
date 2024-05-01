namespace MotoShare.Infrastructure.Repositoires.Base;

public class MongoDbSettings
{
        public string DatabaseName { get; set; } = string.Empty;
        public string HangfireDatabaseName { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
}
