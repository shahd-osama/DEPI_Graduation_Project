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
        [Required, MaxLength(300)]

        // ERD::  Title varchar
        public string Title { get; set; } = string.Empty;

        // ERD:: Body text
        public string? Body { get; set; }
        [MaxLength(100)]

        // ERD:: Category varchar
        public string? Category { get; set; }
        [MaxLength(300)]

        // ERD:: Tags varchar
        public string? Tags { get; set; }
        [MaxLength(500)]

        // ERD:: MediaUrl varchar
        public string? MediaUrl { get; set; }
        [MaxLength(50)]

        // ERD:: Status varchar
        public string Status { get; set; } = "Draft";
    }
}
