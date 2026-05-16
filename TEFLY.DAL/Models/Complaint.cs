using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TEFLY.DAL.Models
{
    public class Complaint
    {
        // ERD: ComplaintID int PK
        [Key]
        public int ComplaintID { get; set; }
 
        // ERD: UserID int FK → User
        [Required]
        public string UserID { get; set; } = string.Empty;
        [ForeignKey(nameof(UserID))]
        public ApplicationUser? User { get; set; }
 
        // ERD: Description text
        public string? Description { get; set; }
 
        // ERD: Status varchar  (e.g. "Open", "In Progress", "Closed")
        [MaxLength(50)]
        public string Status { get; set; } = "Open";
 
        // ERD: Date date
        public DateOnly Date { get; set; }
    }
 
}
