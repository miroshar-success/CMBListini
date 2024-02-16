using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("FilettaturaStelo", Schema = "L6022_1")]
    public class L6022_1FilettaturaStelo
    {
        [Key]
        public int FilettaturaSteloID { get; set; }
        public decimal FilettaturaSteloPrice { get; set; }
        public string FilettaturaSteloAcronym { get; set; }
        public string FilettaturaSteloDesc { get; set; }
        public Boolean XOption { get; set; }
        public Boolean isActive { get; set; }
        public Boolean onDiscount { get; set; }


    }
}