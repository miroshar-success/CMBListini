using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("TrasduttorePrice", Schema = "L6020_2")]
    public class L6020_2TrasduttorePrice
    {
        [Key]
        public int TrasduttorePriceID { get; set; }
        public int TrasduttorePriceCategoryID { get; set; }
        public string TrasduttorePriceDesc { get; set; }
        public decimal TrasduttorePriceRun { get; set; }
        public decimal TrasduttorePriceAfterRun { get; set; }
        public decimal TrasduttorePriceConn { get; set; }
        public decimal TrasduttorePriceRunSlice { get; set; }
        public Boolean isActive { get; set; }
        public Boolean onDiscount { get; set; }


    }
}