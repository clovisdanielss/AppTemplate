namespace AppTemplate.AzureStorageBlobs.Interfaces
{
    public interface IAzureStorageBlobConfiguration
    {
        string StorageConnectionString { get; set; }
        string StorageContainerName { get; set; }
        string StorageEndpoint { get; set; }
    }
}
