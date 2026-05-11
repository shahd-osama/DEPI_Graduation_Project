using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TEFLY.DAL.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? NationalID { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }

        [MaxLength(250)]
        public string? Address { get; set; }
        public int? Age { get; set; }

    }

}
