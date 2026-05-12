using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TEFLY.DAL.Models
{
    public class VaccineEffect
    {
        public int VaccineID { get; set; }
        [ForeignKey(nameof(VaccineID))]
        public Vaccine? Vaccine { get; set; }

        public int SideEffectID { get; set; }
        [ForeignKey(nameof(SideEffectID))]
        public VaccineSideEffect? SideEffect { get; set; }
    }
}
