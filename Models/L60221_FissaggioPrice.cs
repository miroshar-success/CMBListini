using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("FissaggioPrice", Schema = "L6022_1")]
    public class L6022_1FissaggioPrice
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