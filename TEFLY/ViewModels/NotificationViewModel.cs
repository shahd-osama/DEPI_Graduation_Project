namespace TEFLY.ViewModels
{
    public class NotificationViewModel
    {
        public int NotificationID { get; set; }

        public string? Message { get; set; }

        public DateOnly Date { get; set; }

        public string? Type { get; set; }
    }
}
