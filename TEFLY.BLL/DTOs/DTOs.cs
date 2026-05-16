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
        public int ChildID { get; set; }
        public string UserID { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string? NationalID { get; set; }
        public bool HasDiseases { get; set; }
        public string? DiseasesDescription { get; set; }
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

    // ── Complaint ──────────────────────────────────────────────
    public class ComplaintDto
    {
        public int ComplaintID { get; set; }
        public string UserID { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Status { get; set; } = "Open";
        public DateOnly Date { get; set; }
    }

    // Notification DTO
    public class NotificationDto
    {
        public int NotificationID { get; set; }
        public string UserID { get; set; } = string.Empty;
        public string? Message { get; set; }
        public DateOnly Date { get; set; }
        public string? Type { get; set; }
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
        public int RecordID { get; set; }
        public int ChildID { get; set; }
        public string ChildName { get; set; } = string.Empty;
        public int VaccineID { get; set; }
        public string VaccineName { get; set; } = string.Empty;
        public int ProviderID { get; set; }
        public string ProviderName { get; set; } = string.Empty;
        public DateOnly DateGiven { get; set; }
    }

    // Vaccination Schedule DTO
    public class VaccinationScheduleDto
    {
        public int ScheduleID { get; set; }
        public int VaccineID { get; set; }
        public string VaccineName { get; set; } = string.Empty;
        public string AgeStage { get; set; } = string.Empty;
        public int DoseNumber { get; set; }
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
        public int ID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Body { get; set; }
        public string? Category { get; set; }
        public string? Tags { get; set; }
        public string? MediaUrl { get; set; }
        public string Status { get; set; } = "Draft";
    }

    // Appointment DTO
    public class AppointmentDto
    {
        public int AppointmentID { get; set; }
        public int ChildID { get; set; }
        public string ChildName { get; set; } = string.Empty;
        public int VaccineID { get; set; }
        public string VaccineName { get; set; } = string.Empty;
        public int ProviderID { get; set; }
        public string ProviderName { get; set; } = string.Empty;
        public DateOnly Date { get; set; }
        public TimeOnly? Time { get; set; }
        public string Status { get; set; } = "Pending";
        public string? Note { get; set; }
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
