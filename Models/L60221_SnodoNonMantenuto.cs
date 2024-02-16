using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("SnodoNonMantenuto", Schema = "L6022_1")]
    public class L6022_1SnodoNonMantenuto
    {
        [Key]
        public int SnodoNonMantenutoID { get; set; }
        public decimal SteloValue { get; set; }
        public decimal AlesaggioLength { get; set; }
        public Boolean onDiscount { get; set; }
        public decimal SnodoNonMantenutoPrice { get; set; }

    }
}