using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("Base", Schema = "L6020_2")]
    public class L6020_2Base
    {
        [Key]
        public int BaseID { get; set; }
        public decimal AlesaggioLength { get; set; }
        public decimal SteloValue { get; set; }
        public decimal UntilRunPrice { get; set; }
        public decimal BeyondRunPrice { get; set; }
        public decimal SpacerPrice { get; set; }
        public decimal LimitLength { get; set; }
        public Boolean BaseInox { get; set; }
        public Boolean BaseStP { get; set; }
        public decimal IncreaseRunMultiplier { get; set; }




    }
}