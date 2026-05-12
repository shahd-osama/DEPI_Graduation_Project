using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TEFLY.DAL.Models
{
    public class VaccineSideEffect
    {
        [Key]
        public int EffectID { get; set; }
        [Required, MaxLength(150)]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public bool IsCommon { get; set; } = false;

        public ICollection<VaccineEffect> VaccineEffects { get; set; } = new List<VaccineEffect>();
    }
}
