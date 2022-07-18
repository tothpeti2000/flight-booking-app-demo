using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using FlyTonight.API.Options;
using FlyTonight.Application.Interfaces;
using Microsoft.Extensions.Options;

namespace FlyTonight.API.Services
{
    public class BlobStorage : IStorage
    {
        private readonly BlobOptions options;

        public BlobStorage(IOptionsSnapshot<BlobOptions> options)
        {
            this.options = options.Value;
        }

        public async Task<string> UploadPartnerImage(string Base64Image)
        {
            return await UpdloadImage(Base64Image, options.PartnersContainer);
        }

        public async Task<string> UploadDiscountImage(string Base64Image)
        {
            return await UpdloadImage(Base64Image, options.DiscountsContainer);
        }

        public async Task<string> UploadAirportImage(string Base64Image)
        {
            return await UpdloadImage(Base64Image, options.AirportsContainer);
        }

        private string GetBlobName(string imageName, string containerName)
        {
            return $"{options.BaseUrl}/{containerName}/{imageName}";
        }

        private async Task<string> UpdloadImage(string Base64Image, string containerName)
        {
            var container = new BlobContainerClient(options.ConnectionString, containerName);
            await container.CreateIfNotExistsAsync(PublicAccessType.BlobContainer);

            string imageName = Guid.NewGuid().ToString() + ".png";
            var client = new BlobClient(options.ConnectionString, containerName, imageName);

            using (var stream = new MemoryStream(Convert.FromBase64String(Base64Image)))
            {
                await client.UploadAsync(stream);
            }
            return GetBlobName(imageName, containerName);
        }
    }
}
