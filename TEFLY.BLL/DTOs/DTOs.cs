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

    // Child DTO
    public class ChildDto
    {

    }

    // Vaccine DTO
    public class VaccineDto
    {
    }

    // Adverse Reaction DTO
    public class AdverseReactionDto
    {
        public int ReactionID { get; set; }
        public int ChildID { get; set; }
        public string ChildName { get; set; } = string.Empty;
        public int VaccineID { get; set; }
        public string VaccineName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateOnly Date { get; set; }
        public string Status { get; set; } = "Reported";
    }

    // Complaint DTO
    public class ComplaintDto
    {
    }

    // Notification DTO
    public class NotificationDto
    {
    }

    // Healthcare Provider DTO
    public class HealthcareProviderDto
    {
        public int ProviderID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Type { get; set; }
        public string? Location { get; set; }
        public string? Phone { get; set; }
    }

    // Vaccination Record DTO
    public class VaccinationRecordDto
    {
    }

    // Vaccination Schedule DTO
    public class VaccinationScheduleDto
    {
    }

    // Vaccine Inventory DTO
    public class VaccineInventoryDto
    {
        public int InventoryID { get; set; }
        public int VaccineID { get; set; }
        public string VaccineName { get; set; } = string.Empty;
        public int ProviderID { get; set; }
        public string ProviderName { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }

    // Awareness DTO
    public class AwarenessDto
    {
    }

    // Appointment DTO
    public class AppointmentDto
    {
    }

    // Vaccine Effect DTO
    public class VaccineEffectDto
    {
    }

    // Vaccine Side Effect DTO
    public class VaccineSideEffectDto
    {
    }
}
