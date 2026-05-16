using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TEFLY.DAL.Models
{
    public class Awareness
    {
        // ERD: ID int PK
        [Key]
        public int ID { get; set; }

        // ERD: Title varchar
        [Required, MaxLength(300)]
        public string Title { get; set; } = string.Empty;

        // ERD: Body text
        public string? Body { get; set; }

        // ERD: Category varchar  (e.g. "Vaccine Safety", "Disease Prevention")
        [MaxLength(100)]
        public string? Category { get; set; }

        // ERD: Tags varchar
        [MaxLength(300)]
        public string? Tags { get; set; }

        // ERD: MediaUrl varchar
        [MaxLength(500)]
        public string? MediaUrl { get; set; }

        // ERD: Status varchar  (e.g. "Published", "Draft")
        [MaxLength(50)]
        public string Status { get; set; } = "Draft";
    }
}
 