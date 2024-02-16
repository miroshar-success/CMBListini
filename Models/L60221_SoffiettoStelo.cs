using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("SoffiettoStelo", Schema = "L6022_1")]
    public class L6022_1SoffiettoStelo
    {
        [Key]
        public int SoffiettoSteloID { get; set; }
        public decimal SoffiettoSteloFrom { get; set; }
        public decimal SoffiettoSteloTo { get; set; }
        public decimal SoffiettoSteloPrice { get; set; }
        public Boolean onDiscount { get; set; }

    }
}