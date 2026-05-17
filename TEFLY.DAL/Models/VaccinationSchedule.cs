using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TEFLY.DAL.Models
{
    /// <summary>
    /// ERD: VaccinationSchedule — defines when each dose of a vaccine should be given.
    /// </summary>
    public class VaccinationSchedule
    {
        // ERD: ScheduleID int PK
        [Key]
        public int ScheduleID { get; set; }

        // ERD: VaccineID int FK
        [Required]
        public int VaccineID { get; set; }
        [ForeignKey(nameof(VaccineID))]
        public Vaccine? Vaccine { get; set; }

        // ERD: AgeStage varchar  (e.g. "Birth", "2 months", "12 months")
        [Required, MaxLength(100)]
        public string AgeStage { get; set; } = string.Empty;

        // ERD: DoseNumber int
        public int DoseNumber { get; set; }
    }
}