using System;
using System.Collections.Generic;
using System.Text;

namespace TEFLY.BLL.DTOs
{
    // ── User (ApplicationUser) ─────────────────────────────────
    public class UserDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? NationalID { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public int? Age { get; set; }
    }
