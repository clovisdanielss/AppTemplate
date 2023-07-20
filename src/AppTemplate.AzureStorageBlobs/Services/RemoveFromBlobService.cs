using AppTemplate.AzureStorageBlobs.Interfaces;
using AppTemplate.Shared.AbstractClasses;
using AppTemplate.Shared.Interfaces;
using Azure.Storage.Blobs;

namespace AppTemplate.AzureStorageBlobs.Services
{
    public class RemoveFromBlobService : AbstractService, IRemoveFromBlobService
    {
        private readonly BlobContainerClient _client;
        private readonly string _endpoint;
        public RemoveFromBlobService(IAzureStorageBlobConfiguration configuration, INotifier notifier) : base(notifier)
        {
            string connectionString = configuration.StorageConnectionString ?? throw new ArgumentNullException(nameof(configuration));
            string containerName = configuration.StorageContainerName ?? throw new ArgumentNullException(nameof(configuration));
            _client = new BlobContainerClient(connectionString, containerName);
            _endpoint = configuration.StorageEndpoint + $"/{containerName}";
        }

        public async Task HandleAsync(string fileLink)
        {
            try
            {
                if (!fileLink.Contains(_endpoint))
                {
                    Notify("Link de arquivo inválido");
                    return;
                }
                var itemName = fileLink.Split('/')[^1];
                Azure.Response response = _client.DeleteBlob(itemName);
                if (response != null)
                {
                    return;
                }
                else
                {
                    Notify("O servidor não respondeu o chamado para deleção.");
                }
            }
            catch (Exception)
            {
                Notify("Um arquivo com este nome já existe.");
            }
        }
    }
}
