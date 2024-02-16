using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("SensoriInduttivi", Schema = "L6022_1")]
    public class L6022_1SensoriInduttivi
    {
        [Key]
        public int SensoriInduttiviID { get; set; }
        public string SensoriInduttiviAcronym { get; set; }
        public string SensoriInduttiviDesc { get; set; }
        public int NAmm { get; set; }
        public int NSensori { get; set; }
        public decimal SensoriInduttiviPrice { get; set; }
        public decimal SensoriInduttiviPriceMultiplier { get; set; }
        public Boolean isActive { get; set; }


    }
}