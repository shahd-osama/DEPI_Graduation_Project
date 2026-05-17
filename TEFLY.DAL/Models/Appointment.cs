using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TEFLY.DAL.Models
{
    public class Appointment
    {
        // ERD: AppointmentID int PK
        [Key]
        public int AppointmentID { get; set; }

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

        // ERD: ProviderID int FK → HealthcareProvider (NOT ApplicationUser)
        [Required]
        public int ProviderID { get; set; }
        [ForeignKey(nameof(ProviderID))]
        public HealthcareProvider? Provider { get; set; }

        // ERD: Date date
        [Required]
        public DateOnly Date { get; set; }

        // ERD: Time time
        public TimeOnly? Time { get; set; }

        // ERD: Status varchar
        [MaxLength(50)]
        public string Status { get; set; } = "Pending";

        // ERD: Note text
        public string? Note { get; set; }
    }
}