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

        public int EffectID { get; set; }
        [ForeignKey(nameof(EffectID))]
        public VaccineSideEffect? SideEffect { get; set; }
    }
}
