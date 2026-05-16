using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TEFLY.DAL.Models
{
    public class Child
    {
        // ERD::  ChildID int PK
        [Key]
        public int ChildID { get; set; }

        // ERD::  UserID int FK → User
        [Required]
        public string UserID { get; set; } = string.Empty;

        [ForeignKey(nameof(UserID))]
        public ApplicationUser? User { get; set; }

        // ERD::  Name varchar
        [Required, MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        // ERD:: Age int
        public int Age { get; set; }

        // ERD:: NationalID varchar
        [MaxLength(20)]
        public string? NationalID { get; set; }

        // ERD:: HasDiseases boolean
        public bool HasDiseases { get; set; } = false;

        // ERD:: DiseasesDescription text
        public string? DiseasesDescription { get; set; }

        // ── Navigation ── ───────────────────────────────────
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<VaccinationRecord> VaccinationRecords { get; set; } = new List<VaccinationRecord>();
        public ICollection<AdverseReaction> AdverseReactions { get; set; } = new List<AdverseReaction>();
    }
}