using System.ComponentModel.DataAnnotations;

namespace TEFLY.ViewModels
{
    public class RegisterViewModel
    {
        [Required, MaxLength(150)]
        [Display(Name = "Full Name")]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, Phone]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; } = string.Empty;

        [MaxLength(20)]
        [Display(Name = "National ID")]
        public string? NationalID { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }

        [MaxLength(250)]
        public string? Address { get; set; }

        [Range(1, 120)]
        public int? Age { get; set; }

        [Required, DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters.")]
        public string Password { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
