using AppTemplate.AzureStorageBlobs.Interfaces;
using AppTemplate.AzureStorageBlobs.Services;
using AppTemplate.Shared.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AppTemplate.AzureStorageBlobs.Extensions
{
    public static class AzureStorageBlobExtensions
    {
        public static IServiceCollection AddAzureStorageBlob(this IServiceCollection services, IAzureStorageBlobConfiguration configuration)
        {
            services.AddSingleton(configuration);
            services.AddScoped<IAddToBlobService, AddToBlobService>();
            services.AddScoped<IRemoveFromBlobService, RemoveFromBlobService>();
            services.TryAddScoped<INotifier, INotifier>();

            return services;
        }
    }
}
