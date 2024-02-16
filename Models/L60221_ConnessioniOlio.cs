using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("ConnessioniOlio", Schema = "L6022_1")]
    public class L6022_1ConnessioniOlio
    {
        [Key]
        public int ConnessioniOlioID { get; set; }
        public string ConnessioniOlioDesc { get; set; }
        public decimal ConnessioniOlioMultiplier { get; set; }
        public Boolean isMagg { get; set; }
        public Boolean isActive { get; set; }
        public Boolean onDiscount { get; set; }



    }
}