using Microsoft.AspNetCore.Http;
using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Domain.Entites;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Pharmacy.Application.Services
{
    public class ImageService : IImageService
    {
        private readonly IBlobStorage _blobStorage;

        private readonly IRepository<Image> _repository;

        public ImageService(IBlobStorage blobStorage, IRepository<Image> repository)
        {
            _blobStorage = blobStorage;
            _repository = repository;
        }

        private async Task CreateImage(IFormFile image, int medicamentId)
        {
            var imageUri = await _blobStorage.UploadBlobAsync(image);

            await _repository.Create(new Image() { Name = image.FileName, Uri = imageUri, MedicamentId = medicamentId });
        }

        public async Task CreateImages(IList<IFormFile> images, int medicamentId)
        {
            if (images != null && images.Any())
            {
                foreach (var image in images)
                {
                    await CreateImage(image, medicamentId);
                }
            }
        }

        public async Task DeleteImage(int imageId)
        {
            var image = await _repository.GetByIdAsync(imageId);

            await _blobStorage.DeleteBlobAsync(image.Name);
        }
    }
}
