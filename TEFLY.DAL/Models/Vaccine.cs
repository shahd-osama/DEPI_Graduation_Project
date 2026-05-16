using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TEFLY.DAL.Models
{
    public class Vaccine
    {
        [Key]
        public int VaccineID { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [MaxLength(100)]
        public string? RecommendedAge { get; set; }

        public string? DosageInfo { get; set; }

        // ── Navigation ─────────────────────────────────────
        public ICollection<VaccineEffect> VaccineEffects { get; set; } = new List<VaccineEffect>();
        public ICollection<VaccinationSchedule> VaccinationSchedules { get; set; } = new List<VaccinationSchedule>();
        public ICollection<VaccinationRecord> VaccinationRecords { get; set; } = new List<VaccinationRecord>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<AdverseReaction> AdverseReactions { get; set; } = new List<AdverseReaction>();
        public ICollection<VaccineInventory> VaccineInventories { get; set; } = new List<VaccineInventory>();
    }
}
