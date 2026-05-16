using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TEFLY.DAL.Models
{
    public class Complaint
    {
        [Key]
        public int ComplaintID { get; set; }
        [Required]
        public string UserID { get; set; } = string.Empty;
        [ForeignKey(nameof(UserID))]
        public ApplicationUser? User { get; set; }
        public string? Description { get; set; }
        [MaxLength(50)]
        public string Status { get; set; } = "Open";
        public DateOnly Date { get; set; }
    }
}
