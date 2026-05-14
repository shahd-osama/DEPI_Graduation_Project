using System.ComponentModel.DataAnnotations;

namespace TEFLY.ViewModels
{
    public class VaccineViewModel
    {
        public int VaccineID { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? RecommendedAge { get; set; }

        public string? DosageInfo { get; set; }
    }
}
