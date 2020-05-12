using System.IO;

namespace Pharmacy.Application.Common.DTO.In.BlobIn
{
    public class BlobInfoDto
    {
        public BlobInfoDto(Stream stream, string contentType)
        {
            Stream = stream;
            ContentType = contentType;
        }

        public Stream Stream { get; set; }

        public string ContentType { get; set; }
    }
}
