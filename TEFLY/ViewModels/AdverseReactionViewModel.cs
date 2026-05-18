namespace TEFLY.ViewModels
{
    public class AdverseReactionViewModel
    {
        public int ReactionID { get; set; }

        public int ChildID { get; set; }

        public int VaccineID { get; set; }

        public string? Description { get; set; }

        public DateOnly Date { get; set; }

        public string Status { get; set; } = "Reported";
    }
}
