using Microsoft.AspNetCore.Http;
using Pharmacy.Application.Common.DTO.In.BlobIn;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces
{
    public interface IBlobStorage
    {
        Task<IEnumerable<string>> GetBlobsNamesAsync();

        Task<BlobInfoDto> GetBlobAsync(string blobName);

        Task<string> UploadBlobAsync(IFormFile blob);

        Task DeleteBlobAsync(string blobName);
    }
}
