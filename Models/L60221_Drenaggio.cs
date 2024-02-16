using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("Drenaggio", Schema = "L6022_1")]
    public class L6022_1Drenaggio
    {
        [Key]
        public int DrenaggioID { get; set; }
        public decimal DrenaggioPriceMultiplier { get; set; }
        public int DrenaggioGuarnizioniID { get; set; }
        public Boolean onDiscount { get; set; }
        public Boolean inUse { get; set; }

    }
}