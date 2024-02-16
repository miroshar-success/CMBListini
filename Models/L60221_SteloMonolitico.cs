using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("SteloMonolitico", Schema = "L6022_1")]
    public class L6022_1SteloMonolitico
    {
        [Key]
        public int SteloMonoliticoID { get; set; }
        public decimal SteloMonoliticoPriceMultiplier { get; set; }
        public Boolean inUse { get; set; }
        public Boolean onDiscount { get; set; }



    }
}