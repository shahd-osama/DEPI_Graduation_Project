using TEFLY.DAL.Models;

namespace TEFLY.ViewModels
{
    public class VaccinationRecordViewModel
    {
        public int RecordID { get; set; }

        public int ChildID { get; set; }

        public int VaccineID { get; set; }

        public int ProviderID { get; set; }

        public DateOnly DateGiven { get; set; }

        public Vaccine? Vaccine { get; set; }

    }
}
