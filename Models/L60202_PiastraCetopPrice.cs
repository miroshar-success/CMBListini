using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("PiastraCetopPrice", Schema = "L6020_2")]
    public class L6020_2PiastraCetopPrice
    {
        [Key]
        public int PiastraCetopPriceID { get; set; }
        public int PiastraCetopCategoryID { get; set; }
        public decimal AlesaggioLength { get; set; }
        public decimal PiastraCetopPrice { get; set; }
        public Boolean onDiscount { get; set; }
    }
}