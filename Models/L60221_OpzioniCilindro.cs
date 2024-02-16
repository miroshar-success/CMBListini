using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("OpzioniCilindro", Schema = "L6022_1")]
    public class L6022_1OpzioniCilindro
    {
        [Key]
        public int OpzioniCilindroID { get; set; }
        public string OpzioniCilindroDesc { get; set; }
        public decimal OpzioniCilindroMultiplier { get; set; }
        public decimal OpzioniCilindroPrice { get; set; }
        public int OpzioniCilindroCategoryID { get; set; }
        public string OpzioniCilindroVar { get; set; }
        public Boolean onDiscount { get; set; }
        public Boolean isActive { get; set; }


    }
}