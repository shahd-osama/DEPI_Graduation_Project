using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TEFLY.DAL.Models
{
    public enum VaccinationStatus
    {
        Scheduled,
        Completed,
        Missed,
        Overdue ,
        Pending
    }

    public class VaccinationRecord
    {
        // ERD: RecordID int PK
        [Key]
        public int RecordID { get; set; }

        // ERD: ChildID int FK
        [Required]
        public int ChildID { get; set; }
        [ForeignKey(nameof(ChildID))]
        public Child? Child { get; set; }

        // ERD: VaccineID int FK
        [Required]
        public int VaccineID { get; set; }
        [ForeignKey(nameof(VaccineID))]
        public Vaccine? Vaccine { get; set; }

        // ERD: ProviderID int FK → HealthcareProvider
        [Required]
        public int ProviderID { get; set; }
        [ForeignKey(nameof(ProviderID))]
        public HealthcareProvider? Provider { get; set; }

        // ERD: DateGiven date
        [Required]
        public DateOnly DateGiven { get; set; }

        public string? GivenBy { get; set; }

        public string? BatchNumber { get; set; }

        public string Status { get; set; } = "Pending";
    }
}