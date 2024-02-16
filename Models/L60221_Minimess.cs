using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("Minimess", Schema = "L6022_1")]
    public class L6022_1Minimess
    {
        [Key]
        public int MinimessID { get; set; }
        public decimal MinimessPrice { get; set; }
        public decimal MinimessPriceMultiplier { get; set; }
        public string MinimessDesc { get; set; }
        public Boolean onDiscount { get; set; }
        public Boolean isActive { get; set; }


    }
}