namespace AppTemplate.Data.MongoDB.Interfaces
{
    public interface IMongoConfiguration
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
