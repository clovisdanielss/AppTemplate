using AppTemplate.AzureStorageBlobs.Models;
using AppTemplate.Shared.Interfaces;

namespace AppTemplate.AzureStorageBlobs.Interfaces
{
    public interface IAddToBlobService : IService<FileInput, string> { }
}
