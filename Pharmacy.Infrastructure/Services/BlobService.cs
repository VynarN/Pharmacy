using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Pharmacy.Application.Common.Constants;
using Pharmacy.Application.Common.DTO.In.BlobIn;
using Pharmacy.Application.Common.Exceptions;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.Infrastructure.Services
{
    public class BlobService: IBlobStorage
    {
        private readonly BlobServiceClient _blobServiceClient;

        private readonly IConfiguration _configuration;

        private readonly ILogger<BlobService> _logger;

        public BlobService(BlobServiceClient blobServiceClient, IConfiguration configuration, ILogger<BlobService> logger)
        {
            _blobServiceClient = blobServiceClient;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<BlobInfoDto> GetBlobAsync(string blobName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("BlobContainer");

            var blobClient = containerClient.GetBlobClient(blobName);

            var blobDownloadInfo = await blobClient.DownloadAsync();

            return new BlobInfoDto(blobDownloadInfo.Value.Content, blobDownloadInfo.Value.ContentType);
        }

        public async Task<IEnumerable<string>>  GetBlobsNamesAsync()
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_configuration["BlobContainer"]);

            var items = new List<string>();

            await foreach (var blobItem in containerClient.GetBlobsAsync())
            {
                items.Add(blobItem.Name);
            }

            return items;
        }

        public async Task DeleteBlobAsync(string blobName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_configuration["BlobContainer"]);

            var blobClient = containerClient.GetBlobClient(blobName);

            await blobClient.DeleteIfExistsAsync();
        }

        public async Task<string> UploadBlobAsync(IFormFile blob)
        {
            try
            {
                var containerClient = _blobServiceClient.GetBlobContainerClient(_configuration["BlobContainer"]);

                var containerDisposition = ContentDispositionHeaderValue.Parse(blob.ContentDisposition);

                var fileName = containerDisposition.FileName.Trim().ToString();

                var blockBlob = containerClient.GetBlockBlobClient(fileName);

                await blockBlob.UploadAsync(blob.OpenReadStream(), new BlobHttpHeaders { ContentType = blob.ContentType });

                return blockBlob.Uri.AbsoluteUri;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                throw new ObjectCreateException(ExceptionStrings.FileUploading);
            }
        }
    }
}
