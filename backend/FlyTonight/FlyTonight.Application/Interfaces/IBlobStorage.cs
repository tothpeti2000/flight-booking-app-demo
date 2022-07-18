namespace FlyTonight.Application.Interfaces
{
    public interface IStorage
    {
        public Task<string> UploadPartnerImage(string Base64Image);

        public Task<string> UploadDiscountImage(string Base64Image);

        public Task<string> UploadAirportImage(string Base64Image);
    }
}
