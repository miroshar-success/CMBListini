using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("FissaggioPrice", Schema = "L6020_2")]
    public class L6020_2FissaggioPrice
    {
        [Key]
        public int FissaggioPriceID { get; set; }
        public decimal FissaggioAlesaggioLength { get; set; }
        public decimal FissaggioPrice { get; set; }
        public int FissaggioPriceCategoryID { get; set; }
        public decimal FissaggioMagg { get; set; }
        public Boolean onDiscountMagg { get; set; }



    }
}