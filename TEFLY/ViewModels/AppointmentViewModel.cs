namespace TEFLY.ViewModels
{
    public class AppointmentViewModel
    {
        public int AppointmentID { get; set; }

        public int ChildID { get; set; }

        public int VaccineID { get; set; }

        public int ProviderID { get; set; }

        public DateOnly Date { get; set; }

        public TimeOnly? Time { get; set; }

        public string Status { get; set; } = "Pending";

        public string? Note { get; set; }
    }
}
