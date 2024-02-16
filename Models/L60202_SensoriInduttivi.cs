using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("SensoriInduttivi", Schema = "L6020_2")]
    public class L6020_2SensoriInduttivi
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