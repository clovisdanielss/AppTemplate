using AppTemplate.AzureStorageBlobs.Interfaces;
using AppTemplate.AzureStorageBlobs.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AppTemplate.AzureStorageBlobs.Extensions
{
    public static class AzureStorageBlobExtensions
    {
        public static IServiceCollection AddAzureStorageBlob(this IServiceCollection services, IAzureStorageBlobConfiguration configuration)
        {
            services.AddSingleton(configuration);
            services.AddScoped<IAddToBlobService, AddToBlobService>();
            services.AddScoped<IRemoveFromBlobService, RemoveFromBlobService>();

            return services;
        }
    }
}
