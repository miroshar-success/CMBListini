using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("Minimess", Schema = "L6020_2")]
    public class L6020_2Minimess
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