using AppTemplate.AzureStorageBlobs.Interfaces;
using AppTemplate.AzureStorageBlobs.Services;
using AppTemplate.Shared.Interfaces;
using AppTemplate.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AppTemplate.AzureStorageBlobs.Extensions
{
    public static class AzureStorageBlobExtensions
    {
        public static IServiceCollection AddAzureStorageBlob(this IServiceCollection services, IAzureStorageBlobConfiguration configuration)
        {
            services.TryAddScoped<INotifier, NotifierService>();
            services.AddSingleton(configuration);
            services.AddScoped<IAddToBlobService, AddToBlobService>();
            services.AddScoped<IRemoveFromBlobService, RemoveFromBlobService>();

            return services;
        }
    }
}
