namespace FlyTonight.API.Options
{
    public class BlobOptions
    {
        public const string Blob = "Blob";

        public string ConnectionString { get; set; } = String.Empty;
        public string BaseUrl { get; set; } = String.Empty;
        public string PartnersContainer { get; set; } = String.Empty;
        public string DiscountsContainer { get; set; } = String.Empty;
        public string AirportsContainer { get; set; } = String.Empty;
    }
}
