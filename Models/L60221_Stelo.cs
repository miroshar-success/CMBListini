using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("Stelo", Schema = "L6022_1")]
    public class L6022_1Stelo
    {
        [Key]
        public int SteloID { get; set; }
        public string SteloDesc { get; set; }
        public string SteloAcronym { get; set; }
        public int SteloAlesaggioID { get; set; }
        public decimal SteloValue { get; set; }
        public Boolean SteloisActive { get; set; }


    }
}