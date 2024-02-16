using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("ProtezioneSensore", Schema = "L6022_1")]
    public class L6022_1ProtezioneSensore
    {
        [Key]
        public int ProtezioneSensoreID { get; set; }
        public decimal ProtezioneSensorePrice { get; set; }
        public Boolean inUse { get; set; }
        public Boolean onDiscount { get; set; }


    }
}