using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TEFLY.DAL.Models
{
    public class VaccineInventory
    {
        // ERD: InventoryID int PK
        [Key]
        public int InventoryID { get; set; }

        // ERD: VaccineID int FK
        [Required]
        public int VaccineID { get; set; }
        [ForeignKey(nameof(VaccineID))]
        public Vaccine? Vaccine { get; set; }

        // ERD: ProviderID int FK → HealthcareProvider
        [Required]
        public int ProviderID { get; set; }
        [ForeignKey(nameof(ProviderID))]
        public HealthcareProvider? Provider { get; set; }

        // ERD: Quantity int
        public int Quantity { get; set; }
    }

}
