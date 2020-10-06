using Hoshin.Quality.Domain.Evidence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.Repositories
{
    public interface IAzureStorageRepository
    {
        Task<string> InsertFileAzureStorage(Evidence file, string containerName);
        Task<string> InsertFileAzureStorage(Evidence file, TypeData type, string containerName);
        Task<string> DeleteFileAzureStorage(Evidence file, string containerName);
    }
    public enum TypeData
    {
        Base64,
        Byte
    }
}
