using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TEFLY.DAL.Models
{
    public class AdverseReaction
    {
        // ERD: ReactionID int PK
        [Key]
        public int ReactionID { get; set; }

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

        // ERD: Description text
        public string? Description { get; set; }

        // ERD: Date date
        public DateOnly Date { get; set; }

        // ERD: Status varchar  (e.g. "Reported", "Under Review", "Resolved")
        [MaxLength(50)]
        public string Status { get; set; } = "Reported";
    }

}
