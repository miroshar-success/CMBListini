using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("MaterialeStelo", Schema = "L6022_1")]
    public class L6022_1MaterialeStelo
    {
        [Key]
        public int MaterialeSteloID { get; set; }
        public decimal MaterialeSteloPriceMultiplier { get; set; }
        public string MaterialeSteloDesc { get; set; }
        public Boolean isActive { get; set; }
        public Boolean onDiscount { get; set; }



    }
}