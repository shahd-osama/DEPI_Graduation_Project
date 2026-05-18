using System.ComponentModel.DataAnnotations;

namespace TEFLY.ViewModels
{
    public class ChildViewModel
    {
        public int ChildID { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }

        public string? NationalID { get; set; }

        public bool HasDiseases { get; set; }

        public string? DiseasesDescription { get; set; }
    }
}
