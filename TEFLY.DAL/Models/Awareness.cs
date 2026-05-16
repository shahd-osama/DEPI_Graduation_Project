using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TEFLY.DAL.Models
{
    public class Awareness
    {
        [Key]
        public int ID { get; set; }
        [Required, MaxLength(300)]
        public string Title { get; set; } = string.Empty;
        public string? Body { get; set; }
        [MaxLength(100)]
        public string? Category { get; set; }
        [MaxLength(300)]
        public string? Tags { get; set; }
        [MaxLength(500)]
        public string? MediaUrl { get; set; }
        [MaxLength(50)]
        public string Status { get; set; } = "Draft";
    }
}
