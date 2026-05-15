using System.ComponentModel.DataAnnotations;

namespace TEFLY.DAL.Models
{
    /// <summary>
    /// ERD: HealthcareProvider — clinic/hospital that administers vaccines.
    /// This is a SEPARATE entity from ApplicationUser (not an Identity account).
    /// </summary>
    public class HealthcareProvider
    {
        // ERD: ProviderID int PK
        [Key]
        public int ProviderID { get; set; }

        // ERD: Name varchar
        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        // ERD: Type varchar  (e.g. "Hospital", "Clinic", "Pharmacy")
        [MaxLength(100)]
        public string? Type { get; set; }

        // ERD: Location varchar
        [MaxLength(300)]
        public string? Location { get; set; }

        // ERD: Phone varchar
        [MaxLength(20)]
        public string? Phone { get; set; }

        // ── Navigation ─────────────────────────────────────
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<VaccinationRecord> VaccinationRecords { get; set; } = new List<VaccinationRecord>();
        public ICollection<VaccineInventory> VaccineInventories { get; set; } = new List<VaccineInventory>();
    }
}