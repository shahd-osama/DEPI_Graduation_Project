namespace TEFLY.ViewModels
{
    public class HealthcareProviderViewModel
    {
        public int ProviderID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Type { get; set; }

        public string? Location { get; set; }

        public string? Phone { get; set; }
    }
}
