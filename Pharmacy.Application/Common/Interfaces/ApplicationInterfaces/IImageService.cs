using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces.ApplicationInterfaces
{
    public interface IImageService
    {
        Task CreateImages(IList<IFormFile> images, int medicamentId);

        Task DeleteImage(int imageId);
    }
}
