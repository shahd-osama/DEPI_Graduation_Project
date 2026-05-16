using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TEFLY.DAL.Models
{
    public class Awareness
    {
        // ERD::  ID int PK
        [Key]
        public int ID { get; set; }

        // ERD: Title varchar
        [Required, MaxLength(300)]

        // ERD::  Title varchar
        public string Title { get; set; } = string.Empty;

        public string? Body { get; set; }

        // ERD: Category varchar  (e.g. "Vaccine Safety", "Disease Prevention")
        [MaxLength(100)]

        // ERD:: Category varchar
        public string? Category { get; set; }
        [MaxLength(300)]

        public string? Tags { get; set; }
        [MaxLength(500)]

        public string? MediaUrl { get; set; }
        [MaxLength(50)]

        public string Status { get; set; } = "Draft";
    }
}
 