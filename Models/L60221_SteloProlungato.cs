using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("SteloProlungato", Schema = "L6022_1")]
    public class L6022_1SteloProlungato
    {
        [Key]
        public int SteloProlungatoID { get; set; }
        public decimal SteloProlungatoStartLength { get; set; }
        public decimal SteloProlungatoEndLength { get; set; }
        public decimal SteloProlungatoPriceMultiplier { get; set; }
        public decimal SteloProlungatoRunPriceMultiplier { get; set; }
        public decimal SteloProlungatoSlice { get; set; }
        public Boolean onDiscount { get; set; }



    }
}