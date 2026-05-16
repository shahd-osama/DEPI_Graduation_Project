using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TEFLY.DAL.Models
{
    public class Complaint
    {
        // ERD::  ComplaintID int PK
        [Key]
        public int ComplaintID { get; set; }
 
        // ERD::  UserID int FK → User
        [Required]
        public string UserID { get; set; } = string.Empty;

        [ForeignKey(nameof(UserID))]
        public ApplicationUser? User { get; set; }
 
        public string? Description { get; set; }
 
        public string Status { get; set; } = "Open";
 
        public DateOnly Date { get; set; }

    }
 
}
