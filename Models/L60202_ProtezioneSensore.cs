using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("ProtezioneSensore", Schema = "L6020_2")]
    public class L6020_2ProtezioneSensore
    {
        [Key]
        public int ProtezioneSensoreID { get; set; }
        public decimal ProtezioneSensorePrice { get; set; }
        public Boolean inUse { get; set; }
        public Boolean onDiscount { get; set; }


    }
}