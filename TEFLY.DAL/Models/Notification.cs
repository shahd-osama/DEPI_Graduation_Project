using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TEFLY.DAL.Models
{
    public class Notification
    {
        // ERD: NotificationID int PK
        [Key]
        public int NotificationID { get; set; }

        // ERD: UserID int FK → User
        [Required]
        public string UserID { get; set; } = string.Empty;
        [ForeignKey(nameof(UserID))]
        public ApplicationUser? User { get; set; }

        // ERD: Message text
        public string? Message { get; set; }

        // ERD: Date date
        public DateOnly Date { get; set; }

        // ERD: Type varchar  (e.g. "Reminder", "Alert", "Info")
        [MaxLength(50)]
        public string? Type { get; set; }
    }
}
