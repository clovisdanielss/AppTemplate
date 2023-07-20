using AppTemplate.AzureStorageBlobs.Interfaces;
using AppTemplate.AzureStorageBlobs.Models;
using AppTemplate.Shared.AbstractClasses;
using AppTemplate.Shared.Interfaces;
using Azure.Storage.Blobs;

namespace AppTemplate.AzureStorageBlobs.Services
{
    public class AddToBlobService : AbstractService, IAddToBlobService
    {
        private readonly BlobContainerClient _client;
        private readonly string _endpoint;
        public AddToBlobService(IAzureStorageBlobConfiguration configuration, INotifier notifier) : base(notifier)
        {
            string connectionString = configuration.StorageConnectionString ?? throw new ArgumentNullException(nameof(configuration));
            string containerName = configuration.StorageContainerName ?? throw new ArgumentNullException(nameof(configuration));
            _endpoint = configuration.StorageEndpoint + $"/{containerName}";
            _client = new BlobContainerClient(connectionString, containerName);
        }

        public async Task<string> HandleAsync(FileInput input)
        {
            try
            {
                if (string.IsNullOrEmpty(input.Base64))
                {
                    Notify("Não existe arquivo selecionado para upload.");
                    return string.Empty;
                }
                var fileName = Guid.NewGuid().ToString()+ "."+ input.Ext;
                var dataArray = Convert.FromBase64String(input.Base64.Contains(",") ? input.Base64.Split(",")[^1] : input.Base64);

                Azure.Response<Azure.Storage.Blobs.Models.BlobContentInfo> response = await _client.UploadBlobAsync(fileName, new MemoryStream(dataArray));
                if (response != null)
                {
                    return _endpoint + $"/{fileName}";
                }
                Notify("Não foi possível fazer upload do arquivo.");
            }
            catch (Exception)
            {
                Notify("Um arquivo com este nome já existe.");
            }
            return string.Empty;
        }
    }
}
